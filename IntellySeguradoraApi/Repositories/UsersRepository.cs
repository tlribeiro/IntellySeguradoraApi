using IntellySeguradoraApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.Repositories
{
    /// <summary>
    /// Repositório para acesso a dados do usuário.
    /// </summary>
    public class UsersRepository
    {
        //Contexto do banco.
        private IntellyDbContext context;


        /// <summary>
        /// Construtor para receber o contexto da aplicação.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public UsersRepository(IntellyDbContext context)
        {
            //Define o contexto.
            this.context = context;
        }

        /// <summary>
        /// Busca a lista de usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        public IEnumerable<User> GetAll()
        {
            //Realiza a pesquisa de todos os usuários.
            return this.context.Users;
        }

        /// <summary>
        /// Realiza a busca do usuário pelo identificador.
        /// </summary>
        /// <param name="id">Identificador único do usuário.</param>
        /// <returns>Usuário encontrato.</returns>
        public User GetByID(Guid id)
        {
            return this.context.Users.Find(id);
        }

        /// <summary>
        /// Busca o usuário pelo login e senha.
        /// </summary>
        /// <param name="login">Senha do usuário.</param>
        /// <param name="password">Login do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        public User GetByLoginAndPass(string login, string password)
        {
            //Realiza a busca do usuário e senha.
            return this.context.Users
                               .FirstOrDefault
                               (p => p.UserLogin.ToLower().Trim() == login.ToLower().Trim()
                                  && p.UserPassword == password);
        }
    }
}
