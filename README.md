Lang
====

A simple interpreted language written in C#
Lang is a functional programming language, created for fun. It is just an experiment

Specification
-------------

In Lang, there are 4 types:
* numbers
* strings
* functions
* booleans

Every single variable is one of those types; there is no null.

A program consists of expressions, separated by semicolons. The value that a program returns is the value to which the final statement is evaluated. Expressions take the following form:

```
  (add, 2, 3);
  (sub, 12, 4);
```

All expressions are sorrounded by parenthesis, and all expressions return values. The first 'item' in parenthesis is the function to be invoked, and the following items are the function parameters. In the first expression above, `2` and `3` are added, and the resulting value would be `5`.

Expressions can be nested:

```
  (add, 2, (add, 3, 4));
```

New variables can be defined using the `assign` function. Valid names consist only of ([a-z]|[A-Z])+

```
  (assign, "one", 1);
```

Branching can be achieved using the `ifthen` function. If the first parameter is true, then the second is returned, otherwise the 3rd is returned.

```
  (ifthen, true, 1, 2)
```

The expression above would return `1`.

A Lang script always returns a value - the value of the last expression evaluated. For this purpose, the `return` function can be used. The return function is just an identity function, which returns any value which it is given. The following script would return `3`:

```
  (assign, "one", 1);
  (assign, "two", 2);
  (assign, "sum", (add, one, two));
  (return, sum);
```

