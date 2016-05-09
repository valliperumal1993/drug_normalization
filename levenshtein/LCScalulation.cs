using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace levenshtein
{
    class LCScalulation
    {
        public static double
            
            GetLCS(string str1, string str2)
        {
            int[,] table;
            return GetLCSInternal(str1, str2, out table);
        }

        private static double GetLCSInternal(string str1, string str2, out int[,] matrix)
        {
            matrix = null;
            double normailsed_lcs;

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            int[,] table = new int[str1.Length + 1, str2.Length + 1];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                table[i, 0] = 0;
            }
            for (int j = 0; j < table.GetLength(1); j++)
            {
                table[0, j] = 0;
            }

            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                        table[i, j] = table[i - 1, j - 1] + 1;
                    else
                    {
                        if (table[i, j - 1] > table[i - 1, j])
                            table[i, j] = table[i, j - 1];
                        else
                            table[i, j] = table[i - 1, j];
                    }
                }
            }

            matrix = table;
            double x = 0;
            double score = table[str1.Length, str2.Length];
            double temp1 = score * score;
            double temp2 = str1.Length * str2.Length;
            normailsed_lcs = temp1 / temp2;
            return normailsed_lcs;
        }
    }
}






//if (score == str1.Length)
//{

//    if (str1.Length == str2.Length)
//    {
//        x = 1;
//    }



//    else
//    {
//        int y = str1.Length - str2.Length;
//        x = 1 / y;
//        x = Math.Abs(x);
//    }
//    z = x;

//}
//else
//{
//    z = score;
//}

// return table[str1.Length, str2.Length];
//  return z;