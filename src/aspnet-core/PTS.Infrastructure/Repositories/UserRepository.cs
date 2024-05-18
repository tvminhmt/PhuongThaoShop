
using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Domain.Entities;
using PTS.Data;

namespace PTS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; 
        public UserRepository(ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<bool> Create(UserEntity obj)
        {
            var _user = await _context.UserEntity.AnyAsync(user =>user.Username == obj.Username); //x => x.UserName == obj.UserName tìm đối tượng có cùng tên đăng nhập
            if (obj == null || _user == true) // nếu đối tượng tồn tại hoặc giá trị truyền vào rỗng thì trả về false.
            {
                return false;
            }
            try
            {
                await _context.UserEntity.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            // tạo 1 biến _deleteUser và gán cho nó kết quả của đoạn mã lấy 1 bản ghi
            // từ bảng cơ sở dữ liệu"_context.Users" với phương thức FindAsync bằng khóa chính Id
            var _deleteUser = await _context.UserEntity.FindAsync(Id);
            if (_deleteUser == null)
            {
                return false;
            }
            try
            {
                _deleteUser.Status = 0;
                _context.UserEntity.Update(_deleteUser);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
             return await _context.UserEntity.Where(x=>!x.IsDeleted).ToListAsync();
        }

        public async Task<bool> Update(UserEntity obj)
        {
            // tạo 1 biến _update và gán cho nó kết quả của đoạn mã lấy 1 bản ghi
            // từ bảng cơ sở dữ liệu"_context.Users" với phương thức FindAsync bằng khóa chính obj.Id
            var _update = await _context.UserEntity.FindAsync(obj.Id);
            if (_update == null) // Nếu đối tượng muốn tìm rỗng thì trả về null
            {
                return false;
            }
            try
            {
                // _update.Password = obj.Password;
                _update.FullName = obj.FullName;
                _update.Address = obj.Address;
                _update.PhoneNumber = obj.PhoneNumber;
                _update.Status = obj.Status;
                _context.UserEntity.Update(_update);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var result = await _context.UserEntity.FindAsync(id);
            return result;
        } 
        public async Task<UserEntity> GetUserByUsername(string username)
        {
            return await _context.UserEntity.Where(u=>!u.IsDeleted).Include(u => u.RoleEntities).FirstOrDefaultAsync(x => x.Username == username);
        }

    }
}
