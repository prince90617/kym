using log4net;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using log4net.Config;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DatabaseServiceWebservice.Controllers {
    
  public abstract class BaseController : ApiController {
    static IKernel kernel;
    protected ILog logger;

    static BaseController() {
      kernel = new StandardKernel();

      kernel.Bind<ILog>().ToMethod(x => {
        var logName = ConfigurationManager.AppSettings["LoggingName"];
        return LogManager.GetLogger(logName);
      });

     // kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
    }

    public BaseController() {
       
      logger = kernel.Get<ILog>();
    }


    protected string GetValueOrNull(GetValueDelegate getValueMethod) {
      try {
        return getValueMethod();
      } catch (RuntimeBinderException) {
        return null;
      }
    }

    protected delegate string GetValueDelegate();

    protected int[] ParseIdArray(dynamic array) {
      var cameraIds = array as JArray;
      if (cameraIds == null) {
        return new int[0];
      } else {
        return cameraIds.Select(jv => (int)jv).ToArray();
      }
    }

    protected IEnumerable<T> ParseArray<T>(dynamic array) {
      return ((JArray)array).ToObject<List<T>>();
    }
  }
}
