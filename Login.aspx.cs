using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    students stObj = new students();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DataSet dsLoginID = stObj.FillDataSet("Select * from CollegeLogin where LoginID='"+txtLogin.Text+"' and LPassword='"+txtPassword.Text+"'");
        if (dsLoginID.Tables[0].Rows.Count != 0)
        {
            Response.Redirect("LandingScreen.aspx");
            //dsUniqueStudent.Tables[0].Rows[0].ItemArray[3].ToString();
        }
        else
        {
            lblMsg.Text = "Invalid User";
        }
    }
}