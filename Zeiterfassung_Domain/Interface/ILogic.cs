using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassung_Domain.Interface
{
    public interface ILogic
    {

        int CountRows();

        void CheckRows();


        void OpenConnection(MySqlConnection DBConnect);

    }
}
