using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourOperatorConsole
{
    interface IDictionary
    {
        void insert(IComparable key, object attribute);
        object find(IComparable key);
        object remove(IComparable key);
    }
}
