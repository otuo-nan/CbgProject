namespace CbgTaxi24.API.Application.SeedWork
{
    public class HandlerResponse<T>
    {
        public T? Entity { get; set; }

        public HandlerResponse(T entity)
        {
            Entity = entity;
        }
    }
}
