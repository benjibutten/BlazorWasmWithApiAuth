using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Gegga.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var config = builder.Configuration;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(WebConstants.OpenWebApiClientName, client =>
{
    client.BaseAddress = new Uri(config.GetValue<string>("WebApiUrl") ?? throw new ArgumentNullException("API url is missing"));
});

builder.Services.AddHttpClient(WebConstants.SecureWebApiClientName, client =>
{
    client.BaseAddress = new Uri(config.GetValue<string>("WebApiUrl") ?? throw new ArgumentNullException("API url is missing"));
}).AddHttpMessageHandler(sp =>  //Automatiskt skicka med tokens
{
    return sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler([config.GetValue<string>("WebApiUrl") ?? "https://localhost:7191"]);
});
//}).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>(); //<-- automatiskt skicka med tokens om det är samma baseUrl


builder.Services.AddMsalAuthentication(options =>
{
    config.Bind("B2CAuth", options.ProviderOptions.Authentication);
    var apiScope = config.GetValue<string>("B2CAuth:ApiScope") ?? throw new ArgumentNullException("API Scopes cant be null");
    options.ProviderOptions.DefaultAccessTokenScopes.Add(apiScope);
    options.ProviderOptions.Cache.CacheLocation = "sessionStorage"; // Förhindrar cachingproblem
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access"); // refresh tokens
});


await builder.Build().RunAsync();