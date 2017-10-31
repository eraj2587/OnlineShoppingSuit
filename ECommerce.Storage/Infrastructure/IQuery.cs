namespace ECommerce.Storage.Infrastructure
{
    public interface IQuery<TResult>
    {

    }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        QueryResult<TResult> Handle(TQuery query);
    }
}