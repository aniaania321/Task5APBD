using Tutorial3_Task;

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

FileService fileService = new FileService("/Users/aniasmuga/RiderProjects/Task5APBD/Task5APBD/input.txt");
DeviceParser deviceParser = new DeviceParser();
DeviceManager dm=DeviceManagerFactory.CreateDeviceManager(fileService, deviceParser);

PersonalComputer pc = new PersonalComputer("P-1", "PC", true, "Windows");

app.MapGet("/api/animals", () => dm.GetAllDevices());
//app.MapGet("/api/animals/{id}", (int id) => animals[id-1]);
app.MapPost("/api/animals", (Smartwatch device) =>
{
    dm.AddDevice(device);
    return Results.Created($"/api/animals/{device.Id}", device);
});
/*app.MapPut("/api/animals/{id}", (int id,Animal animal) => animals[id-1]=animal);
app.MapDelete("/api/animals/{id}", (int id) => animals.RemoveAt(id-1));*/

app.Run();