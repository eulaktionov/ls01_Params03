using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

using static System.Console;

WriteLine("App Settings and Query Parameters");

Write("Enter name: ");
string firstName = Console.ReadLine();

SqlConnection connection = null;
SqlDataReader reader = null;

try
{
    var connectionString =
        ConfigurationManager.AppSettings.Get("connectionString");
    connection = new SqlConnection(connectionString);
    connection.Open();

    string commandText =
        "select * " +
        "from Authors " +
        "where FirstName = @FirstName";
    SqlCommand command =
        new SqlCommand(commandText, connection);
    
    //SqlParameter paramFirstName = new SqlParameter("@FirstName", System.Data.SqlDbType.VarChar).Value = firstName;

    command.Parameters.AddWithValue("@FirstName", firstName);
    reader = command.ExecuteReader();
    ShowData();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    if(reader != null)
    {
        reader.Close();
    }
    if(connection is not null)
    {
        connection.Close();
    }
    ReadKey();
}

void ShowData()
{
    for(int i = 0; i < reader.FieldCount; i++)
    {
        Write($"{reader.GetName(i),20}");
    }
    WriteLine();
    while(reader.Read())
    {
        for(int i = 0; i < reader.FieldCount; i++)
        {
            Write($"{reader[i],20}");
        }
        WriteLine();
    }
}

