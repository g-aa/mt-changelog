# <p><img src="iconMt.png" width="64px" height="64px" align="middle"/><img src="iconLog.png" width="64px" height="64px" align="middle"/> Mt ChangeLog</p>

Приложение, предназначенное для отслеживания и регистрации изменений в firmware БМРЗ-100/120/150/160.

## Перечень технологий (зависимости):

netstandard2.1, net6.0, [Mt.Utilities](https://github.com/g-aa/mt-utilities), [Mt.Entities.Abstractions](https://github.com/g-aa/mt-entities-abstractions),
Npgsql, Npgsql.EFCore, Dapper, NUnit, Swashbuckle, MediatR, FluentValidation.

## [История изменения.](CHANGELOG.md)

## Состав проекта:

| Компонент                            | Описание                                                                                                                |
|--------------------------------------|-------------------------------------------------------------------------------------------------------------------------|
| Mt.ChangeLog.Entities                | Сущности реляционной модели данных проекта.                                                                             |
| Mt.ChangeLog.Entities.Extensions     | Методы расширения сущностей для преобразования в 'DTO'.                                                                 |
| Mt.ChangeLog.TransferObjects         | Модели объектов 'DTO'.                                                                                                  |
| Mt.ChangeLog.Context                 | Контекст доступа к данным основанный на 'Npgsql.EFCore' для реализации механизма изменяния данных.                      |
| Mt.ChangeLog.DataAccess.Abstractions | Абстракции уровня доступа к данным.                                                                                     |
| Mt.ChangeLog.DataAccess              | Уровень доступа к данным основанный на 'Npgsql', 'Dapper' и хранимых процедурах для реализации механизма чтения данных. |
| Mt.ChangeLog.Logic                   | Уровень логики приложения.                                                                                              |
| Mt.ChangeLog.WebAPI                  | Web-сервис для доступа к данным.                                                                                        |