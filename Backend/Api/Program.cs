using AccesoDatos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Servicios.Interfaces;
using Servicios.Servicios;
using System.Text;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 102400000;
    e.EnableDetailedErrors = true;
});
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataBaseContext>((options) =>
{});
builder.Services.AddScoped<IJtAuth, Auth>();
builder.Services.AddScoped<IUsuarios, Usuarios>();
builder.Services.AddScoped<IProductos, Productos>();
builder.Services.AddScoped<IRoles,Roles>();
builder.Services.AddScoped<ISucursal, Sucursales>();
builder.Services.AddScoped<IRolesUser, RolesUser>();
builder.Services.AddScoped<IDepartamentosMunicipios, DepartamentosMunicipios>();
builder.Services.AddScoped<IClinicas,Clinicas>();
builder.Services.AddScoped<ICamas,Camas>();
builder.Services.AddScoped<IIBodegasLotes,LotesBodegas>();
builder.Services.AddScoped<IPacientes,Pacientes>();
builder.Services.AddScoped<IClientes, Clientes>();
builder.Services.AddScoped<IDiagnosticos, Diagnosticos>();
builder.Services.AddScoped<ICitas, Citas>();
builder.Services.AddScoped<IExamenes, Examenes>();
builder.Services.AddSignalR().AddHubOptions<HubCamasController>(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
             .AllowAnyMethod()
             .AllowAnyHeader()
             //.WithHeaders("Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept")
             .SetIsOriginAllowed(origin => true) // allow any origin
             .AllowCredentials());
app.UseAuthorization();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Version 0.1");
    });
    endpoints.MapControllers();//.RequireAuthorization();
    endpoints.MapHub<HubCamasController>("/hub/Camas");
});
app.MapControllers();

app.Run();
