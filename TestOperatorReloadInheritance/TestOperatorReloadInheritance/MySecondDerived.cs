namespace TestOperatorReloadInheritance
{
    public class MySecondDerived : MyBaseClass
    {
        public MySecondDerived(string name) : base(name)
        { 
        
        }

        public static MySecondDerived operator +(MySecondDerived left, MySecondDerived right)
        {
            return new MySecondDerived(left.Name + " ++ " + right.Name); ;
        }
    }
}