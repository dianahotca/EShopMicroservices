var builder = WebApplication.CreateBuilder(args);

//Add services to the container

var app = builder.Build();

//Configure HTTPS request pipeline

app.Run();
