using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for students
/// </summary>
public class students
{
    SqlConnection Connection;
    public students()
    {
         Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|CollegeDB.mdf;Integrated Security=True");
        //
        // TODO: Add constructor logic here
        //
    }
    private string GenerateID(string Name, string Phone)
    {
        string Name4Char = Name.Substring(0, 4);
        string ID = Name4Char + Phone;

        return ID;
    }
    public DataSet FillDataSet(string Select)
    {
        SqlDataAdapter adpStudents = new SqlDataAdapter(Select, GetCon());
        DataSet dsStudents = new DataSet();

        adpStudents.Fill(dsStudents);
        CloseCon();
        return dsStudents;
    }
    public int SaveData(string ID,string name,string phone , string email,int InsertOrUpdate=0)
    {
        string Insert = "";
        if (InsertOrUpdate == 0)
        {
            Insert = "Insert into Students Values(@ID,@Name,@Ph,@Email,0)";
        }
        else if (InsertOrUpdate == 1)
        {
            Insert = "Update Students Set Name=@Name,PhoneNo=@Ph,Email=@Email Where ID=@ID";
        }

      
        SqlCommand CommandObject = new SqlCommand(Insert, GetCon());


        if (InsertOrUpdate == 0)
        {
            CommandObject.Parameters.AddWithValue("@ID", GenerateID(name, phone));
            CommandObject.Parameters.AddWithValue("@Name", name);
            CommandObject.Parameters.AddWithValue("@Ph", phone);
            CommandObject.Parameters.AddWithValue("@Email", email);


        }
        else if (InsertOrUpdate == 1)
        {
            CommandObject.Parameters.AddWithValue("@ID", ID);
            CommandObject.Parameters.AddWithValue("@Name", name);
            CommandObject.Parameters.AddWithValue("@Ph", phone);
            CommandObject.Parameters.AddWithValue("@Email", email);


        }


        int Status = CommandObject.ExecuteNonQuery();
        CloseCon();

        return Status;

    }
    #region ConnecManagement

    private SqlConnection GetCon()
    {

        Connection.Open();
        return Connection;
    }

    private void CloseCon()
    {
        Connection.Close();


    }
    #endregion
}