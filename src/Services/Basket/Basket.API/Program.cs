var builder = WebApplication.CreateBuilder(args);

//add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services
    .AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    //specify the primary key for ShoppingCart table
    options.Schema.For<ShoppingCart>().Identity(Cart => Cart.Username);
})
    .UseLightweightSessions();

var app = builder.Build();

//configure the HTTPS request pipeline
app.MapCarter();

app.Run();
