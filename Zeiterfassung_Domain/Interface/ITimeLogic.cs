using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassung_Domain.Interface
{
    public interface ITimeLogic : ILogic
    {
        List<Time> SQLReadTime();

        Time SQLReadTimebyID(int id);

        void SQLAddTime(Time time);

        void SQLUpdateTime(Time time);
        void SQLRemoveTime(int id);
    }
}
