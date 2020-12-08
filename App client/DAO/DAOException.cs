using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOException : Exception
    {
        public DAOException(string? message) : base(message) => Code = ErrorCode.UNKNOWN;

        public DAOException() : base() => Code = ErrorCode.UNKNOWN;

        public DAOException(string? message, ErrorCode code) : base(message) => Code = code;

        public DAOException(ErrorCode code) : base() => Code = code;

        public enum ErrorCode
        {
            UNKNOWN,
            MISSING_ENTRY,
            ENTRY_LINKED,
            EXISTING_ENTRY
        }

        public ErrorCode Code { get; init; }

        public override string ToString() => $"{Code} : {base.ToString()}";
    }
}