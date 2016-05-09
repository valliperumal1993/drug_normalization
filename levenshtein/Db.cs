using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace levenshtein
{
    class Db
    {
        public Dictionary<string, string> checkdictionary(Dictionary<string, string> list_cdm_desc)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "Select * from cdm";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            string temp_str, temp_str_1;
            while (dataReader.Read())
            {
                temp_str = dataReader["cdmcode"].ToString();
                temp_str_1 = dataReader["cdmwithoutspec"].ToString();
                list_cdm_desc.Add(temp_str.ToString().ToLower(), temp_str_1.ToString().ToLower());
            }

            dataReader.Close();
            command.Dispose();
            connection.Close();
            return list_cdm_desc;
        }
        public void dbaction1(List<string> list_pn, List<string> list_npn, Dictionary<string, string> list_cdm, Dictionary<string, string> list_cdm_term2, Dictionary<string, string> list_cdm_term3, Dictionary<string, string> list_cdm_term4)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString.ToString();
            string sql, sql1, sql2, sql3;
            SqlCommand command, command1, command2, command3;
            SqlDataReader dataReader, dataReader1, dataReader3;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            sql = "Select * from table_npn";
            command = new SqlCommand(sql, connection);
            // sql1 = "select * from cdm_new";
            sql1 = "select * from cdm_terms";
            command1 = new SqlCommand(sql1, connection);
            string temp_str, temp_str1;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                temp_str = dataReader["pn"].ToString();
                list_pn.Add(temp_str.ToLower());
                list_npn.Add(dataReader["npn"].ToString().ToLower());
            }
            dataReader.Close();
            command.Dispose();
            dataReader1 = command1.ExecuteReader();

            while (dataReader1.Read())
            {
                list_cdm.Add(dataReader1["column1"].ToString().ToLower(), dataReader1["column2"].ToString().ToLower());
                list_cdm_term2.Add(dataReader1["column1"].ToString().ToLower(), dataReader1["column3"].ToString().ToLower());
                list_cdm_term3.Add(dataReader1["column1"].ToString().ToLower(), dataReader1["column4"].ToString().ToLower());
                list_cdm_term4.Add(dataReader1["column1"].ToString().ToLower(), dataReader1["column5"].ToString().ToLower());
            }
            dataReader1.Close();
            command1.Dispose();
            connection.Close();
        }

        public void Insertdb(List<Tuple<string, string, string, int>> x)
        {
            string b=null;
                string c=null;
            string d=null;
            int e=0;
            string sql;
            foreach (var items in x)
            {
                    b=items.Item1;
                    c=items.Item2;
                    d = items.Item3;
                    e = items.Item4;
                    sql = "INSERT INTO dbo.LD (cdmcode,t1,pn,t1_pn) VALUES ('" + b + "','" + c + "','" + d + "'," + e + ")";

                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString.ToString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    //  SqlDataReader dataReader = command.ExecuteNonQuery();
                    command.ExecuteNonQuery();

                    // dataReader.Close();
                    command.Dispose();
                    connection.Close();
            }
         //   float me, me1, temp, temp1, temp2;
            //if (e == 0)
            //{
            //    me = 1;
            //    sql = "INSERT INTO dbo.LD (cdmcode,cdmdesc,pn,t1,t1_pn) VALUES ('" + b + "','" + c + "','" + d + "'," + e + "," + me + ")";
            //}
            //else
            //{
            //    temp = 10 * e;
            //    temp1 = 100 - temp;
            //    temp2 = e / temp1;
            //    me1 = 1 - temp2;

            //    sql = "INSERT INTO dbo.ld_term1_chk (cdmcode,cdmdesc,pn,ldtemp,ld) VALUES ('" + b + "','" + c + "','" + d + "'," + e + "," + me1 + ")";
            //}
            //sql = "INSERT INTO dbo.LD (cdmcode,t1,pn,t1_pn) VALUES ('" + b + "','" + c + "','" + d + "'," + e + ")";

            //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString.ToString();
            //SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();
            //SqlCommand command = new SqlCommand(sql, connection);
            ////  SqlDataReader dataReader = command.ExecuteNonQuery();
            //command.ExecuteNonQuery();

            //// dataReader.Close();
            //command.Dispose();
            //connection.Close();

        }
        public void Insertdb1(List<Tuple<string, string, string, string, int, int>> x)
        {
            //string b = null;
            //string c = null;
            //string d = null;
            //int e = 0;
            string sql;
            foreach (var items in x)
            {
                //b = items.Item1;
                //c = items.Item2;
                //d = items.Item3;
                //e = items.Item4;
                sql = "INSERT INTO dbo.LD (cdmcode,t1,t2,pn,t1_pn,t2_pn) VALUES ('" + items.Item1 + "'," + items.Item2 + "'," + items.Item3 + "'," + items.Item4 + "'," + items.Item5 + "'," + items.Item6 + ")";

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                //  SqlDataReader dataReader = command.ExecuteNonQuery();
                command.ExecuteNonQuery();

                // dataReader.Close();
                command.Dispose();
                connection.Close();
            }
        }

            public void fetchterms(List<Tuple<string, string, string, string, string>> h)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString.ToString();
            string sql1;
            SqlCommand  command1;
            SqlDataReader  dataReader1;
            SqlConnection connection = new SqlConnection(connectionString);
            Tuple<string, string, string, string, string> result;
            connection.Open();
            sql1 = "select * from cdm_terms";
            command1 = new SqlCommand(sql1, connection);
            dataReader1 = command1.ExecuteReader();

            while (dataReader1.Read())
            {
                result = Tuple.Create(dataReader1["column1"].ToString().ToLower(), dataReader1["column2"].ToString().ToLower(), dataReader1["column3"].ToString().ToLower(), dataReader1["column4"].ToString().ToLower(), dataReader1["column5"].ToString().ToLower());
                h.Add(result);
            }
           
            dataReader1.Close();
            command1.Dispose();
            connection.Close();
          
            }

            public void fetchndc(List<string> list_pn, List<string> list_npn)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString.ToString();
                string sql;
                SqlCommand command;
                SqlDataReader dataReader;
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();
                sql = "Select * from table_npn";
                command = new SqlCommand(sql, connection);
                string temp_str;
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    temp_str = dataReader["pn"].ToString();
                    list_pn.Add(temp_str.ToLower());
                    list_npn.Add(dataReader["npn"].ToString().ToLower());
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }

            public void Insert_final_pn(List<Tuple<string, string, string, double>> x)
            {
                //string b = null;
                //string c = null;
                //string d = null;
                //int e = 0;
                string sql;
                string sqlTrunc = "TRUNCATE TABLE final_pn;";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test2"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlTrunc, connection);
                cmd.ExecuteNonQuery();
                SqlCommand command;

                foreach (var items in x)
                {
                    sql = "INSERT INTO dbo.final_pn (cdmcode,cdmdesc,pn,score) VALUES ('" + items.Item1 + "','" + items.Item2 + "','" + items.Item3 + "'," + items.Item4 + ")";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                connection.Close();

            }

            public void Insert_final_npn(List<Tuple<string, string, string, double>> x)
            {
                //string b = null;
                //string c = null;
                //string d = null;
                //int e = 0;
                string sql;
                string sqlTrunc = "TRUNCATE TABLE final_npn;";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test2"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlTrunc, connection);
                cmd.ExecuteNonQuery();
                SqlCommand command;
                
                foreach (var items in x)
                {
                    sql = "INSERT INTO dbo.final_npn (cdmcode,cdmdesc,npn,score) VALUES ('" + items.Item1 + "','" + items.Item2 + "','" + items.Item3 + "'," + items.Item4 + ")";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                connection.Close();

            }

            public void Insert_combine(List<Tuple<string, string, string, double>> x, List<Tuple<string, string, string, double>> y)
            {
                //string b = null;
                //string c = null;
                //string d = null;
                //int e = 0;
                string sql,sql1;
                string sqlTrunc = "TRUNCATE TABLE combinedtable_new ;";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test2"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlTrunc, connection);
                cmd.ExecuteNonQuery();
                SqlCommand command,command1;

                foreach (var items in x)
                {
                    sql = "INSERT INTO dbo.combinedtable_new  (cdmcode,cdmdesc,ndc,score) VALUES ('" + items.Item1 + "','" + items.Item2 + "','" + items.Item3 + "'," + items.Item4 + ")";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                foreach (var items in y)
                {
                    sql1 = "INSERT INTO dbo.combinedtable_new (cdmcode,cdmdesc,ndc,score) VALUES ('" + items.Item1 + "','" + items.Item2 + "','" + items.Item3 + "'," + items.Item4 + ")";
                    command1 = new SqlCommand(sql1, connection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                }

                connection.Close();

            }

            public void Insert_total(List<Tuple<string, string, string, double>> x, List<Tuple<string, string, string, double>> y)
            {
                //string b = null;
                //string c = null;
                //string d = null;
                //int e = 0;
                string sql, sql1;
                string sqlTrunc = "TRUNCATE TABLE total ;";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test2"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlTrunc, connection);
                cmd.ExecuteNonQuery();
                SqlCommand command, command1;

                foreach (var items in x)
                {
                    sql = "INSERT INTO dbo.total  (cdmcode,cdmdesc,ndc,score) VALUES ('" + items.Item1 + "','" + items.Item2 + "','" + items.Item3 + "'," + items.Item4 + ")";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                foreach (var items in y)
                {
                    sql1 = "INSERT INTO dbo.combinedtable_new (cdmcode,cdmdesc,ndc,score) VALUES ('" + items.Item1 + "','" + items.Item2 + "','" + items.Item3 + "'," + items.Item4 + ")";
                    command1 = new SqlCommand(sql1, connection);
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                }

                connection.Close();

            }
    }
}