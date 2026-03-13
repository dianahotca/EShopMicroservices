var builder = WebApplication.CreateBuilder(args);

//add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services
    .AddMarten(options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("Database")!);
    })
    .UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

// configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
