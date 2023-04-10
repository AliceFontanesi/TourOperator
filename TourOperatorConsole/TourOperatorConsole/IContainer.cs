using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourOperatorConsole
{
    interface IContainer
    {
        bool isEmpty();
        void makeEmpty();
        int size();
    }
}
