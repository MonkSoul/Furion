namespace Fur.DatabaseVisitor.Tangent
{
    public interface ITangentDbContext
    {
        TTangent For<TTangent>() where TTangent : class, ITangentQueryDependency;
    }
}