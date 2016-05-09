using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace levenshtein
{
    class LD
    {
        public List<Tuple<string,string, string, double>> lev(List<Tuple<string, string, string, string, string>> cdm_terms, List<string> list_pn)//Dictionary<String, String> input, List<string> list_pn)//, System.IO.StreamWriter file, System.IO.StreamWriter file1)
            {
                Googleapi googleapi = new Googleapi();
              //  string api_result = null;
                list_pn = list_pn.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();//RemoveAll(x => x == null);
                List<string> newcdm = new List<string>();

                double x,x_ld=0,x1_ld=0,x2_ld=0,x3_ld=0,x4_ld=0;
                double x_dc, x1_dc, x2_dc, x3_dc, x4_dc;
                double x_dam, x1_dam, x2_dam, x3_dam, x4_dam;
                double x_jw, x1_jw, x2_jw, x3_jw, x4_jw;
                double x_LCS, x1_LCS, x2_LCS, x3_LCS, x4_LCS;
                double[] index = new double[10];

                List<double> temp = new List<double>();
                List<double> distinct1 = new List<double>();
                Tuple<string,string, string, double> result;
                var h = new List<Tuple<string,string, string, double>>();

                Tuple<string, string, double> result1;
                var h1 = new List<Tuple<string, string, double>>();
                string[] ssizes=null;
                double score_ld_x, score_dc_x_dc, score_dam_x_dam, score_jw_x_jw;
                double score_ld_x1=0,score_ld_x2=0,score_ld_x3=0,score_ld_x4=0;
                double score_dc_x1 = 0, score_dc_x2 = 0, score_dc_x3 = 0, score_dc_x4 = 0;
                double consolidated_score_ld = 0, consolidated_score_dc = 0, consolidated_score_jw = 0, consolidated_score_lcs=0;
                double final_score=0;
                double final_consolidated_score=0;
              
                string term1, term2, term3, term4,append=null,cdmcode=null,cdmterm=null;

                for (int i = 0; i < cdm_terms.Count; i++)
                {
                    var item = cdm_terms.ElementAt(i);

                    cdmcode = item.Item1;
                    term1 = item.Item2;
                    term2 = item.Item3;
                    term3 = item.Item4;
                    term4 = item.Item5;
                    cdmterm = (term1 + " " + term2 + " " + term3 + " " + term4).ToLower().Trim();
                    
                    for (int j = 0; j < list_pn.Count; j++)
                    {
                        x_ld = LDcalc.Compute(cdmterm.ToLower(), list_pn[j].ToLower());
                        x_dc = Dicecoefficientcalculation.DC(cdmterm.ToLower(), list_pn[j].ToLower());
                        x_jw = Jarowinklercalculation.distance(cdmterm.ToLower(), list_pn[j].ToLower());
                        x_LCS = LCScalulation.GetLCS(cdmterm.ToLower(), list_pn[j].ToLower());

                        if (term2.Equals(""))
                        {
                            consolidated_score_ld = 0;
                          //  String term1 = googleapi.api(term1.ToLower());
                            x1_ld = LDcalc.Compute(term1.ToLower(), list_pn[j].ToLower());
                            
                            score_ld_x1 = x1_ld;

                            x1_dc = Dicecoefficientcalculation.DC(term1.ToLower(), list_pn[j].ToLower());
                            x1_jw = Jarowinklercalculation.distance(term1.ToLower(), list_pn[j].ToLower());
                            x1_LCS = LCScalulation.GetLCS(term1.ToLower(), list_pn[j].ToLower());

                            append =term1;
                            if ((score_ld_x1 > 0.60) && (x1_dc > 0.60) && (x1_jw > 0.60) && (x1_LCS > 0.60))
                            {
                                consolidated_score_ld = (score_ld_x1);// +(0.5) * (score_ld_x2);
                                consolidated_score_dc = (x1_dc);// +(0.5) * (x2_dc);
                                consolidated_score_jw = (x1_jw);
                                consolidated_score_lcs = (x1_LCS);
                                final_consolidated_score = (consolidated_score_ld + consolidated_score_dc + consolidated_score_jw + consolidated_score_lcs) / 4;
                                result = Tuple.Create(cdmcode, append, list_pn[j], final_consolidated_score);
                                h.Add(result);
                            }
                            
                        }
                        
                        else if (term3.Equals(""))
                        {
                                consolidated_score_ld = 0;
                                append = (term1 + " " + term2).Trim();
                                //String term1 = googleapi.api(term1.ToLower());
                                //String term2 = googleapi.api(term2.ToLower());

                                x1_ld = LDcalc.Compute(term1.ToLower(), list_pn[j].ToLower());
                                x2_ld = LDcalc.Compute(term2.ToLower(), list_pn[j].ToLower());
                           
                                score_ld_x1 = x1_ld;
                                score_ld_x2 = x2_ld;

                                x1_dc = Dicecoefficientcalculation.DC(term1.ToLower(), list_pn[j].ToLower());
                                x2_dc = Dicecoefficientcalculation.DC(term2.ToLower(), list_pn[j].ToLower());

                                x1_jw = Jarowinklercalculation.distance(term1.ToLower(), list_pn[j].ToLower());
                                x2_jw = Jarowinklercalculation.distance(term2.ToLower(), list_pn[j].ToLower());

                                x1_LCS = LCScalulation.GetLCS(term1.ToLower(), list_pn[j].ToLower());
                                x2_LCS = LCScalulation.GetLCS(term2.ToLower(), list_pn[j].ToLower());

                                if (x_ld==1)
                            {
                                final_consolidated_score = 1;
                            }
                                else if ((x_ld > 0.60) || (x_dc > 0.60) && (x_jw > 0.60) || (x_LCS > 0.60))
                                {
                                    if ((score_ld_x1 > 0.7 && score_ld_x2 > 0.4) || (x1_dc > 0.75 && x2_dc > 0.4) || (x1_jw > 0.75 && x2_jw > 0.4) || (x1_LCS > 0.75 && x2_LCS > 0.4))
                                    {
                                        consolidated_score_ld = (0.5) * (score_ld_x1) + (0.5) * (score_ld_x2);
                                        consolidated_score_dc = (0.5) * (x1_dc) + (0.5) * (x2_dc);
                                        consolidated_score_jw = (0.5) * (x1_jw) + (0.3) * (x2_jw);// +(0.1) * (x3_jw) + (0.1) * (x4_jw);
                                        consolidated_score_lcs = (0.5) * (x1_LCS) + (0.3) * (x2_LCS);
                                        
                                        final_consolidated_score = (consolidated_score_ld + consolidated_score_dc + consolidated_score_jw + consolidated_score_lcs) / 4;
                                        result = Tuple.Create(cdmcode, append, list_pn[j], final_consolidated_score);
                                        h.Add(result);
                                    }
                                   
                                }
                    }
                         
                          else  if (term4.Equals(""))//term 4
                        {
                            consolidated_score_ld = 0;
                            append = (term1 + " " + term2 + " " + term3).Trim();
                            //String term1 = googleapi.api(term1.ToLower());
                            //String term2 = googleapi.api(term2.ToLower());
                            //String term3 = googleapi.api(term3.ToLower());

                            x1_ld = LDcalc.Compute(term1.ToLower(), list_pn[j].ToLower());
                            x2_ld = LDcalc.Compute(term2.ToLower(), list_pn[j].ToLower());
                            x3_ld = LDcalc.Compute(term3.ToLower(), list_pn[j].ToLower());

                            x1_dc = Dicecoefficientcalculation.DC(term1.ToLower(), list_pn[j].ToLower());
                            x2_dc = Dicecoefficientcalculation.DC(term2.ToLower(), list_pn[j].ToLower());
                            x3_dc = Dicecoefficientcalculation.DC(term3.ToLower(), list_pn[j].ToLower());

                            x1_jw = Jarowinklercalculation.distance(term1.ToLower(), list_pn[j].ToLower());
                              x2_jw = Jarowinklercalculation.distance(term2.ToLower(), list_pn[j].ToLower());
                              x3_jw = Jarowinklercalculation.distance(term3.ToLower(), list_pn[j].ToLower());

                              x1_LCS = LCScalulation.GetLCS(term1.ToLower(), list_pn[j].ToLower());
                              x2_LCS = LCScalulation.GetLCS(term2.ToLower(), list_pn[j].ToLower());
                              x3_LCS = LCScalulation.GetLCS(term3.ToLower(), list_pn[j].ToLower());

                              score_ld_x1 = x1_ld;
                              score_ld_x2 = x2_ld;
                              score_ld_x3 = x3_ld;
                           //  if ((x_ld > 0.80) || (x_dc > 0.70) && (x_jw > 0.70) || (x_LCS > 0.80))
                              if (x_ld == 1)
                              {
                                  final_consolidated_score = 1;
                                  result = Tuple.Create(cdmcode, append, list_pn[j], final_consolidated_score);
                                  h.Add(result);
                              }
                              else if ((x_ld > 0.60) || (x_dc > 0.60) && (x_jw > 0.60) || (x_LCS > 0.60))
                              {
                                  if (((score_ld_x1 > 0.5 && score_ld_x2 > 0.5) || (score_ld_x3 > 0.2)) || ((x1_dc > 0.85 && x2_dc > 0.85) || (x1_dc > 0.25)) || ((x1_jw > 0.80 && x2_jw > 0.80) || (x1_jw > 0.25)) || ((x1_LCS > 0.80 && x2_LCS > 0.80) || (x1_LCS > 0.2)))
                                  {

                                      consolidated_score_ld = (0.5) * (score_ld_x1) + (0.4) * (score_ld_x2) + (0.1) * (score_ld_x3);
                                      consolidated_score_dc = (0.5) * (x1_dc) + (0.4) * (x2_dc) + (0.1) * (x3_dc);
                                      consolidated_score_jw = (0.5) * (x1_jw) + (0.3) * (x2_jw) + (0.1) * (x3_jw);// +(0.1) * (x4_jw);
                                      consolidated_score_lcs = (0.5) * (x1_LCS) + (0.3) * (x2_LCS) + (0.1) * (x3_LCS);

                                      final_consolidated_score = (consolidated_score_ld + consolidated_score_dc + consolidated_score_jw + consolidated_score_lcs) / 4;

                                      result = Tuple.Create(cdmcode, append, list_pn[j], final_consolidated_score);
                                      h.Add(result);
                                  }
                                 
                              }

                          }
                        else
                        {
                            consolidated_score_ld = 0;
                            append = (term1 + " " + term2 + " " + term3 + " " + term4).Trim();
                            //String term1 = googleapi.api(term1.ToLower());
                            //String term2 = googleapi.api(term2.ToLower());
                            //String term3 = googleapi.api(term3.ToLower());
                            //String term4 = googleapi.api(term4.ToLower());

                            x1_ld = LDcalc.Compute(term1, list_pn[j].ToLower());
                            x2_ld = LDcalc.Compute(term2, list_pn[j].ToLower());
                            x3_ld = LDcalc.Compute(term3, list_pn[j].ToLower());
                            x4_ld = LDcalc.Compute(term4, list_pn[j].ToLower());

                            x1_dc = Dicecoefficientcalculation.DC(term1.ToLower(), list_pn[j].ToLower());
                            x2_dc = Dicecoefficientcalculation.DC(term2.ToLower(), list_pn[j].ToLower());
                            x3_dc = Dicecoefficientcalculation.DC(term3.ToLower(), list_pn[j].ToLower());
                            x4_dc = Dicecoefficientcalculation.DC(term4.ToLower(), list_pn[j].ToLower());

                            x1_jw = Jarowinklercalculation.distance(term1.ToLower(), list_pn[j].ToLower());
                            x2_jw = Jarowinklercalculation.distance(term2.ToLower(), list_pn[j].ToLower());
                            x3_jw = Jarowinklercalculation.distance(term3.ToLower(), list_pn[j].ToLower());
                            x4_jw = Jarowinklercalculation.distance(term4.ToLower(), list_pn[j].ToLower());

                            x1_LCS = LCScalulation.GetLCS(term1.ToLower(), list_pn[j].ToLower());
                            x2_LCS = LCScalulation.GetLCS(term2.ToLower(), list_pn[j].ToLower());
                            x3_LCS = LCScalulation.GetLCS(term3.ToLower(), list_pn[j].ToLower());
                            x4_LCS = LCScalulation.GetLCS(term4.ToLower(), list_pn[j].ToLower());

                            score_ld_x1 = x1_ld;
                            score_ld_x2 = x2_ld;
                            score_ld_x3 = x3_ld;
                            score_ld_x4 = x4_ld;

                              if(x_ld==1)
                              {
                                  final_consolidated_score = 1;
                                  result = Tuple.Create(cdmcode, append, list_pn[j], final_consolidated_score);
                                  h.Add(result);
                              }
                              else if ((x_ld > 0.60) || (x_dc > 0.60) && (x_jw > 0.60) || (x_LCS > 0.60))
                            {
                                if (((score_ld_x1 > 0.5 && score_ld_x2 > 0.5) || (score_ld_x3 > 0.2 && score_ld_x4 > 0.1)) || ((x1_dc > 0.85 && x2_dc > 0.85) || (x3_dc > 0.2 || x4_dc > 0.1)) || ((x1_jw > 0.80 && x2_jw > 0.80) || (x3_jw > 0.2 || x4_jw > 0.1)) || ((x1_LCS > 0.80 && x2_LCS > 0.80) || (x3_LCS > 0.2 ||x4_LCS > 0.1)))
                                {

                                    consolidated_score_ld = (0.5) * (score_ld_x1) + (0.3) * (score_ld_x2) + (0.1) * (score_ld_x3) + (0.1) * (score_ld_x4);
                                    consolidated_score_dc = (0.5) * (x1_dc) + (0.3) * (x2_dc) + (0.1) * (x3_dc) + (0.1) * (x4_dc);
                                    consolidated_score_jw = (0.5) * (x1_jw) + (0.3) * (x2_jw) + (0.1) * (x3_jw) + (0.1) * (x4_jw);
                                    consolidated_score_lcs = (0.5) * (x1_jw) + (0.3) * (x2_jw) + (0.1) * (x3_jw) + (0.1) * (x4_jw);
                                    consolidated_score_lcs = (0.5) * (x1_LCS) + (0.3) * (x2_LCS) + (0.1) * (x3_LCS) + (0.1) * (x4_LCS);

                                    final_consolidated_score = (consolidated_score_ld + consolidated_score_dc + consolidated_score_jw + consolidated_score_lcs) / 4;
                                }
                                result = Tuple.Create(cdmcode, append, list_pn[j], final_consolidated_score);
                                h.Add(result);
                            }
                        }
                }
                   }
                return h;
            }
        

    }
}
