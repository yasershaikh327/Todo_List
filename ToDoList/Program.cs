using static ToDoList.Repository.RegistrationRepository;
using static ToDoList.Repository.TaskRepository;
using Quartz;
using ToDoList.Repository;
using static RealEstateWebsite.Repository.Home.UpdatePasswordRepository;
using static RealEstateWebsite.Repository.Admin.AdminUpdatePasswordRespository;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
/*Cron Job Service*/
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    var jobKey = new JobKey("AddRoomAutomaticRepository");
    q.AddJob<TaskService>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DemoJob-trigger")
        //.WithCronSchedule(builder.Services.));
        // .WithCronSchedule("0/5 14,18,3-39,52 * ? JAN,MAR,SEP MON-FRI 2002-2010"));
        .WithCronSchedule("*/59 * * * * ?"));

});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


builder.Services.AddScoped<IRegister, RegistrationService>();                              // RegistrationData
builder.Services.AddScoped<ITask, TaskService>();                                          // Task Data
builder.Services.AddScoped<IForgetPasswordService, ForgetPasswordService>();               // Update password Data
builder.Services.AddScoped<IAdminUpdatePasswordService, UpdatePassWordAdminService>();
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
