using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NPVCalculator.Services
{
    [ServiceContract]
    public interface ICalendarService
    {
        [OperationContract]
        DateTimeOffset GetDate();
    }
}
