using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace NPVCalculator.Services
{
    public class AppSvcCtx
    {
        static AppSvcCtx()
        {
            Container = new UnityContainer();
        }

        public static void SetupApplicationDependencies()
        {
            Container.RegisterInstance<ILoggingProvider>(new DebugLoggingProvider());
            Container.RegisterInstance<IStateProvider>(new AspNetInMemoryStateProvider());

            Container.RegisterInstance<ICalendarService>(new CalendarServiceWCFFacade());  // new CalendarService() for in-process service
            //either the direct service, or the wcf facade can be loaded, allowing in process, or network access local or remote using http transport
            //and a config setting could make this switch from in process to wcf service a metadata option allowing post compile configuration for flexible deployment
            
            Container.RegisterInstance<ICalculationService>(new CalculationService()); 
        }

        private static UnityContainer Container; 
        public static UnityContainer Testing_DIContainer // allowing unit tests full control over the container
        {
            get { return Container; }
            set { Container = value; }
        }

        public static T Resolve<T>() //This is the only dependency surfaced to the general logic classes - in fact less dependency than for instance putting an IUnityContainer in the constructor of the object - it's only two points, 1, the app context, and two the resolve method .. however the unit tests will have a dependency on the full container - one approach is to facade the container interface
        {
            return Container.Resolve<T>();
        }
    }
}
