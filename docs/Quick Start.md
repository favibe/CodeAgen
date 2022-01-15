# Быстрый старт с <u>CodeAgen</u>

## Подготовка
Для любой генерации года нам понадобится объект с интерфейсом ```ICodeOutput```, который будет служить кэшем для всего собираемого кода. В библиотеке уже существует ```StandardCodeOutput```, но вы, при желании, можете создать свой. <p>
> Весь код покрыт тестами, реализованными под стандартные средства библиотеки. Это также стоит учитывать при разработке собственных решений.

Для этого гайда мы воспользуемся стандартным объектом:
```c#
ICodeOutput output = new StandardCodeOutput();
```
## Первый билд
Создадим объект типа ```CodeRawString``` и забилдим его с помощью метода ```Build```, принимающего в качестве аргумента объект типа ```ICodeOutput```:
```c#
var code = new CodeRawString("Hello, World!");
code.Build(output);

var result = output.ToString();
```
Результатом выполнения этого кода станет строка ```Hello, World!``` в переменной ```result```.<p>
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
В библиотеке существует валидация имен сущностей, что позволяет избежать неправильной кодогенерации на ранних этапах. Сейчас мы получили исключение из-за наличия в строке имени специальных символов и пробела. 

>Несмотря на наличие механизмов валидации генерируемого кода, она осуществляется на довольно базовом уровне, поэтому всегда внимательно относитесь к тому, что вы хотите использовать в качестве кода.

Давайте изменим его так, чтобы название больше подходило для класса:
```c#
var code = new CodeClass("ExampleClass");
```
И, после выполнения исправленного кода, мы получим результат:
```c#
public class ExampleClass
{
}
```
### Модификатор доступа
Публичные классы - это хорошо, но сокрытие типов еще никто не отменял. Модификаторы доступа хранятся в классe ```CodeAccessModifier``` и доступ к ним происходит только через заранее предсозданные объекты с соответствующими именами. Давайте изменим модификатор доступа для класса:

```c#
var code = new CodeClass("ExampleClass", CodeAccessModifier.Internal);
```
Результат:
```c#
internal class ExampleClass
{
}
```
### Комментарий
Также, неплохо было бы иметь возможность документировать код. Для создания комментария можно воспользоваться классом ```CodeComment```.
```c#
var codeComment = new CodeComment("Какой-то комментарий");
codeComment.Build(output);

var result = output.ToString();
```
Результат:
```c#
// Какой-то комментарий
```

У класса возможность оставить поясняющий его комментарий уже внедрена в конструктор, используем её:
```c#
var code = new CodeClass("ExampleClass", CodeAccessModifier.Internal, new CodeComment("Мой первый класс!"));
```
Результат:
```c#
// Мой первый класс!
internal class ExampleClass
{
}
```
### Члены класса

#### Поля

После генерации пустого класса пиступим к его начинке. Для примера, заведем пару полей:
```c#
code.Field(new CodeField("float", "_index", "5f", CodeAccessModifier.Private, true))
    .Field(new CodeField("string", "_name", null, CodeAccessModifier.Private, true));
```
Несмотря на то, что такой код выглядит громоздким и нагруженным, в результате мы получим лучшую производительность при генерации за счёт внутренних оптимизаций и преимущества валидации.

---
Тем не менее, существует альтернативный вариант осуществления того же действия:
```c#
code.AddUnit(new CodeLine("private readonly float _index;"));
code.AddUnit(new CodeLine("private readonly float _name;"));
```

---
После добавления полей полученный код будет иметь вид:

```c#
// Мой первый класс!
internal class ExampleClass
{
	private readonly float _index = 5f;
	private readonly string _name;
}
```
#### Конструктор

Теперь добавим конструктор:

```c#
code.Constructor(CodeConstructor.CreateFor(code, CodeAccessModifier.Internal)
    .AddParameter(new CodeMethodParameter("float", "index"))
    .AddParameter(new CodeMethodParameter("string", "name"))
);
```

Результат выполнения:

```c#
// Мой первый класс!
internal class ExampleClass
{
	private readonly float _index = 5f;
	private readonly string _name;
	internal ExampleClass(float index, string name)
	{
	}
}
```

Здесь определенно пригодился бы пропуск строки. Между добавлением полей и конструктора добавим новую строку:
```c#
code.Space();
```
Результат:

```c#
// Мой первый класс!
internal class ExampleClass
{
	private readonly float _index = 5f;
	private readonly string _name;
	
	internal ExampleClass(float index, string name)
	{
	}
}
```
Теперь не помешало бы добавить в конструктор пару строк кода. Немного изменим приведенный выше код для добавления конструктора:

```c#
var constructor = CodeConstructor.CreateFor(code, CodeAccessModifier.Internal)
    .AddParameter(new CodeMethodParameter("float", "index"))
    .AddParameter(new CodeMethodParameter("string", "name"));

constructor
    .AddUnit(new CodeLine("_index = index;"))
    .AddUnit(new CodeLine("_name = name;"));

code.Constructor(constructor);
```
Результат:
```c#
// Мой первый класс!
internal class ExampleClass
{
	private readonly float _index = 5f;
	private readonly string _name;
	
	internal ExampleClass(float index, string name)
	{
		_index = index;
		_name = name;
	}
}
```
#### Методы

Теперь создадим два простых метода и добавим их в класс:

```c#
var methodA = new CodeMethod("MethodA", CodeType.Void, CodeAccessModifier.Internal);
var methodB = new CodeMethod("MethodB", CodeType.Void, CodeAccessModifier.Internal);

code.Space().Method(methodA)
    .Space().Method(methodB);
```

