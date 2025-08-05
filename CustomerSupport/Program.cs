using Microsoft.EntityFrameworkCore;
using CustomerSupport.Constants;
using CustomerSupport.Data.Json;
using CustomerSupport.DataBaseConnection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); 
string importedData = new JsonDataReceiver().GetData();
ConstantsCollector? collector = JsonSerializer.Deserialize<ConstantsCollector>(importedData);
Console.WriteLine(collector.DBConnectionString);
if (collector != null && !string.IsNullOrWhiteSpace(collector.DBConnectionString))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(collector.DBConnectionString));

}
else
{
    throw new Exception("Cannot connect to the Database");
}
builder.Services.AddControllersWithViews();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
