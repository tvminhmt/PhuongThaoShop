using Microsoft.EntityFrameworkCore;
using PTS.Data;
using PTS.Core.Repositories;
using PTS.Infrastructure.Repositories;
using PTS.Host.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTS.Domain.Repositories;

namespace PTS.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;
        public IColorRepository _colorRepository { get; }
        public ICpuRepository _cpuRepository { get; }
        public IContactRepository _contactRepository { get; }
        public IHardDriveRepository _hardDriveRepository { get; }
        public IManagePostRepository _managePostRepository { get; }
        public IManufacturerRepository _manufacturerRepository { get; }
        public IProductRepository _productRepository { get; }
        public IProductDetailRepository _productDetailRepository { get; }
        public IProductTypeRepository _productTypeRepository { get; }
        public IRamRepository _ramRepository { get; }
        public IScreenRepository _screenRepository { get; }
        public ISerialRepository _serialRepository { get; }
        public IVoucherRepository _voucherRepository { get; }
        public IRoleRepository _roleRepository { get; }
        public UnitOfWork(
        ApplicationDbContext dbContext, IColorRepository colorRepository, IProductDetailRepository productDetailRepository,
        ICpuRepository cpuRepository, IContactRepository contactRepository, IHardDriveRepository hardDriveRepository,
        IManagePostRepository managePostRepository, IManufacturerRepository manufacturerRepository, IProductRepository productRepository,
        IProductTypeRepository productTypeRepository, IRamRepository ramRepository, IScreenRepository screenRepository,
        ISerialRepository serialRepository, IVoucherRepository voucherRepository, IRoleRepository roleRepository
        )
        {
            _dbContext = dbContext;
            _colorRepository = colorRepository;
            _cpuRepository = cpuRepository;
            _contactRepository = contactRepository;
            _productDetailRepository = productDetailRepository;
            _hardDriveRepository = hardDriveRepository;
            _managePostRepository = managePostRepository;
            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _serialRepository = serialRepository;
            _voucherRepository = voucherRepository;
            _ramRepository = ramRepository;
            _screenRepository = screenRepository;
            _roleRepository = roleRepository;

        }
        //public IAllRepository<T> AllRepository<T>() where T : class
        //{
        //    return new AllRepository<T>(_dbContext);
        //}
        
        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposed = true;
            }
        }
        public async Task RollbackAsync()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _dbContext.SaveChangesAsync();

        }
    }
}
