
using System.Diagnostics;

namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    // Grammar for the language.
    //
    // Statement -> Statement ; Statement               (CompoundStatement)
    // Statement -> id := Expression                    (AssignStatement)
    // Statement -> print (ExpressionList)              (PrintStatement)
    // Expression -> id                                 (IdExpression)
    // Expression -> num                                (NumExpression)
    // Expression -> Expression BinOp Expression        (OpExpression)
    // Expression -> (Statement, Expression)            (SeqExpression)
    // ExpressionList -> Expression, ExpressionList     (PairExpressionList)
    // ExpressionList -> Expression                     (LastExpressionList)
    // BinOp -> +                                       (Plus)
    // BinOp -> -                                       (Minus)
    // BinOp -> x                                       (Times)
    // BinOp -> /                                       (Divide)

    // Tree-like Structure.
    //
    // ------------------------------------- [ Statements ] -------------------------------------------
    // 
    //           CompoundStatement         AssignStatement          PrintStatement
    //             /            \                 |                        |
    //        Statement      Statement        Expression            ExpressionList
    //
    //
    // ------------------------------------- [ Expresssions ] -----------------------------------------
    //
    //           OpExpression             SeqExpression         IdExpression      NumExpression
    //            /        \               /         \                |                 | 
    //      Expression  Expression    Statement  Expression        string              int
    //
    // 
    // ------------------------------------- [ ExpressionList ] ---------------------------------------
    //
    //           PairExpressionList      LastExpressionList
    //              /          \                 |
    //        Expression  ExpressionList     Expression
    //
    //-------------------------------------------------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            // Example 1:
            //
            //              CompoundStm
            //               /       \
            //          PrintStm   PrintStm
            //             /           \
            //       PairExpList     LastExpList
            //          /     \          \
            //    NumExp   LastExpList  NumExp
            //        /       /            \
            //       |     NumExp           |
            //       |       |              |
            //       1       2              3
            //
            // print(1, 2) ; print(3)
            //
            CompoundStatement example1 = new CompoundStatement(
                new PrintStatement(new PairExpressionList(new NumExpression(1), new LastExpressionList(new NumExpression(2)))),
                new PrintStatement(new LastExpressionList(new NumExpression(3))));
            Console.WriteLine(example1);

            // The idea behind the stack is as follows.
            // If we encounter a PrintStatement:
                // Pop the value on top of stack if it exists and increment it.
                // Push that value back on the stack.
                // Push a new count of 0 on top of stack.
            // If we encounter a leaf node, we increment the value on top of stack if it exists.

            int maxCount = 0;
            Traverse(example1, new Stack<int>(), ref maxCount);
            Console.WriteLine(maxCount);

            // Example 2:
            //
            //              CompoundStm
            //               /       \
            //          PrintStm   PrintStm
            //             /           \
            //       PairExpList        \_
            //         /      |            \_
            //        /       |               \
            //       /        |                \
            //      /     PairExpList       LastExpList
            //     /      /       \              \
            //    /   NumExp   LastExpList      NumExp
            //   /       |         |              |
            // NumExp    |       NumExp           |
            //  |        |         |              |
            //  1        2         3              4
            //
            // print(1, 2, 3) ; print(4)
            //
            CompoundStatement example2 = new CompoundStatement(
                new PrintStatement(new PairExpressionList(new NumExpression(1),
                                                          new PairExpressionList(new NumExpression(2), new LastExpressionList(new NumExpression(3))))),
                new PrintStatement(new LastExpressionList(new NumExpression(4))));
            Console.WriteLine(example2);

            maxCount = 0;
            Traverse(example2, new Stack<int>(), ref maxCount);
            Console.WriteLine(maxCount);


            // Example 3:
            //
            //              CompoundStm
            //               /         \
            //          PrintStm     PrintStm
            //             /             \
            //       LastExpList     LastExpList
            //           /                |
            //       SeqExp               |
            //      /        \            |
            //  PrintStm   LastExpList    |
            //     |          |           |
            // LastExpList    |           |
            //     |          |           |
            //   NumExp     NumExp      NumExp
            //     |          |           |
            //     1          2           3
            //
            // print((print(1), 2)) ; print(3)
            //
            CompoundStatement example3 = new CompoundStatement(
                new PrintStatement(new LastExpressionList(new SeqExpression(
                    new PrintStatement(new LastExpressionList(new NumExpression(1))), new NumExpression(2)))),
                new PrintStatement(new LastExpressionList(new NumExpression(3))));
            Console.WriteLine(example3);

            maxCount = 0;
            Traverse(example3, new Stack<int>(), ref maxCount);
            Console.WriteLine(maxCount);

            // Example 4:
            CompoundStatement example4 =
                new CompoundStatement(
                    new AssignStatement(
                        "a",
                        new OpExpression(
                            new NumExpression(5), 
                            BinOp.Plus, 
                            new NumExpression(3)
                        )
                    ),
                new CompoundStatement(
                    new AssignStatement(
                        "b",
                        new SeqExpression(
                            new PrintStatement(
                                new PairExpressionList(
                                    new IdExpression("a"),
                                    new LastExpressionList(
                                        new OpExpression(
                                            new IdExpression("a"),
                                            BinOp.Minus,
                                            new NumExpression(1)
                                        )
                                    )
                                )
                            ),
                            new OpExpression(
                                new NumExpression(10),
                                BinOp.Times,
                                new IdExpression("a")
                            )
                        )
                    ),
                    new PrintStatement(
                        new LastExpressionList(
                            new IdExpression("b")
                        )
                    )
                )
            );
            Console.WriteLine(example4);

            maxCount = 0;
            Traverse(example4, new Stack<int>(), ref maxCount);
            Console.WriteLine(maxCount);

            // Example 5:
            CompoundStatement example5 =
                new CompoundStatement(
                new PrintStatement(
                    new LastExpressionList(
                        new OpExpression(
                            new SeqExpression(
                                new PrintStatement(
                                    new LastExpressionList(
                                        new NumExpression(1)
                                        )
                                    ),
                                new NumExpression(2)
                                ),
                            BinOp.Plus,
                            new SeqExpression(
                                new PrintStatement(
                                    new LastExpressionList(
                                        new NumExpression(3)
                                        )
                                    ),
                                new NumExpression(4)
                                )
                            )
                        )
                    ),
                new PrintStatement(
                    new LastExpressionList(
                        new NumExpression(5)
                        )
                    )
                );

            Console.WriteLine(example5);

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey(true);
        }

        public static void Traverse(Node node, Stack<int> argCount, ref int maxCount)
        {
            if(node == null)
            {
                return;
            }

            if (node.GetType() == typeof (PrintStatement))
            {
                if (argCount.Count > 0)
                {
                    int count = argCount.Pop();
                    count++;
                    argCount.Push(count);
                }
                argCount.Push(0);
            }

            if (node.left == null && node.right == null)
            {
                // Leaf node found.
                if (argCount.Count > 0)
                {
                    // We are an argument of a print statement.
                    int count = argCount.Pop();
                    count++;
                    argCount.Push(count);
                }
            }

            Traverse(node.left, argCount, ref maxCount);
            //Console.WriteLine("[Node] {0} -> {1}", node.GetType(), node);
            Traverse(node.right, argCount, ref maxCount);

            if (node.GetType() == typeof (PrintStatement))
            {
                int count = argCount.Pop();
                if (count > maxCount)
                {
                    maxCount = count;
                }
            }
        }
    }
}
