using Auth0.AspNetCore.Authentication;
using Blazored.Toast;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.HttpOverrides;
using AirAtlasPro.Services;
using Microsoft.OpenApi.Models;

namespace AirAtlasPro;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Auth0
        builder.Services
            .AddAuth0WebAppAuthentication(options => 
            {
                options.Domain = builder.Configuration["Auth0:Domain"]!;
                options.ClientId = builder.Configuration["Auth0:ClientId"]!;
            });

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddBlazoredToast();  // toast notifications

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

        builder.Services.AddHttpContextAccessor();

        // Configure and register HttpClient
        builder.Services.AddHttpClient<ISupportTicketService, SupportTicketService>(client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["OptechX-URLs:BASE-API"]!);
            client.DefaultRequestHeaders.Add("Accept", "");
            client.DefaultRequestHeaders.Add("User-Agent", "API");
            client.DefaultRequestHeaders.Add("X-Admin-Support-API-Key", builder.Configuration["X-Admin:Support-API-Key"]);
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseHttpsRedirection();

            // Add the following lines to configure HTTPS redirection middleware
            var forwardingOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            };
            forwardingOptions.KnownNetworks.Clear();
            forwardingOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardingOptions);
            
            // Configure the cookie policy to set SameSite=None and Secure for cookies
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None, // Allow cross-site cookies
                HttpOnly = HttpOnlyPolicy.None, // Allow JavaScript access to cookies if needed
                Secure = CookieSecurePolicy.Always // Require cookies to be sent over HTTPS
            });
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
