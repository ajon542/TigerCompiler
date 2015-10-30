# Tiger Compiler

A C# implementation of a compiler for the Tiger programming language.
The implementation is based on the book "Modern Compiler Implementation in C" by Andrew W Appel.
Please see https://www.cs.princeton.edu/~appel/modern/c/ for more information.

## Current Status

- Added simple lexical analysis
- Added example top-down parsers for a simple grammar (one with backtracking and one based on the parsing table)

## TODO

### Lexical Analysis
- Add more information to each token to assist in the error checking in later phases

### Parsing
- Implement better error checking (i.e. print out line and point to error)
- Implement unit tests for the simple grammar presented
- Implement a parser for a more complex grammar