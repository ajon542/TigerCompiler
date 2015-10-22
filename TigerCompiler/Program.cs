
namespace TigerCompiler
{
    using System;

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

    class Program
    {
        static void Main(string[] args)
        {
            /*CompoundStatement stm = new CompoundStatement(new AssignStatement("a",
                new OpExpression(new NumExpression(5), BinOp.Plus, new NumExpression(3))),
                new CompoundStatement(new AssignStatement("b",
                new SeqExpression(new PrintStatement(new PairExpressionList(new IdExpression("a"),
                new LastExpressionList(new OpExpression(new IdExpression("a"), BinOp.Minus,
                new NumExpression(1))))),
                new OpExpression(new NumExpression(10), BinOp.Times, new IdExpression("a")))),
                new PrintStatement(new LastExpressionList(new IdExpression("b")))));*/

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

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey(true);
        }
    }
}
