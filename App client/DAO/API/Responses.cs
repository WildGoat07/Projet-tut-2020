using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    internal class DeleteResponse
    {
        public error[] errors;

        public int rowsDeleted;
        public bool success;

        public DeleteResponse()
        {
            rowsDeleted = 0;
            errors = Array.Empty<error>();
            success = true;
        }
    }

    internal class Response<T>
    {
        public error[] errors;

        public bool success;

        public T[] values;

        public Response()
        {
            values = Array.Empty<T>();
            errors = Array.Empty<error>();
            success = true;
        }
    }
}