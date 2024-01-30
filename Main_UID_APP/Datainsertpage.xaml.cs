using MySql.Data.MySqlClient;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Main_UID_APP
{
    /// <summary>
    /// Interaction logic for Datainsertpage.xaml
    /// </summary>
    public partial class Datainsertpage : Window
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=p_wpf_main");
        public Datainsertpage()
        {
            InitializeComponent();
        }
        public string date_formate(string date)
        {
            date = date.Trim();
            string[] d = date.Split('/');
            string ndate = d[2] + "-" + d[1] + "-" + d[0];
            return ndate;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Random rnd = new Random();
            string id = Convert.ToString(rnd.Next(100000000, 999999999));
            string Name = txtName.Text;
            string Mno = txtMob.Text;
            string Address = txtAddress.Text;
            string Caste = cast.Text;
            string date = dob.Text;
            string ndate = date_formate(date);
            string gender = string.Empty;
            if (male.IsChecked == true)
            {
                gender = "M";
            }
            else if (female.IsChecked == true)
            {
                gender = "F";
            }
            else
            {
                gender = "O";
            }
            try
            {
                string q = ("INSERT INTO uid_data (uid,u_name,u_mob_no,u_address,u_gender,u_cast,u_dob) " +
                    "VALUES (@uid,@u_name,@u_mob_no,@u_address,@u_gender,@u_cast,@u_dob)");
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@uid", id.ToString());
                cmd.Parameters.AddWithValue("@u_name", Name.ToString());
                cmd.Parameters.AddWithValue("@u_mob_no", Mno.ToString());
                cmd.Parameters.AddWithValue("@u_address", Address.ToString());
                cmd.Parameters.AddWithValue("@u_gender", gender.ToString());
                cmd.Parameters.AddWithValue("@u_cast", Caste.ToString());
                cmd.Parameters.AddWithValue("@u_dob", ndate.ToString());
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Data saved Successfully. ");
                    QRCodeGenerator QG = new QRCodeGenerator();
                    QRCodeData QD = QG.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);
                    QRCode QR = new QRCode(QD);
                    Bitmap QRIMAGE = QR.GetGraphic(20);
                    image.Source = BitmapToImage(QRIMAGE);
                   
                    this.Visibility = Visibility.Hidden;
                    CamaraImp cam = new CamaraImp(id.ToString(), Name.ToString(), gender.ToString(), ndate.ToString(), Address.ToString());
                    cam.Show();
                   /* Cardpage cam1 = new Cardpage(id.ToString(), Name.ToString(), gender.ToString(), ndate.ToString(), Address.ToString());
                    cam1.Show();*/
                }
                else
                {
                    MessageBox.Show("Data Not saved Successfully !!!");
                }
                con.Close();
            }
            
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(CamaraImp))
                {
                    (window as CamaraImp).photo.Source = image.Source;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Home h = new Home();
            h.Show();
        } 

        private void dob_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string date = dob.Text;
            txtDate.Text = date;
        }

        private ImageSource BitmapToImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}
