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
Конечно, должно быть соответствие имен: `input name="image"` => `IFormFile image`  

## Проверки на клиенте, загрузка нескольких картинок
Хочется немного улучшить форму: загрузка на сервер должна происходить только после выбора изображения, нельзя загружать слишком большие файлы, можно загрузить сразу несколько картинок. Сама форма изменится не существенно:
```
    <form method="post" enctype="multipart/form-data" class="border-bottom pb-3">
        <input id="file_upload" multiple name="image" type="file" class="form-control-file" accept="image/*" />
        <p class="mt-2">
            <input id="btn_upload" disabled="disabled" type="submit" value="Загрузить" class="btn btn-secondary" />
            <span id="busy" style="display: none;">Идет загрузка ...</span>
        </p>
    </form>
```
Появились идентификаторы `id=` для привязки javascript, кнопка *Загрузить* изначально не активна и серая, есть тестовый указатель процесса загрузки.  
Атрибут `multiple` позволяет выбрать несколько файлов.  

Выполним проверки на клиенте, и, если все нормально, активируем кнопку загрузки.
```
@section scripts{
    <script>
        //<![CDATA[

        $('#file_upload').bind('change', function () {
            //this.files[0].size gets the size of your file.

            if (this.files.length > 10) {
                $('#btn_upload').attr("disabled", true);
                alert("Слишком много файлов.\nМаксимально можно загрузить до 10 фото, общим размером до 20МБ за один раз.");
                return false;
            }
            var sizes = 0;
            for (i = 0; i < this.files.length; i++) {
                sizes += this.files[i].size;
                if (this.files[i].size > 4096000) {
                    $('#btn_upload').attr("disabled", true);
                    alert("Размер файла " + this.files[i].name + "превышает 4МБ. Загрузка отменена.");
                    return false;
                }
            }
            if (sizes > 20480000) {
                $('#btn_upload').attr("disabled", true);
                alert("Общий размер файлов превышает 20МБ. Загрузка отменена.\nВыберите меньшее количество файлов, потом сможете загрузить остальные.");
                return false;
            }
            $('#btn_upload').removeClass('btn-secondary').attr("disabled", false).addClass('btn-primary');

        });
        $(function () {
            $('#btn_upload').click(function () {
                $(this).removeClass('btn-primary').addClass('btn-secondary').delay(1000).fadeOut(2000, function () {
                    $('#busy').show(2000); //.show("slow");
                });
                //$(this).hide();
            });
        });

     //]]>
    </script>
}
```


## Файлы с кодом
* [HTML код + javascript](https://github.com/creativcode-ru/image-upload-razer-pages/blob/master/ImageUpload/Pages/Upload/Index.cshtml)  
* [C# код](https://github.com/creativcode-ru/image-upload-razer-pages/blob/master/ImageUpload/Pages/Upload/Index.cshtml.cs)    
 
