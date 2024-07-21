var builder = WebApplication.CreateBuilder(args);
// Add-start
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var app = builder.Build();
// update-Start
//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");
// update end
app.UseStaticFiles();
app.UseSession();
app.Run();