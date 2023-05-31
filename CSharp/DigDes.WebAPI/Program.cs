using DigDes.UWP.Lib;
using DigDes.WebAPI.Lib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/text={text}", async context =>
{
	string text = context.Request.RouteValues["text"] as string ?? "";


	ClassWithPtivateMethod classWithPtivateMethod = new();
	var dictionary = classWithPtivateMethod.InvokeMethod("GetDictionary", text) as Dictionary<string, int> ?? new();

	string output = string.Empty;
	foreach (var item in dictionary)
	{
		output += $"{item.Key} - {item.Value}\n";
	}
	await context.Response.WriteAsync(output);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/time/text={text}", async context =>
{
	string text = context.Request.RouteValues["text"] as string ?? "";

	var proAndThr = new ProcessesAndThreed();

	string output = string.Empty;
	foreach (var item in proAndThr.GetTimeOfProcessingMethod(text))
	{
		output += item.Key + " - " + item.Value + "\n";
	}

	await context.Response.WriteAsync(output);
})
.WithName("time")
.WithOpenApi();


app.Run();
