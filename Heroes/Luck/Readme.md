
# Dice Macros

## The initialisation operators create a pool of numbers
 - x __`:`__ y : returns a number between x and y.
 - x __`d`__ y : rolls x dice of y faces, returning x numbers between 1 and y.

## The filter operators compare every number with a threshold
 - __`h`__ z : keeps the z higher numbers.
 - __`l`__ z : keeps the z lower numbers.
 - __`<`__ z : keeps the numbers higher than z.
 - __`>`__ z : keeps the numbers lower than z.

## The reroll operators reroll the numbers exceding the threshold
 - __`e`__ z : Explosive Dice : reroll the dice with a result equal or higher than z and adds the result to the previous number.
 - __`a`__ z : Compound Explosive Dice : Similar to Explosive Dice, except a new dice is added to the pool instead.
 - __`r`__ z : Reroll Dice : reroll the dice for a new value.
- if z=-1 (default value) then the threshold value is the maximum value of the dice.
- if uppercase (`E`,`A`,`R`), then the operation will reiterate as long as the newer results meet the threshold criteria.
- if followed by `<`, then the operation will use a lower threshold instead of a higher threshold.
- if followed by `>`, then will not have no particular effect. It is just a explicitation of the default behavior.

## the suffixes indicate how to interpret the remaining pool
 - _`<Nothing>`_ : computes the sum by default
 - __`c`__ : returns the number of results

## Exemples

3d6>4c : the number of dice from a 3d6 pool with a result greater or equal than 4.
- [1,4,6] => 2
- [1,3,2] => 0

5d10e9 : Rolls 5d10, and then explodes every result greater or equal to 9 and computes the sum.
- [1,8,9,4,10] => [1,8,(9+5),4,(10+9)] => 46

3d4Ac : Rolls 3d4, and then rolls a new dice for every result greater or equal than 4 (default value), repeats until done, and then counts the number of dice.
- [1,4,4] => [1,4,3,4,4] => [1,4,3,4,4,2] => 6

3d10r<1 : Rolls 3d10, and then rerolls every result smaller or equal to 1 and computes the sum.
- [1,8,9] => [6,8,9] => 23

## TODO
 - {1,56,98} : draws one of these numbers. Initialisation operator.