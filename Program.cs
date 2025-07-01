using Microsoft.Azure.Cosmos;
using TicketBookingApp.Interfaces;
using TicketBookingApp.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using TicketBookingApp.Models;
using TicketBookingApp.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TicketEntryValidator>();

builder.Services.AddSingleton<CosmosClient>(s =>
{
    var config = s.GetRequiredService<IConfiguration>();
    return new CosmosClient(config["CosmosDb:Account"], config["CosmosDb:Key"]);
});

builder.Services.AddSingleton<ITicketService, TicketService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Ticket}/{action=Index}/{id?}");
app.Run();
