using IntellySeguradoraApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.Service
{
    /// <summary>
    /// Operação disponíveis para usuário.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Valida o login do usário.
        /// </summary>
        /// <param name="login">Usuário para acesso a aplicação.</param>
        /// <param name="password">Senha para acesso a aplicação.</param>
        /// <returns>Usuário encontrado.</returns>
        User ValidateLogin(String user, String password);

        /// <summary>
        /// Busca o usuário pelo identificador.
        /// </summary>
        /// <param name="id">Identificador único do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        User GetByID(Guid id);

        /// <summary>
        /// Retorna todos os usuários registrados na aplicação.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAll();

    }
}
