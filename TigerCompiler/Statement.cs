
namespace TigerCompiler
{
    using System.Text;

    class Statement
    {
    }

    class CompoundStatement : Statement
    {
        public Statement s1;
        public Statement s2;

        public CompoundStatement(Statement s1, Statement s2)
        {
            this.s1 = s1;
            this.s2 = s2;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} ; {1}", s1, s2);
            return sb.ToString();
        }
    }

    class AssignStatement : Statement
    {
        public string id;
        public Expression exp;

        public AssignStatement(string id, Expression exp)
        {
            this.id = id;
            this.exp = exp;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} := {1}", id, exp);
            return sb.ToString();
        }
    }

    class PrintStatement : Statement
    {
        public ExpressionList list;

        public PrintStatement(ExpressionList list)
        {
            this.list = list;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("print({0})", list);
            return sb.ToString();
        }
    }
}
