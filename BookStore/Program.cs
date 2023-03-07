using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
string cs =@"Data Source=(localdb)\ProjectModels;Initial Catalog=BookStoreDb;Integrated Security=True";
builder.Services.AddDbContext<BookStoreDbContext>(op => op.UseSqlServer(cs));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
app.UseStaticFiles();
