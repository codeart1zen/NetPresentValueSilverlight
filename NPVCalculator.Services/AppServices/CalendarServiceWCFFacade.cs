using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPVCalculator.Services
{    
    public class CalendarServiceWCFFacade : ICalendarService
    {
        public DateTimeOffset GetDate()
        {
            AppSvcCtx.Resolve<ILoggingProvider>().Log("CalendarServiceWCFFacade.GetDate");
            var proxy = new CalService.CalendarServiceClient();
            var date = proxy.GetDate();
            try
            {
                if (proxy.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    proxy.Close();
                }
            }
            catch (Exception)
            {
                proxy.Abort();
            }
            return date;
        }
  
    }
}