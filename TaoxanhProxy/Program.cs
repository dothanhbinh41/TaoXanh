using Microsoft.AspNetCore.Diagnostics;
using TaoxanhProxy.Jobs;
using TaoxanhProxy.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
ConfigServices(builder.Services);
var app = builder.Build();
builder.WebHost.UseUrls($"http://0.0.0.0:10000");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}
var options = new ExceptionHandlerOptions
{
    ExceptionHandler = httpContext =>
    {
        var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>(); 
        httpContext.Response.StatusCode = 400;
        httpContext.Response.ContentType = "application/json";
        httpContext.Request.ContentType = "application/json"; 
        httpContext.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.Message ?? "");
        return Task.CompletedTask;
    },
    AllowStatusCode404Response = false
};
app.UseExceptionHandler(options);
//app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseRouting(); 
//app.UseAuthorization(); 
app.MapRazorPages();
app.MapControllers();
app.Run();


void ConfigServices(IServiceCollection service)
{
    service.AddMemoryCache(); 
    service.AddSingleton<IInternalService, InternalService>();
    service.AddSingleton<IApplicationService, ApplicationService>();
    service.AddHostedService<ProxyServerService>();
}