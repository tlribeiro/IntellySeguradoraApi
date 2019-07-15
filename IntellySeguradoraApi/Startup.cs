using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IntellySeguradoraApi.Repositories;
using Microsoft.EntityFrameworkCore;
using IntellySeguradoraApi.Service.Implementation;
using IntellySeguradoraApi.Service;
using IntellySeguradoraApi.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntellySeguradoraApi
{
    /// <summary>
    /// Inicialização da aplicação.
    /// </summary>
    public class Startup
    {
        //Variavél de configuração.
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Construtor para configuração
        /// </summary>
        /// <param name="configuration">Recebe a instância da configuração.</param>
        public Startup(IConfiguration configuration)
        {
            //Define o atributo da configuração.
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Recupera a string de conexão do banco de dados.
            var connection = Configuration["ConnectionString:AWS:RDS"];
            //Adiciiona o contexto do banco.
            services.AddDbContext<IntellyDbContext>
                (options => options.UseSqlServer(connection));

            //Habilita o CORS para aceitar requisições do dominio que está a aplicação em REACT.
            services.AddCors(c =>
            {
                c.AddPolicy("IntellyOrigin", options =>
                                             options.WithOrigins("http://localhost:3000",
                                                                 "http://localhost:8080",
                                                                 "https://tlr-intelly-malucelli-view.herokuapp.com")
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            //Repositório do usuário.
            services.AddTransient<UsersRepository>();
            // Registra os serviços para o container de DI.
            // Serviço do usuário.
            services.AddScoped<IUserService, UserServiceImpls>();
            // Serviço do Login.
            services.AddScoped<ILoginService, LoginServiceImpl>();
            // Adiciona o MVC.
            services.AddMvc();

            //Define as configurações da autenticação.
            this.AuthenticationSettings(services);

            //Http Context.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Define a compatiblidade da versão.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        /// <summary>
        /// Define as configurações da autenticação.
        /// </summary>
        /// <param name="services">Serviço de coleção.</param>
        private void AuthenticationSettings(IServiceCollection services)
        {
            //Configurações do login.
            LoginConfiguration loginConfig = new LoginConfiguration();
            //Adiciona a instância para ser usado em toda aplicação.
            services.AddSingleton(loginConfig);

            //Configurações de Autenticação.
            JWTConfiguration jwtConfig = new JWTConfiguration();
            //Binding com a configuração.
            new ConfigureFromConfigurationOptions<JWTConfiguration>(
                Configuration.GetSection("JWTConfiguration"))
                    .Configure(jwtConfig);
            //Adiciona a instância para ser usado em toda aplicação.
            services.AddSingleton(jwtConfig);

            //Adiciona a autenticação do tipo Bearer Json Web Token.
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                //Configurações da validação.
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = loginConfig.Key;
                paramsValidation.ValidAudience = jwtConfig.Audience;
                paramsValidation.ValidIssuer = jwtConfig.Issuer;

                // Indica para validar a assinatura de um token recebido.
                paramsValidation.ValidateIssuerSigningKey = true;
                // Indica para validar se o token ainda é valido.
                paramsValidation.ValidateLifetime = true;
                // Em caso de diferença de sincronização de horário indica a tolenrancia permitida.
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            //Habilitar o uso de token para proteger os recursos do projeto.
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Valida se é o ambiente de desenvolvimeto.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Adiciona o login ao console.
            loggerFactory.AddConsole();
            //Adiciona o CORS especifico.
            app.UseCors("IntellyOrigin");
            //Adiciona o MVC.
            app.UseMvc();
            //Define página inicial.
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("TLR - Intelly | JMalucelli");
            });
        }
    }
}
