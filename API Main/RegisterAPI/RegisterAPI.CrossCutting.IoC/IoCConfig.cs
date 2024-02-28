using Microsoft.Extensions.DependencyInjection;
using RegisterAPI.Application;
using RegisterAPI.Application.Interface;
using RegisterAPI.CrossCutting.ExternalService;
using RegisterAPI.CrossCutting.ExternalService.Interface;
using RegisterAPI.CrossCutting.QueueService;
using RegisterAPI.CrossCutting.QueueService.Interface;
using RegisterAPI.Repository.Interface;
using RegisterAPI.Repository.Repository;
using RegisterAPI.Service;
using RegisterAPI.Service.Interface;

namespace RegisterAPI.CrossCutting.IoC
{
    public static class IoCConfig
    {
        public static void Config(this IServiceCollection services, string connectionString)
        {
            #region App
            services.AddTransient<IUserApp, UserApp>();
            services.AddTransient<INotificationApp, NotificationApp>();
            #endregion

            #region Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ISendMessageService, SendMessageService>();
            #endregion

            #region Repository
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
