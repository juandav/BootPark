using System;

namespace TolBarad
{
    class Evaluate
    {

        public string[] Parser(string expr)
        {
            string[] data = expr.Split(',');
            return data;
        }

    }
}
