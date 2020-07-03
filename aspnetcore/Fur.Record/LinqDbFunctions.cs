using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.Record
{
    public static class LinqDbFunctions
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
        [DbFunction(Name = "FN_GetId", Schema = "dbo")]
        public static int GetId(int id) => throw new NotSupportedException();
    }
}
