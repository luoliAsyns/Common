using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum EResponseCode
    {

        Success = 200,

        BadRequest = 400,
        Unauthorized = 401,
        ValidationError= 402,
        NotFound = 404,

        Fail = 500,

    }
}
