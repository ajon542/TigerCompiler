
namespace TigerCompiler
{
    using System.Text;

    class Statement : Node
    {
    }

    class CompoundStatement : Statement
    {
        public CompoundStatement(Statement s1, Statement s2)
        {
            this.left = s1;
            this.right = s2;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} ; {1}", this.left, this.right);
            return sb.ToString();
        }
    }

    class AssignStatement : Statement
    {
        public string id;

        public AssignStatement(string id, Expression exp)
        {
            this.id = id;
            this.left = exp;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} := {1}", id, this.left);
            return sb.ToString();
        }
    }

    class PrintStatement : Statement
    {
        public PrintStatement(ExpressionList list)
        {
            this.left = list;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("print({0})", this.left);
            return sb.ToString();
        }
    }
}
