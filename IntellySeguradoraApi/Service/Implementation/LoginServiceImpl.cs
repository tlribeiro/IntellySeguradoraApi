using System;

using Microsoft.IdentityModel.Tokens;
using IntellySeguradoraApi.Entities;
using System.Security.Claims;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using IntellySeguradoraApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IntellySeguradoraApi.Configurations;
using Microsoft.AspNetCore.Http;

namespace IntellySeguradoraApi.Service.Implementation
{
    /// <summary>
    /// Serviço para operações de Login.
    /// </summary>
    public class LoginServiceImpl : ILoginService
    {
        //Variáveis
        private ILogger iLogger;
        private IUserService iService;
        private JWTConfiguration jwtConfigurations;
        private LoginConfiguration loginConfigurations;
        private IHttpContextAccessor iHttpContext;

        /// <summary>
        /// COnstrutor para receber via injeção de dependencia o Looger e o Serviço do usuário.
        /// </summary>
        /// <param name="logger">Instância do Logger.</param>
        /// <param name="service">Instância do Serviço do Usuário.</param>
        /// <param name="loginConfigurations"></param>
        /// <param name="jwtConfigurations"></param>
        public LoginServiceImpl(ILogger<LoginServiceImpl> logger, IUserService service, [FromServices] LoginConfiguration loginConfigurations,
            [FromServices]JWTConfiguration jwtConfigurations, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            this.iLogger = logger;
            this.iService = service;
            this.jwtConfigurations = jwtConfigurations;
            this.loginConfigurations = loginConfigurations;
            this.iHttpContext = httpContextAccessor;
        }

        /// <summary>
        /// Retorna o identificador do usuário logado.
        /// </summary>
        /// <returns>Identificador do usuário logado.</returns>
        public Guid GetLoggedUserId()
        {
            //Variável de resulstado.
            Guid userId = Guid.Empty;

            //Valida se o contexto não está nulo.
            if (iHttpContext != null)
            {
                //Busca o usuário no contexto.
                String id = this.iHttpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                //Valida se encontrou o id do usuário.
                userId = id != null ? new Guid(id) : Guid.Empty;
            }

            //Retorna o identificador do usuário.
            return userId;
        }

        /// <summary>
        /// Realiza a validação de login.
        /// </summary>
        /// <param name="login">Dto com informações para validação.</param>
        /// <returns>Dto com resultado da validação.</returns>
        public LoginResultVM ValidateLogin(LoginVM login)
        {
            //Resultado da operação.
            LoginResultVM result = new LoginResultVM();
            try
            {
                //Define o padrão como 'não logado'.
                result.IsOk = false;
                //Valida o login e senha do usuário.
                User user = this.iService.ValidateLogin(login.User, login.Pass);

                //Valida se o usuário foi encontado.
                if (user != null)
                {
                    //Realiza as validações do login.
                    result.IsOk = true;
                    //Token gerado para acesso do cliente.
                    result.Token = this.GenerateJWT(user);
                }
            }
            catch (Exception e)
            {
                //Registra o log do erro.
                this.iLogger.LogError(e, e.Message);
            }

            //Retorna o resultado da operação.
            return result;
        }

        /// <summary>
        /// GEra o JWT token.
        /// </summary>
        /// <returns>JWT Token gerado conforme os dados do usuário.</returns>
        private String GenerateJWT(User user)
        {
            //Token de resultado.
            String tokenResult = null;
            //Data inicial para calculo da validade do token
            DateTime createdDt = DateTime.Now;
            //Validade do token de 60 minutos a partir da data atual.
            DateTime expirationDt = createdDt + TimeSpan.FromMinutes(jwtConfigurations.Minutes);

            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.UserId.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId.ToString())
                    }
                );

            var handler = new JwtSecurityTokenHandler();

            //Cria o token de acesso.
            var accessToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = jwtConfigurations.Issuer,
                Audience = jwtConfigurations.Audience,
                SigningCredentials = loginConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createdDt,
                Expires = expirationDt
            });

            //Serializa o token.
            tokenResult = handler.WriteToken(accessToken);

            //Retorna o token gerado.
            return tokenResult;
        }
    }
}
