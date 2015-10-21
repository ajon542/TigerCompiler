using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerCompiler
{
    enum BinOp
    {
        Plus, Minus, Times, Div
    } 

    class Expression
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
        public Expression left;
        public BinOp op;
        public Expression right;

        public OpExpression(Expression left, BinOp op, Expression right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} {2}", left, op, right);
            return sb.ToString();
        }
    }

    class SeqExpression : Expression
    {
        public Statement statement;
        public Expression exp;

        public SeqExpression(Statement statement, Expression exp)
        {
            this.statement = statement;
            this.exp = exp;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("({0}, {1})", statement, exp);
            return sb.ToString();
        }
    }

    class ExpressionList
    { 
    }

    class PairExpressionList : ExpressionList
    {
        public Expression head;
        public ExpressionList tail;

        public PairExpressionList(Expression head, ExpressionList tail)
        {
            this.head = head;
            this.tail = tail;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}, {1}", head, tail);
            return sb.ToString();
        }
    }

    class LastExpressionList : ExpressionList
    {
        public Expression last;

        public LastExpressionList(Expression last)
        {
            this.last = last;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", last);
            return sb.ToString();
        }
    }
}