В результате получим:

```c#
// Мой первый класс!
internal class ExampleClass
{
	private readonly float _index = 5f;
	private readonly string _name;
	
	internal ExampleClass(float index, string name)
	{
		_index = index;
		_name = name;
	}
	
	internal void MethodA()
	{
	}
	
	internal void MethodB()
	{
	}
}
```

Теперь создадим код, одинаковый для обоих методов. Для этого используем `CodeFragment` и `CodeLoopWhile`

```c#
var commonCode = new CodeFragment();

commonCode
    .AddUnit(new CodeLine("var i = 0;"))
    .AddUnit(CodeLine.Empty)
    .AddUnit(new CodeLoopWhile("i < 10")
        .AddUnit(new CodeLine("Console.WriteLine(i);"))
        .AddUnit(new CodeLine("i++;"))
    );

methodA.AddUnit(commonCode);
methodB.AddUnit(commonCode);
```

Результат:

```c#
// Мой первый класс!
internal class ExampleClass
{
	private readonly float _index = 5f;
	private readonly string _name;
	
	internal ExampleClass(float index, string name)
	{
		_index = index;
		_name = name;
	}
	
	internal void MethodA()
	{
		var i = 0;
	
		while(i < 10)
		{
			Console.WriteLine(i);
			i++;
		}
	}
	
	internal void MethodB()
	{
		var i = 0;
	
		while(i < 10)
		{
			Console.WriteLine(i);
			i++;
		}
	}
}
```
Но, допустим, что нам нужно, чтобы в методе `MethodA` фрагмент `commonCode` выполнялся только при выполнении условия. Немного изменим добавление фрагмента в метод:
```c#
methodA.AddUnit(new CodeIfElse("_index == 0", commonCode));
```

Теперь `MethodA` будет выглядеть так:
```c#
internal void MethodA()
{
    if (_index == 0)
    {
        var i = 0;

        while(i < 10)
        {
            Console.WriteLine(i);
            i++;
        }
    }
}
```

Также, допустим, что я хочу, чтобы `MethodA` возвращал `float`. Изменим его объявление и добавление в него кода:

```c#
var methodA = new CodeMethod("MethodA", "float", CodeAccessModifier.Internal);
```

```c#
methodA
    .AddUnit(new CodeIfElse("_index == 0", 
        new CodeFragment()
            .AddUnit(commonCode)
            .AddUnit(CodeLine.Empty)
            .AddUnit(CodeMethod.Return("3.5f * 2"))
        ))
    .AddUnit(CodeLine.Empty)
    .AddUnit(CodeMethod.Return("12f"));
```
Результат:

```c#
internal float MethodA()
{
    if (_index == 0)
    {
        var i = 0;
    
        while(i < 10)
        {
            Console.WriteLine(i);
            i++;
        }
    
        return 3.5f * 2;
    }
    
    return 12f;
}
```
### Итоговый результат:

После проведенных процедур наш код для генерации теперь выглядит так:

```c#
ICodeOutput output = new StandardCodeOutput();

var code = new CodeClass("ExampleClass", CodeAccessModifier.Internal, new CodeComment("Мой первый класс!"));

code.Field(new CodeField("float", "_index", "5f", CodeAccessModifier.Private, true))
    .Field(new CodeField("string", "_name", null, CodeAccessModifier.Private, true));

code.Space();

var constructor = CodeConstructor.CreateFor(code, CodeAccessModifier.Internal)
    .AddParameter(new CodeMethodParameter("float", "index"))
    .AddParameter(new CodeMethodParameter("string", "name"));

constructor
    .AddUnit(new CodeLine("_index = index;"))
    .AddUnit(new CodeLine("_name = name;"));

code.Constructor(constructor);

var methodA = new CodeMethod("MethodA", "float", CodeAccessModifier.Internal);
var methodB = new CodeMethod("MethodB", CodeType.Void, CodeAccessModifier.Internal);

code.Space().Method(methodA)
    .Space().Method(methodB);

var commonCode = new CodeFragment();

commonCode
    .AddUnit(new CodeLine("var i = 0;"))
    .AddUnit(CodeLine.Empty)
    .AddUnit(new CodeLoopWhile("i < 10")
        .AddUnit(new CodeLine("Console.WriteLine(i);"))
        .AddUnit(new CodeLine("i++;"))
    );

methodA
    .AddUnit(new CodeIfElse("_index == 0", 
        new CodeFragment()
            .AddUnit(commonCode)
            .AddUnit(CodeLine.Empty)
            .AddUnit(CodeMethod.Return("3.5f * 2"))
        ))
    .AddUnit(CodeLine.Empty)
    .AddUnit(CodeMethod.Return("12f"));

methodB.AddUnit(commonCode);

code.Build(output);

var result = output.ToString();
```

Хоть он и может показаться большим по сравнению с результатом, мы подразумеваем, что эти инструменты не будут использоваться для ручного написания кода, и скорее предназначены для реализации генерации кода в рантайме.

Код, полученный в результате:

```c#
// Мой первый класс!
internal class ExampleClass
{
	private readonly float _index = 5f;
	private readonly string _name;
	
	internal ExampleClass(float index, string name)
	{
		_index = index;
		_name = name;
	}
	
	internal float MethodA()
	{
		if (_index == 0)
		{
			var i = 0;
		
			while(i < 10)
			{
				Console.WriteLine(i);
				i++;
			}
		
			return 3.5f * 2;
		}
		
		return 12f;
	}
	
	internal void MethodB()
	{
		var i = 0;
	
		while(i < 10)
		{
			Console.WriteLine(i);
			i++;
		}
	}
}
```