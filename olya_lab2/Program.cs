using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Olya.model;

var builder = WebApplication.CreateBuilder(args);

// Додайте послуги до контейнера.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<CoursePlatformContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CoursePlatformContext>();

var app = builder.Build();

// Налаштуйте конвеєр HTTP-запитів.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles(); // Це дозволяє обслуговувати index.html за замовчуванням

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();