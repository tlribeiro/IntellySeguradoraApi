using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IntellySeguradoraApi.Controllers
{
    /// <summary>
    /// Classe base para o Controller.
    /// </summary>
    public class BaseController : Controller
    {
        //Logger da aplicação.
        protected ILogger ILogger;

        /// <summary>
        /// Construtor para receber a instância do loggger.
        /// </summary>
        /// <param name="logger">Instância de logger.</param>
        public BaseController(ILogger<LoginController> logger)
        {
            //Define a instância do log.
            this.ILogger = logger;
        }
    }
}
