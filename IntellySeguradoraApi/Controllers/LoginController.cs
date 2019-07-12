using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using IntellySeguradoraApi.ViewModel;
using IntellySeguradoraApi.Service;

namespace IntellySeguradoraApi.Controllers
{
    /// <summary>
    /// Rest API para Login versão 1.0.
    /// Versionamento realizado através da url para não impactar possíveis clientes.
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1.0/Login")]
    public class LoginController : BaseController
    {
        /// <summary>
        /// Construtor para receber a instância do loggger.
        /// </summary>
        /// <param name="logger">Instância de logger.</param>
        public LoginController(ILogger<LoginController> logger) : base(logger)
        {
            //Construtor padrão.
        }

        /// <summary>
        /// Valida as credenciais do usuário.
        /// Caso as credenciais estejam corretas é gerado o token de acesso.
        /// </summary>
        /// <param name="loginVM">Dados do login do usuário.</param>
        /// <param name="service">Instância do serviço do login.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginVM loginVM,
                                   [FromServices] ILoginService service)
        {
            //Variavel de resultado.
            IActionResult result;

            //Registra a chamada da API.
            this.ILogger.LogInformation("api/v1.0/login -> POST");

            try
            {
                //Realiza a validação do login.
                LoginResultVM loginResult = service.ValidateLogin(loginVM);

                //Valida se o login foi realizado com sucesso.
                if (loginResult.IsOk)
                {
                    //Resposta da operação.
                    //Define o token de acesso gerado para o usuário.
                    result = new OkObjectResult(new { token = loginResult.Token });
                }
                else
                {
                    //Registra o log da recusa das credenciais.
                    this.ILogger.LogInformation("Login/POST: Usuário e/ou senha inválido.");
                    //Retorna o HTTP Status 403 para sinalizar que ouve problemas com as credencias informada.
                    //É possível personalizar o rseultado, mas o status já é explicativo.
                    result = Forbid(); //StatusCode(403);
                }
            }
            catch (Exception e)
            {
                //Registra o log do erro.
                this.ILogger.LogError(e, "Login/POST:" + e.Message);
                //Informa que houve um erro na aplicação sem informar a causa.
                result = StatusCode(500);
            }
            //Retorna o resultado do login.
            return result;
        }
    }
}
