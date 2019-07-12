using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IntellySeguradoraApi.Service;
using System.Security.Claims;
using IntellySeguradoraApi.Entities;
using Microsoft.Extensions.Logging;

namespace IntellySeguradoraApi.Controllers
{
    /// <summary>
    /// Rest API para Usuário versão 1.0.
    /// Versionamento realizado através da url para não impactar possíveis clientes.
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1.0/users")]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Construtor para receber a instância do loggger.
        /// </summary>
        /// <param name="logger">Instância de logger.</param>
        public UsersController(ILogger<LoginController> logger) : base(logger)
        {
            //Construtor padrão.
        }

        /// <summary>
        /// Recurso para retornar os dados do usuário logado.
        /// A aplicação busca identificador do usuário enviano no JWT Token para poder acessar as informações.
        /// Para acessar esse método o usuário deve estar logado, ou seja, deve enviar o token na tag Authorization.
        /// </summary>
        /// <param name="iService">Instância do serviço do Usuário.</param>
        /// <param name="iLogin">Intância do serviço do Login.</param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("me", Name = "Get")]
        public IActionResult GetMe([FromServices] IUserService iService,
                            [FromServices] ILoginService iLogin)
        {
            //Variável de retorno.
            IActionResult result = null;

            //Registra a chamada da API.
            this.ILogger.LogInformation("api/v1.0/users/me -> GET");

            try
            {
                //A interface iLogin Acessa a inforação do contexto para retornar o Id do usuário logado.
                Guid userId = iLogin.GetLoggedUserId();

                //Busca os dados usuário logado na aplicação.                
                User loggedUser = iService.GetByID(userId);

                //Valida se encontrou o usuário.
                if (loggedUser != null)
                {
                    //Monta o resultado da operação.
                    result = new OkObjectResult(new
                    {
                        id = iLogin.GetLoggedUserId(),
                        name = loggedUser.UserName,
                        email = loggedUser.UserEmail,
                        role = loggedUser.UserRole,
                        linkedin = loggedUser.UserLinkedin
                    });
                }
                else
                {
                    //Não conseguiu encontrar o usuário, apenas de ser um id verdadeiro.
                    //Informa que ocorreu um erro insperado.
                    result = StatusCode(500);
                }
            }
            catch (Exception e)
            {
                //Registra o log do erro.
                this.ILogger.LogError(e, "User/Me:" + e.Message);
                //Informa que houve um erro na aplicação sem informar a causa.
                result = StatusCode(500);
            }

            return result;
        }
    }
}
