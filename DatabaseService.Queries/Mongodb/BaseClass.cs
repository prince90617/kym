using log4net.Config;
using Ninject;
using log4net;
using System.Configuration;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace DatabaseService.Queries.Mongodb
{
    public static class BaseClass
    {
        static IKernel kernel;
        public static ILog logger;

        static BaseClass()
        {
            kernel = new StandardKernel();

            kernel.Bind<ILog>().ToMethod(x =>
            {
                var logName = ConfigurationManager.AppSettings["LoggingName"];
                return LogManager.GetLogger(logName);
            });

            logger = kernel.Get<ILog>();
        }

        //public BaseClass()
        //{

        //    logger = kernel.Get<ILog>();
        //}
    }
}
