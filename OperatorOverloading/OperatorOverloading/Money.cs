namespace OperatorOverloading
{
    public class Money
    {
        public int Amount { get; set; }
        public CurrencyType CurrencyType { get; }

        public Money(int amount, CurrencyType currencyType)
        {
            this.Amount = amount;
            this.CurrencyType = currencyType;
        }

        public static implicit operator double(Money moneyInstance)
        {
            return (double)moneyInstance.Amount;
        }
        public static explicit operator string(Money moneyInstance)
        {
            return moneyInstance.Amount.ToString();
        }
        public static implicit operator Money(double amount)
        {
            return new Money((int)amount, CurrencyType.UAN);
        }
        public static explicit operator Money(string amount)
        {
            return new Money(int.Parse(amount), CurrencyType.UAN);
        }

        public static Money operator ++(Money moneyIstance)
        {
            moneyIstance.Amount++;
            return moneyIstance;
        }
        public static Money operator --(Money moneyIstance)
        {
            moneyIstance.Amount--;
            return moneyIstance;
        }

        public static Money operator *(Money moneyIstance, int power)
        {
            moneyIstance.Amount *= power;
            return moneyIstance;
        }

        public static bool operator >(Money moneyIstance1, Money moneyIstance2)
        {
            return moneyIstance1.Amount > moneyIstance2.Amount 
                && moneyIstance1.CurrencyType == moneyIstance2.CurrencyType;
        }
        public static bool operator <(Money moneyIstance1, Money moneyIstance2)
        {
            return moneyIstance1.Amount < moneyIstance2.Amount 
                && moneyIstance1.CurrencyType == moneyIstance2.CurrencyType;
        }
        public static bool operator ==(Money moneyIstance1, Money moneyIstance2)
        {
            return moneyIstance1.Amount == moneyIstance2.Amount
                && moneyIstance1.CurrencyType == moneyIstance2.CurrencyType;
        }
        public static bool operator !=(Money moneyIstance1, Money moneyIstance2)
        {
            return moneyIstance1.Amount != moneyIstance2.Amount
                && moneyIstance1.CurrencyType != moneyIstance2.CurrencyType;
        }

        public static bool operator true(Money moneyIstance)
        {
            return moneyIstance.Amount != 0
                && moneyIstance.CurrencyType == CurrencyType.UAN;
        }
        public static bool operator false(Money moneyIstance)
        {
            return moneyIstance.Amount == 0
                && moneyIstance.CurrencyType != CurrencyType.UAN;
        }
    }
}
