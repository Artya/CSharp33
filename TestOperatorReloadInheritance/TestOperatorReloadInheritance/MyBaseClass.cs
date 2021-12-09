namespace TestOperatorReloadInheritance
{
    public class MyBaseClass
    {
        private string name;

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public MyBaseClass(string name)
        {
            Name = name;
        }

        public static MyBaseClass operator + (MyBaseClass leftOperand, MyBaseClass rightOperand)
        {
            return new MyBaseClass(leftOperand.Name + " + " + rightOperand.Name);
        }      
    }
}
