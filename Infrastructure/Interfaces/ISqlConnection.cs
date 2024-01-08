using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISqlConnection
    {
        SqlConnection Connection { get; }
        SqlTransaction SqlTransaction { get; }
    }
}
