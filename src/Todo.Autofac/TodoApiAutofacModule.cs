using Autofac;
using Microsoft.Extensions.Options;
using Brafton.Http.RestClient;
using System;
using Skynet.Token;
using Microsoft.AspNetCore.Http;
using Brafton.Common;
using Brafton.Settings;
using Todo.Settings;
using Todo.Client;
using TodoApi.Shared;

namespace Todo.Autofac
{
    public class TodoApiAutofacModule : Module
    {
        private readonly ICallerVersions callerVersions;

        public TodoApiAutofacModule(ICallerVersions callerVersions)
        {
            this.callerVersions = callerVersions;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //Images Api connection
            builder.Register(c =>
            {

                if (!c.IsRegistered<IOptions<TodoSettings>>())
                    throw new BraftonException("IOptions<Settings.TodoSettings>> needs to be registered to setup the client");
                if (!c.IsRegistered<IOptions<SecuritySettings>>())
                    throw new BraftonException("IOptions<Settings.SecuritySettings>> needs to be registered to setup the client");
                var securityOptions = c.Resolve<IOptions<SecuritySettings>>().Value;
                var todoSettings = c.Resolve<IOptions<TodoSettings>>().Value;
                string DelegateToken = null;

                if (todoSettings == null)
                    throw new BraftonException("Base Todo Settings have not been populated");

                if (securityOptions == null)
                    throw new BraftonException("Security Todo Settings Settings have not been populated");

                if (string.IsNullOrEmpty(securityOptions.CallingApplication.UserName) || todoSettings.UseApplicationCredsByDefault == false)
                {
                    //So web-based authentication
                    //grab the idtoken from the claims in the http user.
                    if (!c.IsRegistered<IHttpContextAccessor>())
                        throw new BraftonException("IhttpContextAccessor needs to be registered for non-anonymous user authentication");

                    var loginToken = c.Resolve<IHttpContextAccessor>().HttpContext.User.FindFirst("id_token");
                    if (string.IsNullOrWhiteSpace(loginToken?.Value)) throw new UnauthorizedAccessException();

                    if (securityOptions.CallingApplication?.Auth0ClientId == null)
                        throw new BraftonException("Application Auth0ClientId not been populated");

                    if (todoSettings?.Auth0ClientId == null)
                        throw new BraftonException("Api Auth0ClientId not been populated");

                    TokenResponse token = null;
                    try
                    {
                        token = GenerateToken.DelegateToken(
                            new TokenRequest
                            {
                                ClientId = securityOptions.CallingApplication.Auth0ClientId,
                                IdToken = loginToken.Value.ToString(),
                                Domain = securityOptions.Auth0Domain,
                                TargetClient = todoSettings.Auth0ClientId
                            });
                    }
                    catch (Exception ex)
                    {
                        throw new BraftonException("Could not create delegate token from logged in token", ex);
                    }
                    DelegateToken = token.IdToken;
                }
                else
                {
                    TokenResponse token = null;
                    try
                    {
                        token = GenerateToken.DelegateToken(
                            new TokenRequest
                            {
                                ClientId = securityOptions.CallingApplication.Auth0ClientId,
                                Username = securityOptions.CallingApplication.UserName,
                                Password = securityOptions.CallingApplication.Password,
                                Domain = securityOptions.Auth0Domain,
                                TargetClient = todoSettings.Auth0ClientId
                            });
                    }
                    catch (Exception ex)
                    {
                        throw new BraftonException("Could not create anonymous delegate token", ex);
                    }
                    DelegateToken = token.IdToken;
                }

                if (todoSettings.Endpoint == null)
                    throw new BraftonException("ImagesApi Endpoint not been populated");

                ITodoConnection connection = null;
                try
                {
                    connection = new TodoConnection(new Uri(todoSettings.Endpoint), DelegateToken, this.callerVersions);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating connection with Endpoint:{todoSettings.Endpoint} ApplicationName:{securityOptions.CallingApplication.Name} DelegateToken:{DelegateToken}. Inner Exception - {ex.ToString()}");
                }
                return connection;
            })
            .As<ITodoConnection>().InstancePerLifetimeScope();

            builder.RegisterType<TodoClient>().As<ITodo>().InstancePerLifetimeScope();
        }
    }
}
