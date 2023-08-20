namespace smart_dental_webapi.Boot
{
    public static class CorsBoot
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => options
                .AddPolicy("AllowAny", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));
        }
    }
}
