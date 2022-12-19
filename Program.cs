using Birdnest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Quartz;
using Birdnest.Jobs;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BirdnestContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("CollectSensorData");
    q.AddJob<CollectSensorData>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts   
        .ForJob(jobKey)
        .WithIdentity("CollectSensorData-trigger")
        .WithCronSchedule("*/2 * * * * ?")
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
app.Run();
