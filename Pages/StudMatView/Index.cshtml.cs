using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace DepSite.Pages.StudMatView
{
    public class IndexModel : PageModel
    {
        public void OnGet(string courseName)
        {
            
            try
            {
                string connectionString = "server=localhost;port=3306;database=CSEAI;user=root;password=vaibhav@2210;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT file_name, file_link FROM study_material WHERE semester_id = 4 AND course_id = @courseName";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@courseName", courseName);

                    connection.Open();

                    List<string[]> resultList = new List<string[]>();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] row = new string[2]; // Assuming two columns: file_name and file_link
                            row[0] = reader.GetString(0); // Assuming file_name is in the first column (index 0)
                            row[1] = reader.GetString(1); // Assuming file_link is in the second column (index 1)
                            resultList.Add(row);
                        }
                    }

                    // Convert List<string[]> to string[][]
                    string[][] resultArray = resultList.ToArray();
                    ViewData["resultArray"] = resultArray;
                }
            }
            catch (Exception ex)
            {
                //string[][] err = new string[1][];
                
            }
        }
    }
}
