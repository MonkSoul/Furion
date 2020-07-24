using Fur.DatabaseAccessor.Identifiers;

namespace Fur.DatabaseAccessor.Models.Entities
{
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1>
        where TDbContextIdentifier1 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }

    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2>
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }

    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3>
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }

    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4>
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }

    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5>
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }

    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6>
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }

    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7>
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
        where TDbContextIdentifier7 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }

    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8> : DbNoKeyEntity, IDbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8>
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
        where TDbContextIdentifier7 : IDbContextIdentifier
        where TDbContextIdentifier8 : IDbContextIdentifier
    {
        public DbNoKeyEntityOfT(string entityName) : base(entityName) { }
    }
}