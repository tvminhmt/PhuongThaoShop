using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Interfaces.Repositories
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
		//	IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
		IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
		Task<int> Save(CancellationToken cancellationToken);

		Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

		Task Rollback();
		Task ChangeTrackerClear();
	}
}
