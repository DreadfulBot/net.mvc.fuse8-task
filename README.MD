##Текст задания###

Необходимо создать веб-приложение ASP.NET MVC (желательно на 5 или .Core) на C#, которое подсоединяется к базе данных и строит отчёт (в Excel-файл, также не лишним будет отображение на странице), после чего отправляет его на e-mail.
 
Отчёт по продажам должен строиться за определенный период (предусмотреть возможность самому задавать временной диапазон) и содержать следующую информацию: 
номер заказа, дата заказа, артикул товара, название товара, количество реализованных единиц товара, цена реализации за единицу продукции. 
 
Excel-файл должен содержать вычисляемый столбец (столбец с формулой), который для каждой позиции должен считать стоимость.
 
Данные для отчёта находятся в базе данных Northwind (она присоединена в виде архивированного bak-файла, нужно ее восстановить на SQL Server).

##Notes##
`/Scripts/Core.js` - весь мой javascript-code

`HomeController` - при запуске, по-умолчанию отображаются последние 100 заказов в порядке убывания по `OrderDate` (вызывается метод `GetDefaultResult()`)

При указании временных интервалов через datepikcer'ы на странице и нажатии на кнопку "Отправить" вызывается метод `GetIntervalResult(string startDate, string endDate)` и, если все указано верно, то отображаются новые результаты.

В сессии было решено хранить только временные промежутки, которые устанавливаются при вызове функции `GetIntervalResult` из пункта выше:

```
HomeController.cs - line 25-26

Session["startDateTxt"] = startDate;
Session["endDateTxt"] = endDate;	
```

При нажатии на кнопку "Отправить отчет по почте" формируется EXCEL-файл `report.xls` в папке A`pp_Data` и отправляется по почте (вычисляемый столбец не работал при просмотре через Яндекс.Документы, но в локальном Excel все ОК).

Для отправки письма используются данные, указанные в форме с полями с префиксом `smtp_` (тестировалась отправка с gmail на yandex - результат положительный).

IDE - Visual Studio Enterprise 2015 v 14.0.25431.01 Update 3