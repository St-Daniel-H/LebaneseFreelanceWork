using Microsoft.EntityFrameworkCore;
using LebUpwor.core.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using LebUpwor.core.Interfaces;
using LebUpwor.core.Repository;
using LebUpwork.Api.Interfaces;
using LebUpwork.Api.Repository;
using LebUpwork.service.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LebUpwork.Api.Extensions;
using LebUpwork.Api.Settings;
using LebUpwork.Api.Validators;
using LebUpwork.service.Interfaces;
using Hangfire;
using Microsoft.Extensions.Options;
using LebUpwork.Api.CronJobs;
using Microsoft.Extensions.DependencyInjection;
using Castle.Core.Configuration;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.SignalR;
using LebUpwork.Api.Hubs;
using LebUpwork.Api.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<UpworkLebContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//add swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
//make admin policy that checks if jwt has role Admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),

        ClockSkew = TimeSpan.Zero
    };
});
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
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<INewJobRepository, NewJobRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IAppliedToTaskService, AppliedToTaskService>();
builder.Services.AddScoped<ITokenHistoryService, TokenHistoryService>();
builder.Services.AddScoped<ICashOutHistoryService, CashOutHistoryService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<INewJobService, NewJobServices>();
builder.Services.AddScoped<INotificationService, NotificationService>();


builder.Services.AddTransient<FileValidation>();


//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//idenitty

//signalR stuff
builder.Services.AddSignalR();
builder.Services.AddHostedService<ServerTimeNotifier>();
//end signalR stuff
//hangfire stuff
JobStorage.Current = new SqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddHangfire((sp, config) =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(cs);
});
builder.Services.AddHangfireServer();
builder.Services.AddTransient<JobPost24HourMark>();
//end hangfire
var app = builder.Build();
//map controllers
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var jobPost24HourMark = serviceProvider.GetRequiredService<JobPost24HourMark>();
    jobPost24HourMark.ConfigureHangfire();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseAuth();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the root URL
});
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapRazorPages();
app.UseHangfireDashboard();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/Hubs/NotificationHub");
});
app.Run();
