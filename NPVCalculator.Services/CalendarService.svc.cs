using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace NPVCalculator.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CalendarService : ICalendarService
    {
        public DateTimeOffset GetDate()
        {
            var date = DateTimeOffset.Now.Date;
            AppSvcCtx.Resolve<ILoggingProvider>().Log("CalendarService.GetDate | " + date.ToShortDateString());
            return date;
        }

    }
}