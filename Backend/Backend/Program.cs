var builder = WebApplication.CreateBuilder(args);
var openaiconf = new OpenAiConfig();
var value = System.Environment.GetEnvironmentVariable("OpenAI", EnvironmentVariableTarget.Machine);
builder.Services.Configure<OpenAiConfig>(options =>
{
    options.Key = openaiconf.Key;
});
//builder.Services.Configure<OpenAiConfig>(builder.Configur//ation.GetSection("OpenAI"));
//builder.Services.AddSingleton(openaiconf);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOpenAiService, OpenAiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
