using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace Main_UID_APP
{
    /// <summary>
    /// Interaction logic for CamaraImp.xaml
    /// </summary>
    public partial class CamaraImp : Window
    {
        public CamaraImp()
        {
            InitializeComponent();
        }
        public String i, n, g, d, a;
        public CamaraImp(String i, String n, String g, String d, String a)
        {
            InitializeComponent();
            this.i = i;
            this.n = n;
            this.g = g;
            this.d = d;
            this.a = a;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Image files|*.bmp;*.png;*.jpg";
            od.FilterIndex = 1;
            if(od.ShowDialog() == true) { 
                cam.Source = new BitmapImage(new Uri(od.FileName));
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cardpage cam1 = new Cardpage(i.ToString(), n.ToString(), g.ToString(), d.ToString(), a.ToString());
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(Cardpage))
                {
                    (window as Cardpage).p2.Source = photo.Source;
                    (window as Cardpage).p1.Source = cam.Source;
                }
            }
            this.Visibility = Visibility.Hidden;
            cam1.Show();
        }
    }
}
