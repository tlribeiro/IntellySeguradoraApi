using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IntellySeguradoraApi.Migrations
{
    /// <summary>
    /// Migration para adicionar novo usuário.
    /// </summary>
    public partial class AddNewUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adiciona o novo usuário da aplicação.
            migrationBuilder.InsertData(
                   table: "Users",
                   columns: new[] { "UserId", "UserName", "UserEmail", "UserRole", "UserLinkedin", "UserLogin", "UserPassword" },
                   values: new object[] { Guid.NewGuid(), "Thiago Ribeiro", "tlribeiro@outlook.com", "Software Architect", "https://www.linkedin.com/in/tlribeiro/", "thiago", "123456" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Script de rollback.
        }
    }
}
