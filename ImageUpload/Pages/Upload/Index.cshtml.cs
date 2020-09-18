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
    }
}
