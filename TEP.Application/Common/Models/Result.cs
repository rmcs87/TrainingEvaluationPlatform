using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEP.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Suceceeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Suceceeded { get; set; }
        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }
        
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
