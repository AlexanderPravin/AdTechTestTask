# iConText Group Test Task


1. [**Задание.**](#01)
2. [**Структура проекта.**](#02)
3. [**Результаты работы Unit тестов.**](#03)

## 1. Задание.<a name="01"></a>

### Тестовая задача: необходимо создать консольное приложение, обрабатывающее текстовый файл, содержащий список сотрудников в формате JSON. Формат записи о сотруднике:

- столбец Id, тип в C# - int;
- столбец FirstName, тип в C# - string;
- столбец LastName, тип в C# - string;
- столбец SalaryPerHour, тип в C# - decimal.

Приложение принимает входные аргументы (в string[] args метода Main), и на их основе
выполняет соответствующую операцию.

Доступны следующие аргументы и операции:

1. -**add FirstName:John LastName:Doe Salary:100.50.**
Добавляет в файл новую запись. Поля FirstName, LastName и SalaryPerHour заполняются из аргументов (John, Doe, 100.50). Поле Id генерируется автоматически по следующему принципу: самое большое значение столбца Id, из всех имеющихся в файле, + 1.
2. **-update Id:123 FirstName:James**
Обновляет запись с Id=123, меняет в нем поле FirstName на указанное (James). Таким образом можно обновлять любые поля, кроме Id. Если не существует записи с таким Id, в консоль выводится строка, сообщающая об ошибке (текст ошибки - на усмотрение разработчика).
3. **-get Id:123**
Выводит в консоль строку формата «Id = {Id}, FirstName = {FirstName}, LastName = {LastName}, SalaryPerHour = {SalaryPerHour}», вместо {Id}, {FirstName}, {LastName},{SalaryPerHour} должны быть подставлены соответствующие поля из записи с Id=123 из файла. Если не существует записи с таким Id, в консоль выводится строка, сообщающая об ошибке (текст ошибки - на усмотрение разработчика).
4. **-delete Id:123**
Удаляет запись с Id=123 из файла. Если не существует записи с таким Id, в консоль выводится строка, сообщающая об ошибке (текст ошибки - на усмотрение разработчика).
5. **-getall**
Возвращает список всех сотрудников (формат аналогичен приведенному в описании аргумента -get).

**Дополнительные условия:**

1. Один из методов, на выбор разработчика, должен быть протестирован unit-тестами
(любой тестовый фреймворк);
2. Наибольшее внимание следует уделить качеству коду.

## 2. Стуктура проекта. <a name="02"></a>

<a href="https://github.com/AlexanderPravin/AdTechTestTask/tree/master/.src/ConsoleApp">.src/ConsoleApp</a> - Консольное приложение, выполняющая необходимый функционал;

<a href="https://github.com/AlexanderPravin/AdTechTestTask/tree/master/.src/Application">.src/Application</a> - Библиотека, содержащая Application слой приложения;

<a href="https://github.com/AlexanderPravin/AdTechTestTask/tree/master/.src/Infrastructure">.src/Infrastrucutre</a> - Библиотека, содержащая Infrastructure слой приложения;

<a href="https://github.com/AlexanderPravin/AdTechTestTask/tree/master/.src/Domain">.src/Domain</a> - Библиотека, содержащая Domain слой приложения;

<a href="https://github.com/AlexanderPravin/AdTechTestTask/tree/master/.tst/SolutionTest">.tst/SolutionTest</a> - Проект, содержащий юнит тесты для проекта;

## 3. Результаты работы Unit тестов. <a name="03"></a>

![image](https://github.com/user-attachments/assets/20b35cbb-4cb8-44fc-a503-022828e7edc6)
