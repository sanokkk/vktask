using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using VKTask.Auth;
using VKTask.DAL;
using VKTask.DAL.Interfaces;
using VKTask.DAL.Repos;
using VKTask.Service.Interfaces;
using VKTask.Service.Profiles;
using VKTask.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(l =>
{
    l.AddConsole();
});

builder.Services.AddAuthentication("BasicAuth").AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", null);

builder.Services.AddRouting(r =>
{
    r.LowercaseQueryStrings = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Pg")));

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
 = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupStateRepo, GroupStateRepo>();

builder.Services.AddAutoMapper(typeof(UserProfiles).Assembly);

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new UserProfiles(provider.GetService<IGroupStateRepo>()));
}).CreateMapper());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();

//FOR TESTING
public partial class Program { }
