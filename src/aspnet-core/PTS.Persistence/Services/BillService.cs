using PTS.Application.Interfaces.Repositories;
using PTS.Core.Services;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Shared.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using PTS.Shared.Interfaces;
using PTS.Domain.Model.Base;
using PTS.Application.Features.Bill.Commands;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using PTS.Shared;
using System.Threading;
using Nest;
using PTS.Application.Features.Bill.DTOs;
using Microsoft.EntityFrameworkCore;

namespace PTS.Persistence.Services
{
    public class BillService : IBillService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillRepository _billRepository;
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ResponseDto _reponse;
        private readonly PBillDto _reponseBill;
        private static IEnumerable<CartItemDto>? cartItem;
        private readonly IMapper _mapper;
        public BillService(IBillRepository billRepository, IBillDetailRepository billDetailRepository,
            IProductDetailRepository productDetailRepository, IUserRepository userRepository,
            ICartRepository cartRepository, IVoucherRepository voucherRepository,
             UserManager<UserEntity> userManager, IMapper mapper, IUnitOfWork unitOfWork
            )
        {
            _billRepository = billRepository;
            _billDetailRepository = billDetailRepository;
            _productDetailRepository = productDetailRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _reponse = new ResponseDto();
            _reponseBill = new PBillDto();
            _voucherRepository = voucherRepository;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<BillDto>> CreateBill(PBillCreateCommand command)
        {
            try
            {
                UserEntity user = null;
                if (!string.IsNullOrEmpty(command.UserName))
                {
                    user = await _userManager.FindByNameAsync(command.UserName);
                }

                IEnumerable<CartItemDto> cartItems = null;

                // Check User
                if (!string.IsNullOrEmpty(command.UserName))
                {
                    cartItems = await _cartRepository.GetCartItem(command.UserName);
                    if (cartItems == null)
                    {
                        return new ApiResult<BillDto>
                        {
                            IsSuccessed = false,
                            Message = "Không có sản phẩm trong giỏ hàng"
                        };
                    }

                    // Check for product availability
                    foreach (var item in cartItems)
                    {
                        int availableQuantity = GetCount(item.IdProductDetails);
                        if (item.Quantity > availableQuantity)
                        {
                            return new ApiResult<BillDto>
                            {
                                IsSuccessed = false,
                                Message = $"Sản phẩm {item.MaProductDetail} chỉ còn {availableQuantity}"
                            };
                        }
                    }
                }

                // Get voucher 
                var vouchers = await _voucherRepository.GetAll();
                var appliedVoucher = vouchers.FirstOrDefault(v => v.MaVoucher == command.CodeVoucher);

                // Create a new bill entity
                var bill = new BillEntity
                {
                    Id = 0,
                    InvoiceCode = StringUtility.RandomString(12),
                    CrDateTime = DateTime.Now,
                    Status = 2, // Status 2: Pending 
                    FullName = user?.FullName ?? command.FullName,
                    PhoneNumber = user?.PhoneNumber ?? command.PhoneNumber,
                    Address = command.Address,
                    UserEntityId = user?.Id,
                    Payment = command.Payment,
                    IsPayment = false,
                    VoucherEntityId = appliedVoucher?.Id
                };

                // cart items to add to the bill
                IEnumerable<CartItemDto> itemsToAdd = command.UserName == null ? command.CartItem : cartItems;
                decimal totalValue = 0;

                // Calculate total value and check product availability again
                foreach (var item in itemsToAdd)
                {
                    totalValue += item.NewPrice * item.Quantity;
                    int availableQuantity = GetCount(item.IdProductDetails);
                    if (item.Quantity > availableQuantity)
                    {
                        return new ApiResult<BillDto>
                        {
                            IsSuccessed = false,
                            Message = $"Sản phẩm {item.MaProductDetail} chỉ còn {availableQuantity}"
                        };
                    }
                }

                // Validate voucher 
                if (appliedVoucher != null && totalValue / 2 < appliedVoucher.GiaTri)
                {
                    return new ApiResult<BillDto>
                    {
                        IsSuccessed = false,
                        Message = $"Mã giảm giá không phù hợp, bạn chỉ có thể dùng mã giảm giá tối đa 50% giá trị của đơn hàng"
                    };
                }

                // Create the bill and associated details
                if (await _billRepository.Create(bill))
                {
                    foreach (var item in itemsToAdd)
                    {
                        var billDetail = new BillDetailEntity
                        {
                            Id = 0,
                            Code = bill.InvoiceCode + StringUtility.RandomString(7),
                            CodeProductDetail = item.MaProductDetail,
                            Price = item.NewPrice,
                            Quantity = item.Quantity,
                            BillEntityId = bill.Id
                        };
                        await _billDetailRepository.CreateBillDetail(billDetail);
                    }

                    var billDto = _mapper.Map<BillDto>(bill);

                    // Clear the user's cart if applicable
                    if (user != null)
                    {
                        var cartDetails = _unitOfWork.Repository<CartDetailEntity>()
                            .Entities
                            .Where(x => x.CartEntityId == user.Id)
                            .ToList();
                        await _unitOfWork.Repository<CartDetailEntity>().DeleteManyAsync(cartDetails);
                    }

                    // Save the changes and return the result
                    var result = await _unitOfWork.Save(new CancellationToken());
                    return new ApiResult<BillDto>
                    {
                        IsSuccessed = true,
                        Message = $"{bill.InvoiceCode}",
                        ResultObj = billDto
                    };
                }
                else
                {
                    return new ApiResult<BillDto>
                    {
                        IsSuccessed = false,
                        Message = "Đặt hàng thất bại"
                    };
                }
            }
            catch (Exception e)
            {
                return new ApiResult<BillDto>
                {
                    IsSuccessed = false,
                    Message = "Có lỗi gì đó: " + e.Message
                };
            }
        }
        private int GetCount(int id)
        {
            int getCount = _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
                .Where(x => x.Status > 0 && x.Id == id)
                .Join(_unitOfWork.Repository<SerialEntity>().Entities.AsNoTracking().Where(x => x.BillDetailEntityId == null),
                      a => a.Id,
                      b => b.ProductDetailEntityId,
                      (a, b) => new { a.Id })
                .Count();
            return getCount;
        }
        public async Task<ResponseDto> PGetBillByInvoiceCode(string invoiceCode)
        {
            var billT = await _billRepository.GetBillByInvoiceCode(invoiceCode);
            if (billT != null)
            {
                var listBillDetail = await _billRepository.GetBillDetailByInvoiceCode(invoiceCode);
                _reponseBill.InvoiceCode = billT.InvoiceCode;
                _reponseBill.PhoneNumber = billT.PhoneNumber;
                _reponseBill.FullName = billT.FullName;
                _reponseBill.Address = billT.Address;
                _reponseBill.Status = billT.Status;
                _reponseBill.CreateDate = billT.CreateDate;
                _reponseBill.CodeVoucher = billT.CodeVoucher;
                _reponseBill.GiamGia = billT.GiamGia;
                _reponseBill.Payment = billT.Payment;
                if (billT.Payment == 1)
                {
                    _reponseBill.StringPayment = "Thanh toán tại cửa hàng";
                }
                else if (billT.Payment == 2)
                {
                    _reponseBill.StringPayment = "Thanh toán khi nhận hàng (COD)";
                }
                else if (billT.Payment == 3)
                {
                    _reponseBill.StringPayment = "Thanh toán bằng chuyển khoản ngân hàng";
                }
                else if (billT.Payment == 4)
                {
                    _reponseBill.StringPayment = "Thanh toán qua VNPAY";
                }
                else
                {
                    _reponseBill.StringPayment = "Không xác định";
                }
                _reponseBill.IsPayment = billT.IsPayment;
                _reponseBill.UserId = billT.UserId;
                _reponseBill.BillDetail = listBillDetail;
                _reponseBill.Count = listBillDetail.Count();
                _reponse.Message = $"Lấy hóa đơn của khách hàng {invoiceCode} thành công.";
                _reponse.Result = _reponseBill;
                return _reponse;
            }
            _reponse.Code = 404;
            _reponse.IsSuccess = false;
            _reponse.Message = $"Không tìm thấy hóa đơn của khách hàng {invoiceCode}.";
            return _reponse;
        }
        public async Task<ResponseDto> GetAllBill(string? phoneNumber)
        {
            _reponse.Result = await _billRepository.GetAll();
            return _reponse;
        }
        public async Task<ResponseDto> GetBillDetailByInvoiceCode(string invoiceCode)
        {
            _reponseBill.Count = 1;
            _reponse.Message = $"thành công.";
            _reponse.Result = await _billRepository.GetBillDetailByInvoiceCode(invoiceCode);
            return _reponse;
        }
        private ResponseDto NotFoundResponse(string message)
        {
            return new ResponseDto
            {
                Result = null,
                IsSuccess = false,
                Code = 404,
                Message = message
            };
        }

        private ResponseDto SuccessResponse(BillEntity bill, string message)
        {
            return new ResponseDto
            {
                Result = bill,
                IsSuccess = true,
                Code = 200,
                Message = message
            };
        }

        private ResponseDto ErrorResponse(string message)
        {
            return new ResponseDto
            {
                Result = null,
                IsSuccess = false,
                Code = 400,
                Message = message
            };
        }
    }
}


