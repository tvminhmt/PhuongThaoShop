using PTS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        //  IAllRepository<T> AllRepository<T>() where T : class;
        IColorRepository _colorRepository { get; }
        ICpuRepository _cpuRepository { get; }
        IContactRepository _contactRepository { get; }
        IHardDriveRepository _hardDriveRepository { get; }
        IManagePostRepository _managePostRepository { get; }
        IManufacturerRepository _manufacturerRepository { get; }
        IProductRepository _productRepository { get; }
        IProductDetailRepository _productDetailRepository { get; }
        IProductTypeRepository _productTypeRepository { get; }
        IRamRepository _ramRepository { get; }
        IScreenRepository _screenRepository { get; }
        ISerialRepository _serialRepository { get; }
        IVoucherRepository _voucherRepository { get; }
        IRoleRepository _roleRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
