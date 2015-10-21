
namespace TigerCompiler
{
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
            CompoundStatement stm = new CompoundStatement(new AssignStatement("a",
                new OpExpression(new NumExpression(5), BinOp.Plus, new NumExpression(3))),
                new CompoundStatement(new AssignStatement("b",
                new SeqExpression(new PrintStatement(new PairExpressionList(new IdExpression("a"),
                new LastExpressionList(new OpExpression(new IdExpression("a"), BinOp.Minus,
                new NumExpression(1))))),
                new OpExpression(new NumExpression(10), BinOp.Times, new IdExpression("a")))),
                new PrintStatement(new LastExpressionList(new IdExpression("b")))));

            for (; ; ) { }
        }
    }
}
