using MessengerCoreAPI.Models.RGDialogsClients;
using MessengerCoreAPI.Models.RGDialogsClients.RGDialogsClientsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IRGDialogsClientsFactory dialogsClientsFactory = new DefaultRGDialogsClientsFactory();
var dialogs = new RGDialogsClientsCollection { RGDialogsClients = dialogsClientsFactory.CreateRGDialogsClients() };
builder.Services.AddSingleton(dialogs);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
