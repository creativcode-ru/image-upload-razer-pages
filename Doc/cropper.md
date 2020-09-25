[← Загрузка изображений](/README.md)  

# Библиотка Cropper.js
📘 [Сайт Cropper.js](https://fengyuanchen.github.io/cropperjs/)   
📘 [Github Cropper.js](https://github.com/fengyuanchen/cropperjs)  
♻ [CDN ](https://cdnjs.com/libraries/cropperjs)  
📘 [Github jquery-cropper](https://github.com/fengyuanchen/jquery-cropper)   

Для загрузки библиотеки в Visual Studio 2019 используйте [Использование LibMan с ASP.NET Core в Visual Studio](https://docs.microsoft.com/ru-ru/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-3.1). Для проекта откройте окно LibMan (Добавить/Клиентская библиотека) В поле ввода "Библиотека" введтите `cropperjs` и нажмите ввод - автоматически найдется библиотека с последней актуальной версией.

## Простейшее применение
В заголовке страницы присоедините стиль, это надо для прорисовки маркеров, затемнения фона и т.д.:
```
@section PageCss{
    <link href="~/lib/cropper/cropperjs/cropper.css" rel="stylesheet">
}
```
При этом в разметке *Shared/_Layout.cshtml* есть соответсвующий вызов данной секции:
```
<head>
    <meta charset="utf-8" />
    ...
    @RenderSection("PageCss", required: false)
</head>
```
* **Без отправки на сервер:**  
Разметка html для отображения вырезанного изображения не содержит тега `form` -- всё делается на клиенте:
```
<div class="m-3">
    <!-- выбираем исходную картинку -->
    <input id="img_select" type="file" class="form-control-file" accept="image/*" />
    <button id="btn_crop">Обрезать</button>

    <!-- отображаем исходную картинку с маркерами обрезки -->
    <div id="img_bg" class="m-3"></div>

    <!-- отображаем обрезанную картинку -->
    <div class="m-3">
        <img id="img_cropped"  src="" alt="">
    </div>

    <!-- форма отправки данных на сервер -->
    @* здесь добавим форму позже *@
</div>
```
Скрипт для обработки изображения сначала включает ссылку на библиотеку cropper.js, затем её использование:
```
@section scripts{
    <script src="~/lib/cropper/cropperjs/cropper.js"></script>

    <script>
        //<![CDATA[

        let
            upload = document.querySelector('#img_select'), //поле выбора исходной картинки
            result = document.querySelector('#img_bg'), //контейер для отображения исходной картинки
            save = document.querySelector('#btn_crop'), //кнопка обрезки
            cropped = document.querySelector('#img_cropped'),//обрезанная картинка
            //data = здесь добавим поле для отправки данных на срвер
            cropper = '';

        //отоброзить фоновую картинку для обрезки после её выбора
        upload.addEventListener('change', (e) => {
            if (e.target.files.length) {
                //чтение файла картинки
                const reader = new FileReader();
                reader.onload = (e) => {
                    if (e.target.result) {
                        //создать фоновую картинку 
                        let img = document.createElement('img');
                        img.id = 'image';
                        img.src = e.target.result
                        //очистить контейнер, если ранее уже выбирали картинку
                        result.innerHTML = '';
                        //отобразить фоновую картинку
                        result.appendChild(img);
                        
                        //применить для фоновой картинки cropper -- появятся метки обрезки
                        cropper = new Cropper(img);
                    }
                };
                reader.readAsDataURL(e.target.files[0]);
            }
        });

        //обрезать картинку
        save.addEventListener('click', (e) => {
            e.preventDefault();
            //результат обрезки
            let imgSrc = cropper.getCroppedCanvas({}).toDataURL();
            cropped.src = imgSrc;
            //data -- здесь будет отправка данных на сервер
        });

     //]]>
    </script>
}
```
Скрипт при выборе картики отображает её в качестве фона в контейнере, и накладывает на неё рамку для обрезки изображения. Кодга нажимаем кнопку *Обрезать*, cropper отображает результат обрезки.  
Теперь надо передать обрезанную картинку на сервер.

* **Отправк на сервер:** 
Для отправки картинки на сервер используем самый простой способ: добавляем в разметку форму, в которой есть скрытое поле. А в это скрытое поле запишем данные которые и представляют собой картинку.  

Добавляем форму:
```
<!-- форма отправки данных на сервер -->
    <form method="post" enctype="multipart/form-data" class="border-bottom pb-3">
        <input type="hidden" name="imgCropped" id="imgCropped" />
        <input id="btn_upload" type="submit" value="Загрузить" />
    </form>
```
Тут только скрытое поле для данных, и кнопка отправки на сервер.  

Добавляем пару строк в скрипт:  
Вместо строки `//data = здесь добавим поле для отправки данных на срвер` добавляем:  
`data = document.querySelector('#imgCropped'),//двоичная кодировка обрезанной картинки`  
А вместо строки `//data -- здесь будет отправка данных на сервер` добавляем:  
```
data.value = imgSrc; //помещаем картинку в скрытое поле формы 
```
Теперь, после нажатия кнопки *Обрезать*, кроме отображения результата обрезки, мы также заполняем скрытое поле формы двоичными данными для картинки. Это можно увидеть прям в броузере Хром, в режиме разработчика(F12), поле 
