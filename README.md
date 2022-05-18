# InteractiveHelper
## TODO-list
- Дополнить API каталога возможностью поиска и фильтрации. Систему заказов тоже было бы неплохо реализовать.
- Обдумать получше структуру хранения данных опросника, возможно применить древовидную структуру хранения вопросов и результатов (как, например, [здесь](https://habr.com/ru/post/516596/)), чтобы можно было сразу фильтровать набор товаров согласно уже пройденным вопросам (полученным ответам). Да и хранить ссылку на следующий вопрос в предыдущем кажется мне куда более правильным решением, чем просто хранить порядковые номера. Плюс, это пригодится при автоматической генерации результатов (на текущий момент в проекте это нереализованный метод, просто выбрасывающий исключение).
- Согласно новой структуре сделать новый админ-API для создания опросов и API прохождения опросов. Возможно, выделить эти API в отдельный проект. Кстати, насколько это хорошее решение, не означает ли разделение API по проектам необходимость назначать им разные доменные имена (если мы представим, что это дошло до продакшна)?
- Заменить Scope-авторизацию на авторизацию по ролям, либо дополнить одно другим. Это логичнее, чтобы за опросы отвечал один человек, за каталог другой, а за заказы третий.
- Разобраться, почему в интеграционных тестах (их в проекте на данный момент всего 2) при выполнении get-запроса к тестовому серверу возвращается 404 код, как будто контроллеры не были зарегистрированы в ApiStartup. Будет здорово, если проверяющий взглянет на это, может я что-то потерял. После чего развернуть полноценные нормальные тесты.
- Написать клиентское приложение для каждого из API:
  - Для API каталога это интернет-магазин. Главная страница, возможность упорядочивания товаров, их фильтрация по категориям, брендам и характеристикам; Страница товара с разделом "характеристики"; Админ-панель каталога; Админ-панель заказов.
  - Для API опросника - Админ-панель, она же конструктор этого самого опросника; Проект-одностраничник, где будет непосредственно прохождение этого теста и получение результата. После получения результата должна быть возможность добавить все товары в корзину/сделать заказ.
