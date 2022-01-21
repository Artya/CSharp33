using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_2_3_lab
{
    class Money
    {
        public static CurrencyTypes DefaultCurrency = CurrencyTypes.UAH;

        public double Amount { get; private set; }
        public CurrencyTypes CurrencyType { get; }

        public Money(CurrencyTypes currencyType, double amount)
        {
            this.Amount = amount;
            this.CurrencyType = currencyType;
        }

        public static Money operator + (Money leftMoney, Money rightMoney)
        {
            if (leftMoney.CurrencyType != rightMoney.CurrencyType)
            {
                throw new Exception("Left and right money should be the same currency type");
            }

            var resultAmount = leftMoney.Amount + rightMoney.Amount;
            return new Money(leftMoney.CurrencyType, resultAmount);
        }

        public static Money operator + (Money money, double addition)
        {
            var resultAmount = money.Amount + addition;
            return new Money(money.CurrencyType, resultAmount);
        }

        public static Money operator -- (Money money)
        {
            --money.Amount;
            return money;
        }
        public static Money operator * (Money money, int multiplier)
        {
            var rezultAmount = money.Amount * multiplier;
            return new Money(money.CurrencyType, rezultAmount);
        }

        public static bool operator > (Money left, Money Right)
        {
            if (left.CurrencyType != Right.CurrencyType)
                throw new Exception("Left and Right currency types must be equal");

            return left.Amount > Right.Amount;
        }
        public static bool operator < (Money left, Money Right)
        {
            if (left.CurrencyType != Right.CurrencyType)
                throw new Exception("Left and Right currency types must be equal");

            return left.Amount < Right.Amount;
        }

        public static bool operator true(Money money)
        {
            return money.CurrencyType == DefaultCurrency;
        }

        public static bool operator false(Money money)
        {
            return money.CurrencyType != DefaultCurrency;
        }

        public static implicit operator string(Money money)
        {
            return money.ToString();
        }

        public static implicit operator double(Money money)
        {
            return money.Amount;
        }

        public override string ToString()
        {
            return Amount.ToString() + " " + CurrencyType;
        }

        public static explicit operator Money(double amount)
        {
            return new Money(Money.DefaultCurrency, amount);
        }

        public static explicit operator Money(string stringMoney)
        {
            var errorText = "Can`t convert string to Money, incorrect format";

            var parts = stringMoney.Split(new char[] { ' ' });

            if (parts.Length != 2)
                throw new Exception(errorText);

            double amount;
            CurrencyTypes currencyType;


            if (double.TryParse(parts[0].Trim(), out amount) && Enum.TryParse(parts[1].Trim(), out currencyType))
            {
                return new Money(currencyType, amount);
            }

            if (double.TryParse(parts[1].Trim(), out amount) && Enum.TryParse(parts[0].Trim(), out currencyType))
            {
                return new Money(currencyType, amount);
            }

            throw new Exception(errorText);
        }

        public static Money ConvertCurrency(Money money, CurrencyTypes newCurrency, double course)
        {
            var amount = money.Amount * course;
            return new Money(newCurrency, amount);
        }
    }
}
