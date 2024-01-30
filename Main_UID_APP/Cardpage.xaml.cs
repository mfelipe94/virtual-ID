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
    /// Interaction logic for Cardpage.xaml
    /// </summary>
    public partial class Cardpage : Window
    {
        public Cardpage()
        {
            InitializeComponent();
        }
        public String i, n, g, d, a;

        private void Window_Activated_1(object sender, EventArgs e)
        {
            this.nametxt.Text = n.ToString();
            this.gentxt.Text = g.ToString();
            this.dobtxt.Text = d.ToString();
            this.addtxt.Text = a.ToString();
            this.uid2.Text = i.ToString();
            this.uid1.Text = i.ToString();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.nametxt.Text = n.ToString();
            this.gentxt.Text = g.ToString();
            this.dobtxt.Text = d.ToString();
            this.addtxt.Text = a.ToString();
            this.uid2.Text = i.ToString();
            this.uid1.Text = i.ToString();
        }
        public Cardpage(String i,String n, String g, String d, String a)
        {
            InitializeComponent();
            this.i = i;
            this.n  = n;
            this.g  = g;
            this.d = d;
            this.a = a;
        } 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Home h = new Home();
            h.Show();
        }

       
    }
}
