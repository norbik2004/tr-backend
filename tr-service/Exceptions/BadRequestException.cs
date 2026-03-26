using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_service.Exceptions
{
    public class BadRequestException(string message) : Exception(message)
    {
    }
}
