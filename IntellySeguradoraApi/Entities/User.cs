using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.Entities
{
    /// <summary>
    /// Entidade de usuário.
    /// </summary>
    public class User
    {
        public Guid UserId { get; set; }
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String UserRole { get; set; }
        public String UserLinkedin { get; set; }
        //Dados de acesso a conta.
        public String UserLogin { get; set; }
        public String UserPassword { get; set; }
    }
}
