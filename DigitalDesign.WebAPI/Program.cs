using DigitalDesign.ClassLib;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

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

app.MapGet("/text={text}", async context =>
{
	string text = context.Request.RouteValues["text"] as string ?? "";
	var task = WorkWithString.GetDictionaryAsync(text);
	var dict = task.Result;
	string output = string.Empty;
	foreach (var item in dict)
	{
		output += $"{item.Key} - {item.Value}\n";
	}
	await context.Response.WriteAsync(output);
}).WithName("test");

app.MapGet("/time/text={text}", async context =>
{
	string text = context.Request.RouteValues["text"] as string ?? "";

	var exampleType = typeof(WorkWithString);
	MethodInfo method = exampleType.GetMethod("GetDictionary", BindingFlags.NonPublic | BindingFlags.Static)!;
	var dict = method.Invoke(null, new object[] { text }) as Dictionary<string, int>;
	await WorkWithString.GetDictionaryAsync(text);
	WorkWithString.GetDictionaryThread(text);
	WorkWithString.GetDictionaryParallel(text);

	string output = $"Regular: {WorkWithString.RegularTime} ms\n" +
	$"Task: {WorkWithString.TaskAsyncTime} ms\n" +
	$"Thread: {WorkWithString.ThreadTime} ms\n" +
	$"Parallel: {WorkWithString.ParallelTime} ms";

	await context.Response.WriteAsync(output);
}).WithName("time");

app.Run();