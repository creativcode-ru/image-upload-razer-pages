using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ImageUpload.Pages.Upload
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        /*
        public async Task OnPostAsync(IFormFile image)
        {
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads", //каталог для загрузки картинки
                        image.FileName); 
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
        }
        */

        const int MAX_PHOTOS = 20;
        public async Task OnPostAsync(List<IFormFile> image)
        {
            if (image == null || !image.Any())
                throw new ArgumentNullException("Нет файлов для загрузки. Пожалуйста выберите одну или несколько фотографий.");
            else if (image.Count > MAX_PHOTOS)
                throw new ArgumentException($"Слишком много файлов. Максимально можно загрузить {MAX_PHOTOS} за один раз.");
            else
            {
                foreach (IFormFile ff in image) 
                {
                    var path = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/uploads", 
                       ff.FileName);
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ff.CopyToAsync(stream);
                    }

                }
            }
        }
       
    }
}
