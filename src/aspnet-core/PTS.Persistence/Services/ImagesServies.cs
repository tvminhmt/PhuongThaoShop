
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Data;
using PTS.Persistence.Services;

namespace PTS.Persistence.Services
{
    public class ImagesServies: IImagesServies
    {
        private readonly ApplicationDbContext _context;
        public ImagesServies(ApplicationDbContext context)
        {
                _context= context;
        }
        public async Task<int> SaveImageAsync(ImageEntity image)
        {
            _context.ImageEntity.Add(image);
            await _context.SaveChangesAsync();
            return image.Id;
        }





        //// failing...
        //public async Task<string> UploadImages(IFormFile file, string imagescode)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await file.CopyToAsync(memoryStream);
        //            byte[] fileContent = memoryStream.ToArray();

        //            var image = new Image
        //            {
        //                Id=Guid.NewGuid(),
        //                Ma = imagescode,
        //                LinkImage= file.ContentType, // Đường dẫn tương đối đến ảnh
        //                Status=1,
        //                //ContentType = file.ContentType,
        //                //FileContent = fileContent
        //            };

        //            _context.Images.Add(image);
        //            await _context.SaveChangesAsync();
        //            return "Success";
        //        }
        //    }

        //    return "Failure";
        //}      
    }
}
