using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NPVCalculator.Services
{
    [KnownType(typeof(NetPresentValue))]
    [DataContract]
    public class NetPresentValue
    {
        [DataMember]
        public decimal Amount;
        [DataMember]
        public decimal DiscountRate;
    }
}
