using Microsoft.Extensions.Options;

var AllowFrontendOrgin = "_allowFrontendOrigin";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowFrontendOrgin,
                             policy =>
                             {
                                 policy.WithOrigins("http://localhost:5173")
                                                     .AllowAnyHeader()
                                                     .AllowAnyMethod();
                             });
    //options.AddPolicy(name: AllowFrontendOrgin,
    //                  policy =>
    //                  {
    //                      policy.WithOrigins("http://localhost:5173");
    //                  });
    //options.AddDefaultPolicy(builder =>
    //{
    //    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
    //});
});



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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowFrontendOrgin);

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.Run();