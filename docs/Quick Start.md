# Быстрый старт с <u>CodeAgen</u>
## Подготовка
Для любой генерации года нам понадобится объект с интерфейсом ```ICodeOutput```, который будет служить кэшем для всего собираемого кода. В библиотеке уже существует ```StandardCodeOutput```, но вы, при желании, можете создать свой. <p>
```c#
ICodeOutput output = new StandardCodeOutput();
```
## Первый билд
Создадим один объектов и забилдим его:
```c#
var code = new CodeRawString("Hello, World!");
code.Build(output);

var result = output.ToString();
```
Итогом исполнения этого кода станет строка ```Hello, World!``` в переменной ```result```.<p>
Типы ```CodeRawString``` и ```CodeRawChar``` являются самыми базовыми типами в библиотеке и используются для хранения ```string``` и ```char``` соответственно. Также у них существуют перегруженные операции преобразования типов, так что код ниже легален и будет выполняться:
```c#
CodeRawString @string = "Hello, World!";
CodeRawChar @char = '1';
```
## Код класса
Приступим к чему-то более сложному и увлекательному. Немного изменим приведенный выше код и попробуем запустить его:
```c#
ICodeOutput output = new StandardCodeOutput();

var code = new CodeClass("Hello, World!");
code.Build(output);

var result = output.ToString();
```
Результатом будет приведенный ниже эксепшен:
```c#
CodeAgen.Exceptions.CodeNamingException: Invalid variable name: Hello, World!
```
В библиотеке существует валидация имен сущностей, что позволяет избежать неправильной кодогенерации на ранних этапах. Сейчас мы получили исключение из-за наличия в строке имени специальных символов и пробела. Давайте изменим его так, чтобы название больше подходило для класса:
```c#
var code = new CodeClass("ExampleClass");
```
И, после выполнения исправленного кода мы получим результат:
```c#
public class ExampleClass
{
}
```