using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NPVCalculator.Services
{
    [KnownType(typeof(Cashflow))] //using KnownType instead of ServiceKnownType due to documented bug with svcutil generation and SvcKnownType
    [DataContract]
    public class Cashflow
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int PaymentIntervalInMonths { get; set; }
        [DataMember]
        public int NumberOfPayments { get; set; }
        [DataMember]
        public decimal AmountPerPayment { get; set; } 
    }
}
