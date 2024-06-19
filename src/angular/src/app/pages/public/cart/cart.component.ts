import { Component, Input, OnInit } from '@angular/core';
import { PublicService } from '../../../shared/services/public.service';
import { AccountService } from 'src/app/shared/services/account.service';
import { CartItemDto, RequestBillDto, ResponseDto, VoucherDto, } from '../../../shared/models/model';
import { NzMessageService } from 'ng-zorro-antd/message';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {
  radioValue = 'A';
  cartItems: CartItemDto[] = [];
  createBillForm: FormGroup;
  username!: string;
  //@Input() request: RequestBillDto | undefined;
  request!: RequestBillDto;
  loadVouchers: VoucherDto[] = [];
  constructor(
    private publicService: PublicService, private nzMessageService: NzMessageService, private fb: FormBuilder,
    private accountService: AccountService, private router: Router) {
    this.createBillForm = this.fb.group({
      fullName: ['', [Validators.required]],
      address: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      email: ['', [Validators.required]],
      codeVoucher: [''],
      payment: [null, [Validators.required]]
    });
    this.request = {
      fullName: 'sấ', // Cần khởi tạo giá trị mặc định cho các trường
      address: 'Hà Nội',
      phoneNumber: '',
      //email: '',
      codeVoucher: '',
      payment: 1,
      cartItem: [] // Cần khởi tạo giá trị mặc định cho các trường
    };
  }
  ngOnInit(): void {
    this.loadCart();
    this.loadListVoucher();
    console.log(this.accountService.getUsername());
  }

  loadCart(): void {
    this.username = this.accountService.getUsername();
    if (this.username) {
      this.publicService.getCartByUser().subscribe(
        (data: CartItemDto[]) => {
          this.cartItems = data;
          console.log(this.cartItems)
        },
        (error) => {
          console.error('Error fetching products:', error);
        }
      );
    }
    else {
      const cartItemsString = localStorage.getItem('cartItems');
      if (cartItemsString) {
        this.cartItems = JSON.parse(cartItemsString);
      }
    }
  }
  calculateTotalPrice(): number {
    let totalPrice = 0;
    this.cartItems.forEach(item => {
      totalPrice += item.price * item.quantity;
    });
    return totalPrice;
  }
  cancel(): void {
    this.nzMessageService.info('Bạn đã hủy thao tác');
  }
  deleteCartDetail(id: number): void {
    if (this.username) {
      this.publicService.deleteCartDetai(id).subscribe(response => {
        this.loadCart();
        this.nzMessageService.success('Xóa thành công');
      }, error => {
        this.nzMessageService.info('Xóa thất bại');
      });
    } else {
      const cartItemsString = localStorage.getItem('cartItems');
      if (cartItemsString) {
        let cartItems: CartItemDto[] = JSON.parse(cartItemsString);
        const index = cartItems.findIndex(item => item.id === id);
        if (index !== -1) {
          cartItems.splice(index, 1);
          localStorage.setItem('cartItems', JSON.stringify(cartItems));
          this.loadCart();
          this.nzMessageService.success('Xóa thành công');
        } else {
          this.nzMessageService.info('Xóa thất bại: Mục không tồn tại trong giỏ hàng');
        }
      }
    }
  }

  quantityChange(idCartDetail: number, event: number): void {
    if (this.username) {
      this.publicService.updateQuantityCartItem(event, idCartDetail).subscribe(response => {
        this.loadCart();
        this.nzMessageService.success('Thay đổi số lượng thành công');
      }, error => {
        this.nzMessageService.info('Thay đổi số lượng thất bại');
      });
    } else {
      const cartItemsString = localStorage.getItem('cartItems');
      if (cartItemsString) {
        let cartItems: CartItemDto[] = JSON.parse(cartItemsString);
        const index = cartItems.findIndex(item => item.id === idCartDetail);
        if (index !== -1) {
          cartItems[index].quantity = event;
          localStorage.setItem('cartItems', JSON.stringify(cartItems));
          this.loadCart();
          this.nzMessageService.success('Thay đổi số lượng thành công');
        } else {
          this.nzMessageService.info('Thay đổi số lượng thất bại: Mục không tồn tại trong giỏ hàng');
        }
      }
    }
  }

  loadListVoucher(): void {
    this.publicService.getListVouchers().subscribe(data => {
      this.loadVouchers = data.data;
    });
  }

  createBill(): void {
    console.log(this.cartItems)
    if (this.cartItems.length > 0 && this.cartItems != null) {
      if (this.createBillForm.valid) {
        this.request.fullName = this.createBillForm.get('fullName')?.value;
        this.publicService.createBill(this.createBillForm.value).subscribe(
          (response: ResponseDto) => {

          },
          (error) => {

          }
        );
      } else {
        this.nzMessageService.error('Vui lòng điền đầy đủ thông tin!');
      }
    }
    else {
      this.nzMessageService.error('Bạn hãy thêm sản phẩm vào giỏ hàng trước khi đặt hàng');
    }

  }

  refreshNavbar() {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/cart']); // Thay đổi với route hiện tại của bạn
    });
  }
}
