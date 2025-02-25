# Клиент-серверный "Игровой менеджер"
Ларионов Алексей 33536\2

Приложение представляет из себя менеджер сетевых игр (преимущественно настольных). Состоит из 4 проектов:

- Head-Server - головной сервер, предполагается, что будет запущен на отдельном сетевом интерфейсе, 
который в будущем можно будет найти через DNS
Сервер совмещает в себе 3 "подсервера", соответственно: 
  - сервер БД - хранит главную базу данных и предоставляет методы для извлечения (занесения) полезных данных из нее.
  Сервер доступен только для других подсистем Head-Server'а
  - сервер аутентификации - обращается к базе данных для проверки логина/пароля, 
  которые пришли по сети с запросом на вход/регистрацию пользователя. Соответственно разрешает или запрещает вход/регистрацию
  - сервер регистрации игровых серверов - принимает запросы от игровых серверов и регистрирует их. После регистрации,
  Head-Server будет готов принимать информацию об обновлениях статуса игрового сервера (изменения игровых настроек или числа
  подключенных игроков). Также, когда клиентское приложение будет искать IP/Port игровых серверов, 
  к которым оно может подключиться, оно запросит эту информацию у головного сервера (т.к. головной сервер может быть найден через
  DNS, в то время как игровые сервера могут быть где угодно
- Game-Server - сервер игры, который реализует логику некоторый игры. После запуска сервера, чтобы быть доступным для клиентов,
сервер регистрируется в головном сервере. Игровой сервер принимает подключения игроков уже независимо от головного сервера.
Игровой сервер по сети сообщает всем игрокам об обновлениях игры (например, смена хода, смена игровых очков игрока)
- Game-Client - клиент/пользователь/игрок. Представляет из себя оконное приложение. Работа начинается с аутентификации через головной 
сервер. Затем через головной сервер запрашиваются данные (IP/Port/Другая информация) о доступных игровых серверах. Далее, при
выборе одного сервера из списка происходит подключение напрямую к серверу игры (без участия головного сервера)
Клиентское приложение может запускать на своей машине игровой сервер с некоторыми игровыми настройками. Предполагается, что
пользователь может быть подключен к нескольким игровым серверам одновременно, поэтому графический интерфейс напоминает вкладки
браузера. Интерфейс также располагает графическим представлением некоторой игры (в данным момент - игра "Кости"), в системе-backend
на игровой сервер отправляются действия игрока (напр. запрос конца хода), и принимаются события от игрового сервера 
(напр. истечение времени хода игрока)
- Common-Library - все необходимые классы для работы серверов, клиента, сетевого взаимодействия. Библиотека раззделена на Модель и 
Реализацию

# Не хватило времени на универсальное приложение
Т.к. предполагалось, что все подсистемы(сервера) могут поддерживать любую игру, то абстрагирование от специфичного функционала заняло 
у меня слишком много времени (в т.ч. графический интерфейс). Поэтому, полностью реализовано только:
- Головной сервер - поддерживает БД, сервер аутентификации, но (!)сетевое взаимодействие с игровыми серверами не реализоваоно
- Игровой сервер - может запускать любую игру, но реализована только игра "Кости", однако (!)она работает только прямым написанием
кода на сервере (т.к. сетевое взаимодействие с головным сервером и игроками недореализовано)
- Игровой клиент - успешно аутентифицируется, графический интерфейс полностью работает, но (!) не реализует сетевое взяимодействие с
игровыми серверами, поэтому играть пока не может
- Библиотека - ок

# Правила игры Кости (реализованы разные гибкие настройки, в т.ч. разное число костей, наличие кости "Джокер"):
Играют два и более игроков. Игроки по очереди совершают ход.

В свой ход игрок бросает свои кости (по умолчанию 6). Далее из выпавших костей, он должен убрать со стола хотя бы одну кость, 
так чтобы получить некоторое (ненулевое) число очков. Очки даются если убрана "комбинация":
- Одна кость "1" дает 10 очков, одна кость "5" дает 5 очков, другие кости не дают очков по одиночке
- Сет: Три кости с одинаковым значением дают очков 10*значение (исключение - три кости "1" дают 100 очков)
- Квад: Четыре кости с одинаковым значением дают очков 20*значение (исключение - четыре кости "1" дают 200 очков)
- Флеш: Пять костей с одинаковым значением дают очков 40*значение (исключение - пять костей "1" дают 350 очков = комбинация Флеш Рояль)
- Стрит: Кости "1,2,3,4,5" дают 100 очков, кости "2,3,4,5,6" дают 200 очков

Если игрок убрал со стола хотя бы 1 кость и получил очки, он может потребовать перебросить оставшиеся кости заново. Если после 
переброса игрок не может составить ни одной комбинации, все очки за этот ход у него сгорают. Игрок, получив ненулевое число
очков должен закончить ход, чтобы очки за ход прибавились к общей сумме.

Побеждает игрок, набравший целевое число очков (по умолчанию 1000)






