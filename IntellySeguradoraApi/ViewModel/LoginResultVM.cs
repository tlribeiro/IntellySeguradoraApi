using IntellySeguradoraApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.ViewModel
{
    /// <summary>
    /// Resultado do Login.
    /// </summary>
    public class LoginResultVM
    {
        public Boolean IsOk { get; set; }
        public String Token { get; set; }
        public User User { get; set; }
    }
}
