using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace levenshtein
{
    class LDmain
    {
        public void ld()
        {
            Db databaseaccess = new Db();
            List<Tuple<string, string, string, string, string>> cdm_terms = new List<Tuple<string, string, string, string, string>>();
            databaseaccess.fetchterms(cdm_terms);
            
            //foreach (var items in cdm_terms)
            //{
            //    Console.WriteLine(items.Item1 + "," + items.Item2 + "," + items.Item3 + "," + items.Item4+","+items.Item5);
            //}
            System.IO.StreamWriter file = new System.IO.StreamWriter("E:\\check.txt");

            System.IO.StreamWriter file1 = new System.IO.StreamWriter("E:\\check1.txt");

            List<string> pn = new List<string>();
            List<string> npn = new List<string>();

            databaseaccess.fetchndc(pn, npn);
            LD l = new LD();
          //  List<string> xx = new List<string>();
          //  xx.Add("Prednisone");
          //  xx.Add("Prednisolone");
         //   xx.Add("tridione");
            //file1.WriteLine("haii");

             List<Tuple<string,string, string, double>> x = l.lev(cdm_terms, pn);
            List<Tuple<string,string, string, double>> x_npn = l.lev(cdm_terms, npn);
            // databaseaccess.Insert_final_pn(x);
           //databaseaccess.Insert_final_npn(x_npn);
         //   databaseaccess.Insert_combine(x,x_npn);
             databaseaccess.Insert_total(x,x_npn);

            //foreach (var items in x)
            //{
            //    //    file1.WriteLine("haii");
            //    file.WriteLine(items.Item1 + " -- " + items.Item2 + " -- " + items.Item3);
            //    // Console.WriteLine(items.Item1 + " " + items.Item2 + " " + items.Item3);
            //}

            //foreach (var items in x_npn)
            //{
            //    //    file1.WriteLine("haii");
            //    file1.WriteLine(items.Item1 + " --" + items.Item2 + "-- " + items.Item3);
            //    // Console.WriteLine(items.Item1 + " " + items.Item2 + " " + items.Item3);
            //}
        }
    }
}
