using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillGrid("Select * from Students");
    }
    public void FillGrid(string Select)
    {
        DataSet dsStudents = stObj.FillDataSet(Select);
        if (dsStudents.Tables[0].Rows.Count != 0)
        {
            dataGridView1.DataSource = dsStudents;
            dataGridView1.DataBind();
        }
    }

    students stObj = new students();
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsUniqueStudent = SearchID();
        int St = 0;
        if (dsUniqueStudent.Tables[0].Rows.Count == 0)
        {
            St = stObj.SaveData("",txtName.Text, txtPh.Text, txtEMail.Text,0);
        }
        else if (dsUniqueStudent.Tables[0].Rows.Count == 1)
        {
            St = stObj.SaveData(txtSearch.Text,txtName.Text, txtPh.Text, txtEMail.Text,1);
        }
        if (St == 1)
        {
            Label1.Text = "Saved";
            FillGrid("Select * from Students");
        }
        else
            Label1.Text = "OOPS!!!";
    }

    public DataSet SearchID()
    {
        DataSet dsUniqueStudent = stObj.FillDataSet("Select * from Students where Id='" + txtSearch.Text + "'");
        return dsUniqueStudent;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet dsUniqueStudent = SearchID();

        if (dsUniqueStudent.Tables[0].Rows.Count != 0)
        {
            txtName.Text = dsUniqueStudent.Tables[0].Rows[0].ItemArray[1].ToString();
            txtPh.Text = dsUniqueStudent.Tables[0].Rows[0].ItemArray[2].ToString();
            txtEMail.Text = dsUniqueStudent.Tables[0].Rows[0].ItemArray[3].ToString();
        }
        else
        {
            Label1.Text = "Type the ID carefully. It is not there...";
        }
    }
}