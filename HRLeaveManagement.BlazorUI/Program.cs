using HRLeaveManagement.BlazorUI;
using HRLeaveManagement.BlazorUI.Contracts;
using HRLeaveManagement.BlazorUI.Services;
using HRLeaveManagement.BlazorUI.Services.Base;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<IClient, Client>(Client=>Client.BaseAddress = new Uri("https://localhost:7228"));

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
