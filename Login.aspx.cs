using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace EmployeeDetails_CmpyTest.Forms
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void getcon()
        {
            string qur = ConfigurationManager.ConnectionStrings["Demo"].ConnectionString;
            con = new SqlConnection(qur); 
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text.Trim();
                string password = txtpswd.Text.Trim();

                getcon();
                string strLogin = "SP_Login";
                con.Open();
                SqlCommand cmd = new SqlCommand(strLogin,con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username",username);
                cmd.Parameters.AddWithValue("@password", password);

                string user = cmd.ExecuteScalar().ToString() ;

                if (user != string.Empty)
                {
                    Response.Redirect("EmpDetails.aspx");
                }
                else 
                {
                    lblerror.Text = "Invalid Username and Password !!...";
                }
                con.Close();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}