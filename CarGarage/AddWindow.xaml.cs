using System.Data.SqlClient;
using System.Windows;
namespace CarGarage;
public partial class AddWindow : Window
{
    private string model = "";
    private string marka = "";
    public string ConnectionString;
    public string Marka { get => marka; set => marka = value; }
    public string Model { get => model; set => model = value; }
    public AddWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        using (SqlConnection connection = new(ConnectionString))
        {
            try
            {
                if (Marka == "" || Model == "")
                    throw new("Marka and model should be entered");
                connection.Open();
                string query = @"INSERT INTO Car_Table (Marka, Model)
                     VALUES (@param1, @param2)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@param2", System.Data.SqlDbType.NVarChar).Value = Model;
                cmd.Parameters.Add("@param1", System.Data.SqlDbType.NVarChar).Value = Marka;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            Close();
        }
    }

    private void Cancel_Button_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
