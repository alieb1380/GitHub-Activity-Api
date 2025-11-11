using GitHubActivityApi.Services; // برای IGitHubService

// ⭐️ ارجاعات لازم برای کلاس‌های اصلی ASP.NET Core
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


// ⭐️ اگر از Implicit Usings استفاده نمی‌کنید، ممکن است به این موارد هم نیاز باشد:
// using System.Net.Http; 
// using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

// Add CORS Policy (همانطور که قبلا توافق شد، باید اضافه شود)
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// -------------------------------------------------------------------


// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddScoped<IGitHubService, GitHubService>();

// برای فعال کردن کنترلرها
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // این قسمت‌ها مربوط به Swagger است که فعلا غیرفعال شده
}

// ⭐️ استفاده از CORS قبل از UseHttpsRedirection
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

// برای مسیریابی کنترلرها
app.MapControllers();

app.Run();