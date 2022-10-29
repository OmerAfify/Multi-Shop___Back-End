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
            }

            return ImgName;

        }
        
        
        public static bool DeleteImage(string path)
        {
            try {

        
             File.Delete(path);
            
             return true;
                

            }
            catch(Exception ex)
            { 
                return false;
            }

        }


        public static bool MoveImage(string src, string dest)
        {
            try {

                File.Move(src, dest);
                return true;
                }
            catch(Exception ex)
            {
                return false;
            }    
        }

    
        public static void GetImagesNames()
        {

            string[] files = Directory.GetFiles(  Path.Combine(Directory.GetCurrentDirectory(), @"wwwRoot\Images\ProductsImages" ));
            foreach (string file in files)
                Console.WriteLine(Path.GetDirectoryName(file));

        }




    }
}
