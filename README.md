# aspnetcore-dynamic-cors
aspnetcore dynamic cors

## In case we use services.AddDynamicCorsDefault();

We configure middleware in `appsettings.json` for the first step.
``` 
{
  "DynamicCors": {
    "GrantedOrigins": [ "http://localhost:6701", "http://localhost:6702" ],
    "ForbiddenOrigins": [ "http://localhost:6801", "http://localhost:6802" ]
  }
}
```

Then we use the middleware in `startup.cs`.
```
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DynamicCorsOptions>(Configuration.GetSection("DynamicCors"));
            services.AddDynamicCorsDefault();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseDynamicCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
```

## In case we use services.AddDynamicCorsSnapshot();

We configure middleware in `appsettings.json` for the first step.
``` 
{
  "DynamicCors": {
    "GrantedOrigins": [ "http://localhost:6701", "http://localhost:6702" ],
    "ForbiddenOrigins": [ "http://localhost:6801", "http://localhost:6802" ]
  }
}
```

startup.cs
Then we use the middleware in `startup.cs`.
```
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DynamicCorsOptions>(Configuration.GetSection("DynamicCors"));
            services.AddDynamicCorsSnapshot();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseDynamicCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
```

## In case we use services.AddDynamicCorsSingleton<CustomDynamicCorsPolicyService>();

We add `CustomDynamicCorsPolicyService` and its dependent， and then we use the middleware.
```
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SampleDBContext>();
            services.AddDynamicCorsSingleton<CustomDynamicCorsPolicyService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
```
