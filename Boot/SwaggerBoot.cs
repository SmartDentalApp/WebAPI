namespace smart_dental_webapi.Boot
{
    public static class SwaggerBoot
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
