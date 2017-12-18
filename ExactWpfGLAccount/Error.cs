using System;

namespace Exact
{
    public class ExactError : Exception
    {
        public ExactError() { }

        public ExactError(string s) : base(s) { }
    }
}
