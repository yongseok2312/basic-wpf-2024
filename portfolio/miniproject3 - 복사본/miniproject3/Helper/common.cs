using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject3.Helper
{
    public class Common
    {
        public static readonly string CONNSTRING = "Data Source=localhost;" +
                                                    "Initial Catalog=Map;" +
                                                    "Persist Security Info=True;" +
                                                    "User ID=sa;" +
                                                    "Encrypt=False;" +
                                                    "Password=mssql_p@ss";
    }
}
