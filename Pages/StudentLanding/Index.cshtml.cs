using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace DepSite.Pages.StudentLanding
{
    public class IndexModel : PageModel
    {
        public void OnGet(string username)
        {

            try
            {
                string connectionString = "server=localhost;port=3306;database=CSEAI;user=root;password=vaibhav@2210;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT name FROM student WHERE username = @username";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();

                    string userName = command.ExecuteScalar() as string;
                    ViewData["UserName"] = userName;

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request");
                Console.WriteLine("Error: " + ex.Message);
            }

            
        }
    }
}
