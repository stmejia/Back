using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces
{
    public interface IAguilaMap
    {
        T Map<T>(object source);
    }
}
