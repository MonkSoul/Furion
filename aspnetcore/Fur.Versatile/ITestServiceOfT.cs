using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fur.Versatile
{
    public interface ITestServiceOfT<T>
    {
        string GetName();
    }
}
