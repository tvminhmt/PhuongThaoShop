using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace PTS.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper();
            services.AddMediator(configuration);
            services.AddValidators();
           // IntAppSetting(configuration);
		}

        private static void AddAutoMapper(this IServiceCollection services)
        {
			services.AddAutoMapper(config =>
			{
				config.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t)); 
			});

			services.AddAutoMapper(Assembly.GetExecutingAssembly());
		}

        private static void AddMediator(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

				//cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

				////Add PipelineBehavior
				//if (configuration.GetValue<bool>("MediatorSettings:UsingPerformanceBehaviourPipeline"))
    //            {
    //                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
				//	cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
				//}
            });
        }

        private static void AddValidators(this IServiceCollection services)
        {
			services.AddFluentValidationClientsideAdapters();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

		//private static void IntAppSetting(IConfiguration configuration)
		//{
		//	AppConstant.Init(configuration);
		//}
	}
}
