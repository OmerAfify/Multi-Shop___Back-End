using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShopWebAPIs.Helpers
{
    public static class ImageUploadingHelper
    {

        public static async Task<string> UploadImage(IFormFile imageUploaded , string pathToAddImgIn )
        {
            string ImgName = Guid.NewGuid().ToString() + ".jpg";

            var path = Path.Combine(pathToAddImgIn,ImgName);
            
            using (FileStream stream = File.Create(path))
            {
                await imageUploaded.CopyToAsync(stream);
                stream.Close();

                return ImgName;
            }

      
        }
        
        
        
        public static async Task UploadImage(List<IFormFile> imagesUploaded , string pathToAddImgIn )
        {
           
            foreach(var image in imagesUploaded) { 
            
            string ImgName = Guid.NewGuid().ToString() + "jpg";

            var path = Path.Combine(pathToAddImgIn,ImgName);
            
            using (FileStream stream = File.Create(path))
            {
                await image.CopyToAsync(stream);
                stream.Close();
            }


            }

        }



    }
}
