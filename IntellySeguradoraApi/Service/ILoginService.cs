using IntellySeguradoraApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.Service
{
    public interface ILoginService
    {
        /// <summary>
        /// Realiza a validação de login.
        /// </summary>
        /// <param name="login">Dto com informações para validação.</param>
        /// <returns>Dto com resultado da validação.</returns>
        LoginResultVM ValidateLogin(LoginVM login);

        /// <summary>
        /// Retorna o identificador do usuário logado.
        /// </summary>
        /// <returns>Identificador do usuário logado.</returns>
        Guid GetLoggedUserId();
    }
}
