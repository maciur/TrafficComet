# TrafficComet
Tool for saving information about (request/response) traffic from website written in .Net Core 2.1 to store in separate storage

## Simple Instalation 
#### Run command in Nuget Package Manager 
```csharp
Install-Package TrafficComet
``` 

#### Edit Startup file like below :
```csharp 
public class Startup
{
  	public IConfiguration Configuration { get; }
  
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc();
		services.AddTrafficComet(Configuration);
	}

	public void Configure(IApplicationBuilder app, IHostingEnvironment env)
	{
		app.UseTrafficComet();
		app.UseMvc();
	}
}
```

#### Add configuration to appsettings.js :
```json 
"TrafficComet": {
    "Middleware": {
      "Root": {
        "ApplicationId": "Application Name or Id"
      }
    },
    "TrafficLogWriter": {
      "PathToLogFolder": "Path to folder where you want to store all logs"
    }
  }
``` 
  
  
