using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
namespace Main_UID_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=p_wpf_main");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUname.Focus(); 
        }

        private void Uname_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtUname.Text) && txtUname.Text.Length > 0) {
                textUname.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUname.Visibility = Visibility.Visible;
            }
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            txtPass.Focus();
        }

        private void Pass_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPass.Text) && txtPass.Text.Length > 0)
            {
                textPass.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPass.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string q = "select emp_id,deo_pass from deo_auth where emp_id = '"+empid.Text+"' and deo_pass = '"+pass.Text+"'";
                string q = "select emp_id,deo_pass from deo_auth where emp_id = @id and deo_pass = @name";
                MySqlCommand cmd = new MySqlCommand(q, con);
                con.Open();
                String Uname = txtUname.Text.ToString();
                String Pass = txtPass.Text.ToString();
                cmd.Parameters.AddWithValue("@id",Uname);
                cmd.Parameters.AddWithValue("@name", Pass);
                MySqlDataReader dr = cmd.ExecuteReader();
                int i = Convert.ToInt32(dr.Read());
                if (i == 1)
                {
                    MessageBox.Show("Data  Found");
                    string emp_id = dr[0].ToString();
                    string pas = dr[1].ToString();
                    this.Visibility = Visibility.Hidden;
                    Home home = new Home();
                    home.Show();
                }
                else
                {
                    MessageBox.Show("Data Not Found");
                }
                con.Close();
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
