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
    public partial class EmpDetails : System.Web.UI.Page
    {
        SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Gridbind();
            }
        }

        public void getcon()
        {
            string qur = ConfigurationManager.ConnectionStrings["Demo"].ConnectionString;
            con = new SqlConnection(qur);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string qur = "insert into EmpDetails(FirstName,MiddleName,LastName) values('" + txtFirstName.Text + "','" +txtMiddleName.Text + "','" + txtlastname.Text + "')";
                getcon();
                SqlCommand cmd = new SqlCommand(qur,con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Gridbind();
                clearAll();
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        public void Gridbind()
        {
            try
            {
                getcon();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select EmpCode,FirstName,MiddleName,LastName from EmpDetails", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                GridView1.DataSource = dt;
                ViewState["dt"] = dt;
                bindgrid();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void bindgrid()
        {
            try
            {
                GridView1.DataSource = ViewState["dt"] as DataTable;
                GridView1.DataBind();               
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void clearAll()
        {
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtlastname.Text = "";

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Gridbind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Gridbind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            int id = Convert.ToInt16(row.Cells[0].Text.ToString());
            TextBox name = (TextBox)row.Cells[1].Controls[0];
            TextBox middlename = (TextBox)row.Cells[2].Controls[0];
            TextBox lastname = (TextBox)row.Cells[3].Controls[0];
            getcon();
            con.Open();
            SqlCommand cmd = new SqlCommand("Update [dbo].[EmpDetails] set FirstName='" + name.Text + "', MiddleName='" + middlename.Text + "', LastName='" + lastname.Text + "' where EmpCode=" + Convert.ToInt32(id), con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            Gridbind();

        }
    }
}