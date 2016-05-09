using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace levenshtein
{
    class Dicecoefficientcalculation
    {
        public static double DC(string stOne, string stTwo)
        {
            HashSet<string> nx = new HashSet<string>();
            HashSet<string> ny = new HashSet<string>();

            for (int i = 0; i < stOne.Length - 1; i++)
            {
                char x1 = stOne[i];
                char x2 = stOne[i + 1];
                string temp = x1.ToString() + x2.ToString();
                nx.Add(temp);
            }
            for (int j = 0; j < stTwo.Length - 1; j++)
            {
                char y1 = stTwo[j];
                char y2 = stTwo[j + 1];
                string temp = y1.ToString() + y2.ToString();
                ny.Add(temp);
            }

            HashSet<string> intersection = new HashSet<string>(nx);
            intersection.IntersectWith(ny);

            double dbOne = intersection.Count;
            return (2 * dbOne) / (nx.Count + ny.Count);
        }
    }
}
