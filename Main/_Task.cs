using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Calendar.Main
{
    //DATABASE * FUNCTIONS *
    interface IDBfunctions
    {
        void Insert(int level,DateTime alarm,string content);
        void Edit();
        void Delete();
        void Read();
    }

    public class Task:IDBfunctions
    {
        //Properties
        public static List<Task> DbContent = new List<Task>();
        public int Date_Id { get; set; }
        public int Task_Id_Dates { get; set; }
        public int Task_Id_Tasks { get; set; }
        public int TaskLevel { get; set; }
        public DateTime Alarm { get; set; }
        public string TaskContent { get; set; }


        //ezt majd valahová lehet ellehet pakolni
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Datas.mdf;Integrated Security=True";
        SqlConnection sqlCon = new SqlConnection(connectionString);

        //IDBfunctions
        public void Insert(int level,DateTime alarm,string content)
        {
            string insertQuery = "insert into Dates(Date_ID) values(@dateid)";
            try
            {
                //Date Table
                sqlCon.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlCon))
                {
                    insertCommand.Parameters.AddWithValue("@dateid", Calendar.choosedDay);

                    insertCommand.ExecuteNonQuery();
                }
                Console.WriteLine("[Date] insertation is complete");


                //Task Table
                insertQuery = "insert into Tasks(Level,Alarm,TaskContent) values(@level,@alarm,@taskcontent)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlCon))
                {
                    insertCommand.Parameters.AddWithValue("@level",level);
                    insertCommand.Parameters.AddWithValue("@alarm", alarm);
                    insertCommand.Parameters.AddWithValue("@taskcontent", content);

                    insertCommand.ExecuteNonQuery();
                }
                Console.WriteLine("[Task] insertation is complete");
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                sqlCon.Close();
            }
            finally
            {
                sqlCon.Close();
            }
            
        }
        public void Edit()
        {

        }
        public void Delete()
        {

        }
        public void Read()
        {
           
        }
        public static void ReadOnLoad()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Calendar\Datas.mdf;Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(connectionString);

            string readQuery = "select * from Dates";
            try
            {
                //DATE table
                sqlCon.Open();
                using (SqlCommand readCommand = new SqlCommand(readQuery, sqlCon))
                {
                    SqlDataReader reader = readCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        DbContent.Add(new Task()
                        {
                            Task_Id_Dates = Convert.ToInt32(reader["Task_ID"]),
                            Date_Id= Convert.ToInt32(reader["Date_ID"])
                        });
                    }

                    reader.Close();
                }
                Console.WriteLine("[Date] table read complete");


                //TASK Table
                readQuery = "select * from Tasks";
                using (SqlCommand readCommand = new SqlCommand(readQuery, sqlCon))
                {
                    SqlDataReader reader = readCommand.ExecuteReader();

                    int count = 0;

                    while (reader.Read())
                    {
                        foreach (var item in DbContent)
                        {
                            if (item.Task_Id_Dates == Convert.ToInt32(reader["Id"]))
                            {
                                item.Task_Id_Tasks = Convert.ToInt32(reader["Id"]);
                                item.TaskLevel = Convert.ToInt32(reader["Level"]);
                                item.Alarm = Convert.ToDateTime(reader["Alarm"]);
                                item.TaskContent = reader["TaskContent"].ToString();
                            }
                        }
                    }

                    reader.Close();
                }
                Console.WriteLine("[Task] table read complete");
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                sqlCon.Close();
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public static void testData()
        {
            if(DbContent.Count == 0)
            {
                ReadOnLoad();
            }

            foreach (var item in DbContent)
            {
                Console.WriteLine(item.Task_Id_Dates+" "+item.TaskContent+" "+item.Task_Id_Tasks);
            }
        }
    }
    class InsertTaskInformations
    {
        public int Level { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public static InsertTaskInformations insertTaskInfo = new InsertTaskInformations();
    }
}

