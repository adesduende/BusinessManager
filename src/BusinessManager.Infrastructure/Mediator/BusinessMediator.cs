using BusinessManager.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessManager.Infrastructure.Mediator
{
    public class BusinessMediator : IMediator
    {
        private readonly IServiceProvider _provider;

        public BusinessMediator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _provider.CreateScope();

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = scope.ServiceProvider.GetService(handlerType);
            if (handler == null) throw new InvalidOperationException("No handler found for this operation");

            var method = handlerType.GetMethod("HandleAsync");
            if (method == null) throw new InvalidOperationException("No method found for this type");

            var result = method.Invoke(handler, new[] { request });
            if (result == null) throw new InvalidOperationException("No results for this operation");

            return await (Task<TResponse>)result;
        }
    }
}
