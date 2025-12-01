var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
 {
    options. JsonSerializerOptions.ReferenceHandler = 
System.Text.Json.Serialization.ReferenceHandler.Preserve;
 }); 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
 var connectionString = 
builder.Configuration.GetConnectionString("WebRestOracleConnection");
 builder.Services.AddDbContext<WebRestOracleContext>
     (options => options.UseOracle(connectionString)
     .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

var loggerFactory = LoggerFactory.Create(builder =>
 {
 builder.AddFilter("Microsoft", LogLevel.Warning)
 .AddFilter("System", LogLevel.Warning)
 .AddFilter("SampleApp.Program", LogLevel.Debug)
 .AddConsole();
 });

#keep this in
 var mapperConfig = new MapperConfiguration(mc =>
 {
 mc.AddProfile(new MappingProfile());
 });
IMapper mapper = mapperConfig.CreateMapper();
 builder.Services.AddSingleton(mapper);




builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
