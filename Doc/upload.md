[← Загрузка изображений](/README.md)  

# Простая фоорма загрузки

Простейшая форма:
```
<div class="m-3">
    <form method="post" enctype="multipart/form-data" class="border-bottom pb-3">
        <input name="image" type="file" class="form-control-file mb-2" accept="image/*" />
        <input type="submit" value="Upload" class="btn btn-primary" />
    </form>
</div>
```
Код:
``` 
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
```
