using System;
using System.ComponentModel.DataAnnotations;

namespace PTS.Base.Application.Dto
{
    public class FileDto
    {
        [Required]
        public string FileName { get; set; }

        public string FileType { get; set; }

        [Required]
        public string FileToken { get; set; }
        public byte[] FileBytes { get; set; }
        public string FileBase64 { get; set; }
        public bool IsSuccess { get; set; }

        public FileDto()
        {

        }

        public FileDto(string fileName, string fileType, bool isSetFileName = false, bool isSuccess = true)
        {
            FileName = fileName;
            FileType = fileType;
            FileToken = isSetFileName ? fileName : Guid.NewGuid().ToString();
            IsSuccess = isSuccess;
        }
    }
}