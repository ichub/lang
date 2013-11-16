Lang
====

A simple interpreted language written in C#
Lang is a functional programming language, created for fun.

Specification
-------------

There are 4 types:
* numbers
* strings
* functions
* booleans

Every single variable is one of those types; there is no null.

A script consists of expressions, separated by semicolons.
All expressions are sorrounded by parenthesis, and all expressions return values. The first 'item' in parenthesis is the function to be invoked, and the following items are the function parameters. In the first expression below, `2` and `3` are added, and the resulting value would be `5`.

```
  (add, 2, 3);
  (sub, 12, 4);
```

Expressions can be nested:

```
  (add, 2, (add, 3, (mul, 3, 5)));
```

New variables can be defined using the `assign` function. Valid names consist only of `([a-z]|[A-Z])+`.

```
  (assign, "one", 1);
```

You can print the value of a variable by using the `print` function:

```
  (print, False);
```

Branching can be achieved using the `ifthen` function. If the first parameter is true, then the second is returned, otherwise the 3rd is returned.

```
  (ifthen, True, 1, 2);
```

The expression above would return `1`.

A script always returns a value - the value of the last expression evaluated. For this purpose, the `return` function can be used. The `return` function is just an identity function, which returns any value which it is given. The following script would return `3`:

```
  (assign, "one", 1);
  (assign, "two", 2);
  (assign, "sum", (add, one, two));
  (return, sum);
```

Note that the `return` function does not actually stop execution. The following would still return `3`:
```
  (return, 0);
  (assign, "one", 1);
  (assign, "two", 2);
  (assign, "sum", (add, one, two));
  (return, sum);
```

New functions can be created using function literals. Function literals consist of the function parameters, and a single expression that is to be evaluated given those paramaters. For example, this function would square a number:

```
  (assign, "square", [a][(mul, a, a)]);
  (square, 10);
```

Functions can have multiple parameters, separated by the `:` character.

```
  (assign, "myMultiply", [first:second][(mul, first, second)]);
  (myMultiply, 19, 28);
```

Functions can also be recursive

```
  (assign, "factorial", [num][(ifthen, (more, num, 1), (mul, (factorial, (add, num, -1))), 1)]);
  (factorial, 10);
```

Built-in functions:
* `assign` assigns a value to a variable with the given name. If that variable doesn't exist, creates it
* `return` returns the variable that it is given
* `print` prints the value of the variable given to it to standard output
* `add` adds two numbers
* `sub` subtracts the second number from the first one
* `mul` multiplies two numbers
* `div` divides the first number by the second one
* `and`, `not`, `or`, `xor` bitwise operators on booleans
* `ifthen` returns the second parameter if true, the third if false
* `randfloat` returns a random double
* `less` returns a boolean indicating if the first number is bigger than the second
* `more` returns a boolean indicating of the first number is smaller than the second
* `equal` returns a boolean indicating if two numbers are equal

Built-in constants (can be overwritten):
* `PI` is PI

Todo
----
* Check script for validity before running it to prevent crashes
* Add escape sequences to strings
* Check for validity of variable names before assigning to them
* Fix whitespace stripping so that it doesn't remove whitespace in string literals
