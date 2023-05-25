# Задания по курсу CShape

## Reflection

### Основное

Реализацию по вступительному заданию разделить на 2 сборки: `exe` и `dll`.

- `Exe` читает файл, вызывает *приватный метод* из `dll`, передает ему *текст из файла*, *получает результат* и *записывает его в файл*.
- `Dll` содержит *1 класс* и *приватный метод*, который принимает на вход *текст*, возвращает `Dictionary<string, int>`

### Дополнительное

При помощи **Рефлексии/IL** вставок или любого друго механизма *создать экземпляр* **абстрактного класса**. Если пойдете по пути рефлексии рекомендую заглянуть на [Reflaction](https://referencesource.microsoft.com/)

## Процессы и потоки

В `dll` предыдущего задания реализовать публичный метод аналогичный приватному, но с многопоточной обработкой текста. Сравнить время выполнения приватного и публичного методов при помощи объекта `StopWach`. Вместе с кодом прислать время выполнения методов

## Веб-сервисы

На основе публичного метода из задания *"Процессы и потоки"* реализовать `WCF` или `WebAPI` сервис, который на **вход** принимает *текст*, **возвращает** `Dictionary<string, int>`. Вместе с кодом сервиса должен присутствовать код приложения его вызывающий.

Отправляйте ваши решения на почту **Dev_school@digdes.com**. В теле письма *<Фамилия> <Имя>. <Имя блока>*. Наример: "Микаелян Андрей. Reflection" или "Микаелян Андрей. Итоговое". В теле письма ссылка на github или архив с кодом, также любые ваши комментарии.
