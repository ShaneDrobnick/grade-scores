using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grade_scores
{
    class UnitTests
    {
        public static void nameObjectTest()
        {
            Name name = new Name("Peter", "Johnson", 80);
            Console.WriteLine(name.First + " " + name.Last + " " + name.Score);
        }

    }
}
