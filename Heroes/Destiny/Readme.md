# A condition macro parser

## Supported operations
 - A || B
 - A && B
 - !A
 - V > V
 - V < V
 - V >= V
 - V <= V
 - V == V
 - V != V

### Notations
 - A,B as : logic operations
 - V as :
   - (-V)
   - A int value
   - A module path
   - A mathematical operation (+-*/)

### Precautions
 -> Do not use spaces

### TODO
 - Bool Values  ('True' and 'False')
 - Bool Values  (Module)
 - Float Values (numeric and module alike)
 - Contains operations :
   - String[] module contains 'expr'
   - Int[]    module contains 'int'