namespace BusinessManager.Application.Interfaces
{
    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }
}
