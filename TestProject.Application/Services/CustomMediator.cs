using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject.Application.Services
{
    public class CustomMediator : Mediator
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomMediator(ServiceFactory serviceFactory, IServiceProvider serviceProvider) : base(serviceFactory)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task PublishCore(IEnumerable<Func<INotification, CancellationToken, Task>> allHandlers, INotification notification, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());

                var handler = scope.ServiceProvider.GetService(handlerType);

                var handleMethod = handlerType.GetMethod(nameof(INotificationHandler<INotification>.Handle));
                handleMethod.Invoke(handler, new object[] { notification, CancellationToken.None } );
            });

            return Task.CompletedTask;
        }
    }
}