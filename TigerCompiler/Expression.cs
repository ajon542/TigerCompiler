
namespace TigerCompiler
{
    using System.Text;

    enum BinOp
    {
        Plus, Minus, Times, Div
    }

    class Expression : Node
    {
    }

    class IdExpression : Expression
    {
        public string id;

        public IdExpression(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return id;
        }
    }

    class NumExpression : Expression
    {
        public int num;

        public NumExpression(int num)
        {
            this.num = num;
        }

        public override string ToString()
        {
            return num.ToString();
        }
    }

    class OpExpression : Expression
    {
        public BinOp op;

        public OpExpression(Expression left, BinOp op, Expression right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} {2}", this.left, op, this.right);
            return sb.ToString();
        }
    }

    class SeqExpression : Expression
    {
        public SeqExpression(Statement statement, Expression exp)
        {
            this.left = statement;
            this.right = exp;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("({0}, {1})", this.left, this.right);
            return sb.ToString();
        }
    }

    class ExpressionList : Node
    {
    }

    class PairExpressionList : ExpressionList
    {
        public PairExpressionList(Expression head, ExpressionList tail)
        {
            this.left = head;
            this.right = tail;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}, {1}", this.left, this.right);
            return sb.ToString();
        }
    }

    class LastExpressionList : ExpressionList
    {
        public LastExpressionList(Expression last)
        {
            this.left = last;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", this.left);
            return sb.ToString();
        }
    }
}
