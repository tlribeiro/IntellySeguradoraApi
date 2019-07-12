using IntellySeguradoraApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntellySeguradoraApi.Repositories
{
    /// <summary>
    /// Contexto do banco de dados.
    /// </summary>
    public class IntellyDbContext : DbContext
    {
        /// <summary>
        /// Contrutor padrão.
        /// </summary>
        /// <param name="options">Instância DbContext Options.</param>
        public IntellyDbContext(DbContextOptions<IntellyDbContext> options) : base(options)
        {

        }

        //Usuários.
        public DbSet<User> Users { get; set; }
    }}
