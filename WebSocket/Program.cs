using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebSocket;
using WebSocket.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
});

builder.Services.AddScoped(sp => new HttpClient
{
    //BaseAddress = new Uri("http://192.168.1.115:5220/")
    BaseAddress = new Uri("https://pedidosapi-596w.onrender.com/")
});

builder.Services.AddScoped<PedidoService>();

await builder.Build().RunAsync();
