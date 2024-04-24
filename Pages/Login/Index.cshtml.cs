using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace DepSite.Pages.Login
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost(string username, string password)
        {
            try
            {
                string connectionString = "server=localhost;port=3306;database=CSEAI;user=root;password=vaibhav@2210;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM login WHERE login_id = @username AND password = @password";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();


                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {
                        // Username and password match, login successful
                        return RedirectToPage("/StudentLanding/Index", new { username = username });
                    }
                    else
                    {
                        // Username and password don't match
                        ViewData["ErrorMessage"] = "Invalid username or password";
                        ViewData["Username"] = username;
                        return Page();
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request");
                Console.WriteLine("Error: " + ex.Message);
                return RedirectToPage("/Index");
            }
        }
    }
}
