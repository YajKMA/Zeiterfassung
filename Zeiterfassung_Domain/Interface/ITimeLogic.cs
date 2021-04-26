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

       List<Time> SQLReadTimebyID(int id); //Email because the method ReadEmployeeByEmail is used to get the id


        void SQLAddTime(Time time, int id);

        void SQLUpdateTime(Time time);
        void SQLRemoveTime(int id);
    }
}
