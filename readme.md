# Tiger Compiler

A (in progress) C# implementation of a compiler for the Tiger programming language.
The implementation is based on the book "Modern Compiler Implementation in C" by Andrew W Appel.
Please see https://www.cs.princeton.edu/~appel/modern/c/ for more information.

## Current Status

- Added simple lexical analysis
- Added example top-down parsers for a simple grammar (one with backtracking and one based on the parsing table)

## TODO
During my investigation on parsing techniques over the past week, I have found I could spend a long time attempting to get a valid grammar
for my compiler. I have decided to implement a much simpler grammar such as a calculator with +, -, * and /. This will allow me to focus on the
overall compiler construction rather than getting bogged down implementing a parser for the complete Tiger programming language. That being said
once I have an overall understanding of compiler construction, I will come back and implement a more complex grammar.

### Lexical Analysis
- Add more information to each token to assist in the error checking in later phases

### Parsing
- Implement better error checking (i.e. print out line and point to error)
- Implement unit tests for the simple grammar presented
- Implement a parser for a more complex grammar such as a calculator
- Construct the abstract syntax tree for the program

### Abstract Syntax Tree
- Read chapter
- Implement AST