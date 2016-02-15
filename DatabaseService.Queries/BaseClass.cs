using log4net.Config;
using Ninject;
using log4net;
using System.Configuration;
namespace DatabaseService.Queries
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
