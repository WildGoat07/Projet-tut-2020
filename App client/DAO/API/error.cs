using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    internal class error
    {
        public string error_code;

        public string error_desc;

        public error()
        {
            error_code = "00000";
            error_desc = "";
        }
    }
}