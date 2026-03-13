var builder = WebApplication.CreateBuilder(args);

//add services to the container

var app = builder.Build();

//configure the HTTPS request pipeline

app.Run();
