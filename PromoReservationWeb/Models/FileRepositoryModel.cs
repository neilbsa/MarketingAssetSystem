using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace PromoReservationWeb.Models
{
    public class FileRepositoryModel : IChangeTracker
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

   

        public byte[] byteContent { get; set; }
        public string contentType { get; set; }
        public decimal contentLenght { get; set; }
        public string FileName { get; set; }


   


        [NotMapped]
        public string content64base { get { return Convert.ToBase64String(byteContent); } }
        [NotMapped]
        public IFormFile ImageModel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastDateModified { get; set; }
        public string CreateUser { get; set; }
        public string LastModifiedUser { get; set; }
        public Guid TransactionId { get; set; }


        public byte[] convertFileToByte()
        {
            
            if (ImageModel != null)
            {
                byte[] value = new byte[ImageModel.Length];
                using (var mod = new MemoryStream())
                {
                   ImageModel.CopyTo(mod);
                   value = mod.ToArray();
                }

                return value;
            }
              //  await item.CopyToAsync(value);
            return null;
        }

    }

}
