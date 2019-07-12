using IntellySeguradoraApi.Entities;
using IntellySeguradoraApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.Service.Implementation
{
    /// <summary>
    /// Implementação do serviço do usuário.
    /// </summary>
    public class UserServiceImpls : IUserService
    {
        //Contexto do banco.
        private UsersRepository repository;

        /// <summary>
        /// Construtor para receber o repositório como DI.
        /// </summary>
        /// <param name="repository">Instância do repositório.</param>
        public UserServiceImpls(UsersRepository repository)
        {
            //Define o contexto.
            this.repository = repository;
        }

        /// <summary>
        /// Retorna a Lista de usuários.
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public IEnumerable<User> GetAll()
        {
            //Retorna a lista de usuários.
            return this.repository.GetAll();
        }

        /// <summary>
        /// Retorna o usuário pelo identificador único.
        /// </summary>
        /// <param name="id">Identificador único do usuário.</param>
        /// <returns>Instância do usuário.</returns>
        public User GetByID(Guid id)
        {
            //Pesquisa o usuário pelo identificador.
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Valida o login do usário.
        /// </summary>
        /// <param name="login">Usuário para acesso a aplicação.</param>
        /// <param name="password">Senha para acesso a aplicação.</param>
        /// <returns>Usuário encontrado.</returns>
        public User ValidateLogin(string login, string password)
        {
            User result = null;

            //Valida se os dados foram informados para realizar a consulta.
            if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(password))
            {
                //Realiza a busca do usuário e senha.
                result = this.repository.GetByLoginAndPass(login, password);
            }

            //Retorna o resultado encontrado.
            return result;
        }
    }
}
