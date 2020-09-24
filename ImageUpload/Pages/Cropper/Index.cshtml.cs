using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ImageUpload.Pages.Cropper
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
        }

        public async Task OnPostAsync(string imgCropped)
        {
            byte[] bytes = Convert.FromBase64String(imgCropped.Split(',')[1]);

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads", //каталог для загрузки картинки
                        "imgCropped.png"); //пока фиксированное название
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await stream.WriteAsync(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

    }
}
