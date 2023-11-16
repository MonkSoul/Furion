namespace Furion.Application;

public class TestRepositoryFactory : IDynamicApiController
{
    private readonly IRepositoryFactory<Person> _repositoryFactory;

    public TestRepositoryFactory(IRepositoryFactory<Person> repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public void Create()
    {
        using var repository = _repositoryFactory.CreateRepository();
        var persons = repository.DetachedEntities.ToList();

        using (var repository2 = _repositoryFactory.CreateRepository())
        {
            var persons2 = repository.DetachedEntities.ToList();
        }

        var persons3 = repository.DetachedEntities.ToList();
    }
}