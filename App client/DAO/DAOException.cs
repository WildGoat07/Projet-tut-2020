using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOException : Exception
    {
        public DAOException(string? message = null) : base(message)
        {
        }
    }
}