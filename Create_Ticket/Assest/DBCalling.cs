using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Create_Ticket.Assest
{
    public class DBCalling
    {
        public DBCalling() { }

        public string LoginCredentials(string username, string password)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_logins", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                        sql.Open();

                        // Execute the command and retrieve the result
                        object result = cmd.ExecuteScalar();

                        // Check if the result is not null
                        if (result != null)
                        {
                            // If the login is successful, return the role ID
                            int roleId = Convert.ToInt32(result);
                            return "Login successful";
                        }
                        else
                        {
                            // If the login fails, return an appropriate message
                            return "Invalid username or password.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Return an appropriate message to the user
                return "An error occurred during login.";
            }
        }

        public void AddEmployeeDetails(Parameters cParams)
        {
            try
            {
                SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Inserting";
                cmd.Connection = sql;
                cmd.CommandType = CommandType.StoredProcedure;

                List<SqlParameter> lstPar = new List<SqlParameter>();

                lstPar.Add(new SqlParameter("@task", cParams.TaskName));
                lstPar.Add(new SqlParameter("@assby", cParams.AssignedBy));

                lstPar.Add(new SqlParameter("@assto", cParams.AssignedTo));
                lstPar.Add(new SqlParameter("@assdate", cParams.AssignedDate));

                lstPar.Add(new SqlParameter("@statusdate", cParams.StatusDate));
                lstPar.Add(new SqlParameter("@statusname", cParams.StatusName));
                lstPar.Add(new SqlParameter("@prio", cParams.Priority));

                foreach (SqlParameter p in lstPar)
                {
                    cmd.Parameters.Add(p);
                }
                sql.Open();
                cmd.ExecuteNonQuery();
                sql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal DataTable GetData(string query, bool isSP = true)
        {
            //throw new NotImplementedException();
            try
            {
                SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = sql;
                if (isSP)
                    cmd.CommandType = CommandType.StoredProcedure;
                else
                    cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal DataTable GetDataDetails(string query, int RoleId, string username, bool isSP)
        {
            // throw new NotImplementedException();
            try
            {
                SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = sql;

                if (isSP)
                    cmd.CommandType = CommandType.StoredProcedure;
                else
                    cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@RoleId", RoleId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        internal DataTable GetDataStatus(string query, string username, bool isSP)
        {
            // throw new NotImplementedException();
            try
            {
                SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = sql;

                if (isSP)
                    cmd.CommandType = CommandType.StoredProcedure;
                else
                    cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void UpdatedDetails(Parameters cParams)
        {
            try
            {
                SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UpdateDetails";
                cmd.Connection = sql;
                cmd.CommandType = CommandType.StoredProcedure;

                List<SqlParameter> lstPar = new List<SqlParameter>();
                lstPar.Add(new SqlParameter("@uid", cParams.Id));
                lstPar.Add(new SqlParameter("@statusname", cParams.StatusName));
                lstPar.Add(new SqlParameter("@statusdate", cParams.StatusDate));


                foreach (SqlParameter p in lstPar)
                {
                    cmd.Parameters.Add(p);
                }
                sql.Open();
                cmd.ExecuteNonQuery();
                sql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        internal int getStatusId(string status, string query)
        {
            DataTable dt = new DataTable();

            SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = sql;
            //if (isSP)
            cmd.CommandType = CommandType.StoredProcedure;
            //else
            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            cmd.Parameters.Add(status);
            //  cmd.ExecuteNonQuery();
            return cmd.ExecuteNonQuery();

        }
        internal int getUserRoleId(string query, string username)
        {
            // throw new NotImplementedException();
            DataTable dt = new DataTable();

            SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = sql;
            //if (isSP)
            cmd.CommandType = CommandType.StoredProcedure;
            //else
            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            cmd.Parameters.Add(new SqlParameter("@username", username));
            sql.Open();
            Object id = cmd.ExecuteScalar();
            int RoleId = Convert.ToInt32(id);
            //  cmd.ExecuteNonQuery();
            return RoleId;
        }

        internal DataTable GetDataBsedOnSelectedIndex(int userId, int taskId, int priorityId, int statusId)
        {
            try
            {
                SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetDataBasedOnIndex";
                //  cmd.CommandText = "SELECT * FROM TicketDetails WHERE (@userName = 0 OR [Assigned To] = @userName) AND (@TaskName = 0 OR [Task Name] = @TaskName) AND (@Priority = 0 OR [Priority] = @Priority) AND (@Status = 0 OR [Status Name] = @Status)";
                cmd.Connection = sql;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", userId);
                cmd.Parameters.AddWithValue("@TaskName", taskId);
                cmd.Parameters.AddWithValue("@Priority", priorityId);
                cmd.Parameters.AddWithValue("@Status", statusId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;

            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        internal DataTable DateCalling(DateTime fD, DateTime tD, int reportId, int statusId, int prioritId)
        {
            // throw new NotImplementedException();
            SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetValueBasedOnDate";
            cmd.Connection = sql;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FD", fD);
            cmd.Parameters.AddWithValue("@TD", tD);
            cmd.Parameters.AddWithValue("@ReportId", reportId);
            cmd.Parameters.AddWithValue("@stId", statusId);
            cmd.Parameters.AddWithValue("@PrId", prioritId);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public DataTable Card(string query, bool isSP)
        {
            try
            {
                DataTable Dt = new DataTable();
                SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = sql;
                if (isSP)
                    cmd.CommandType = CommandType.StoredProcedure;
                else
                    cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(Dt);
                return Dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}