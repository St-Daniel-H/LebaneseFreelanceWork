using Microsoft.EntityFrameworkCore;
using LebUpwor.core.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using LebUpwor.core.Interfaces;
using BookingSystem.core.Repository;
using LebUpwor.core.Repository;
using LebUpwork.Api.Interfaces;
using LebUpwork.Api.Repository;
using LebUpwork.service.Repository;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IAppliedToTaskRepository, AppliedToTaskRepository>();
builder.Services.AddScoped<ITokenHistoryRepository, TokenHistoryRepository>();
builder.Services.AddScoped<ICashOutHistoryRepository, CashOutHistoryRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IAppliedToTaskService, AppliedToTaskService>();
builder.Services.AddScoped<ITokenHistoryService, TokenHistoryService>();
builder.Services.AddScoped<ICashOutHistoryService, CashOutHistoryService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IJobService, JobService>();
builder.Services.AddTransient<IAppliedToTaskService,AppliedToTaskService>();
builder.Services.AddTransient<ITokenHistoryService, TokenHistoryService>();
builder.Services.AddTransient<ICashOutHistoryService, CashOutHistoryService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IRoleService, RoleService>();

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//idenitty

var app = builder.Build();
//map controllers
app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


builder.Services.AddDbContext<UpworkLebContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
