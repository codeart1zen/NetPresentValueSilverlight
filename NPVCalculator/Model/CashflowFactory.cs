using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NPVCalculator.Model
{
    public class CashflowFactory
    {
        public static CalcService.Cashflow GetRandomCashflow()
        {
            var cashflow = new CalcService.Cashflow();
            cashflow.Name = GetRandomName();
            cashflow.PaymentIntervalInMonths = rnd.Next(3) + 1;
            cashflow.NumberOfPayments = rnd.Next(20) + 1;
            cashflow.AmountPerPayment = 10000M;             
            return cashflow;
        }
        
        private static string GetRandomName()
        {
            var name = "";
            for (int i = 0; i < 3; i++)
            {
                name += GetRandomLetter();
            }
            return name;
        }

        private static Random rnd = new Random();
        private static char GetRandomLetter()
        {
            return (char)((int)'A' + rnd.Next(26));
        }
    }
}
