using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grade_scores
{
    public class Name
    {
       public string First { get; }
       public string Last { get; }
       public int Score { get; }

        public Name(string first, string last, int score)
        {
            this.First = first;
            this.Last = last;
            this.Score = score;
        }
    }
}
