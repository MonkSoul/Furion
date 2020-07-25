using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Contexts.Identifiers;
using System;

namespace Fur.Core.DbEntities
{
    public static class DbStaticFunctions
    {
        /// <summary>
        /// Linq中使用函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        // ================================
        //   CREATE FUNCTION FN_GetId
        //  (
        //      @id INT
        //  )
        //  RETURNS INT
        //  AS
        //  BEGIN
        //      RETURN @id + 1;
        //  END;
        // ================================
        [DbEFFunction("FN_GetId", "dbo", typeof(FurDbContextIdentifier))]
        public static int GetId(int id) => throw new NotSupportedException();
    }
}