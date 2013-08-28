using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using SupportAnalyst.Data;

namespace SupportAnalyst.Common
{
    public class ServiceLocator
    {
        private static IKernel Kernel;

        static ServiceLocator()
        {
            Kernel = new StandardKernel();

            //DataAccess Mappings
            Kernel.Bind<DbContext>().To<ESamplesDbContext>().WhenInjectedInto<IESamplesLogRepository>();
            Kernel.Bind<DbContext>().To<PACDbContext>().WhenInjectedInto<IPacLogRepository>();
            //Kernel.Bind<DbContext>().To<CCUIDbContext>().WhenInjectedInto<ICCUILogRepository>();

            Kernel.Bind<ILogRepository>().To<LogRepository>();
            Kernel.Bind<IPacLogRepository>().To<PacLogRepository>();
            Kernel.Bind<IESamplesLogRepository>().To<ESamplesLogRepository>();
            //Kernel.Bind<ICCUILogRepository>().To<CCUILogRepository>();

            //Business Logic Mappings
            //Kernel.Bind<ILogManager>().To<LogManager>();
            //Kernel.Bind<IPacLogManager>().To<PacLogManager>();
            //Kernel.Bind<IEsamplesLogManager>().To<ESamplesLogManager>();
            //Kernel.Bind<ICCUILogManager>().To<CCUILogManager>();
        }

        public static T GetInstance<T>() where T : class
        {
            return Kernel.Get<T>();
        }
    }
}
