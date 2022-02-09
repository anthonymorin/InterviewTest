using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest
{
    public interface IResult
    {
        int OrderNumber { get; }
        bool Success { get; }
        Exception? Exception { get; }
    }
}
