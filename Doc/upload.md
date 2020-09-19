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
`accept="image/*"` -- атрибут accept не проверяет типы выбранных файлов;  
 Он просто предоставляет браузеру подсказки, которые помогут пользователям выбрать правильные типы файлов. 
 Пользователи по-прежнему могут (в большинстве случаев) переключить параметр в селекторе файлов,
 который позволяет отменить это и выбрать любой файл, который они хотят, а затем выбрать неправильные типы файлов.  
 По этой причине вы должны убедиться, что ожидаемые требования подтверждены на стороне сервера.
 ◻ [developer.mozilla: Attributes accept](https://developer.mozilla.org/en-US/docs/Web/HTML/Attributes/accept)  
 
 `enctype="multipart/form-data"` -- кодировка данных формы, которая позволяет передавать двоичные данные.  
 
`class="m-3"` -- используются стандартные Bootstrap классы

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


◻ [JQuery Image Upload with Razor Pages](https://www.codeproject.com/Articles/1223613/JQuery-Image-Upload-with-Razor-Pages) 
