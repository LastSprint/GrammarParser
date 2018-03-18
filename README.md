![Travis CI](https://travis-ci.org/LastSprint/GrammarParser.svg?branch=master)
# GrammarParser

## Структура лексера

Файл, содержащий специйикаацию для парсера имеют определенную структуру.
Структурные элементы (Блоки) определяются следующимобразом:

```
block BLOCK_NAME {
...
}
```

Внутри блока описывается что-то по правилам этого самого блокаю.

### Aliases

Набор токенов, которыми можно заменить сложные правила в лексере.
Блок имеет следующдий синтаксис:

```
block Aliases {
   key1: value
   key2: value
   ...
}
```

В дальшнейшем `key` можно использовтаь в лексере.

Пример:
```
block Aliases {
   CharacterAlias = ('a'..'z')|('A'..'Z')
   StringAlias = AnyCharacter+
   AnyDigitAlias = ('0'..'9')
   AnyIntegerAlias = AnyDigit+
   ...
}

...

StringAlias|AnyIntegerAlias

```

## Правила лексера:
### Обозначения:
**r** - любое правило.
**s** - любой символ

- () - группировка
- '**s**' - обычный символ.
- **r**? - Символ либо не встречается, либо встречается, но только 1 раз.
- **r**\* - Символ либо не встречается, либо может встреится ниограниченное число раз.
- **r**+ - Символ должен встретится как минимум 1 раз.
- '**s1**'..'**s2**' - Множество символов начиная с **s1**, заканчивая **s2** (включая крайнией символы)
- **r**|**r** - Правило ИЛИ. Если выполняется первое правило, то второе проверяться не будет.

#### Примеры

- '**s**':

   'a' <- b = false  
   'a' <- a = true
  
- **r**?:

   'a'? <- b = true  
   'a'? <- a = true  
   'a'? <- = true
  
- **r**\*:

   'a'* <- b = true  
   'a'* <- a = true  
   'a'* <- aaaaaaaaaaaaaa = true  
   'a'* <- = true
  
- **r**+:

   'a'+ <- b = false  
   'a'+ <- a = true  
   'a'+ <- aaaaaaaaaaaaaa = true  
   'a'+ <- = false
  
- '**s1**'..'**s2**':

   'a'..'c' <- d = false  
   'a'..'c' <- b = true  
   'a'..'c' <- a = true  
   'a'..'c' <- c = true
  
- **r**|**r**:

   'a'|'b' <- c = false  
   'a'|'b' <- b = true  
   'a'|'b' <- a = true
