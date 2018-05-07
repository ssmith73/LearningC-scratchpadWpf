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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScratchpadWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.txtBoxLength.Text = "";
            this.txtBoxLength.Text = ((CheckBox)sender).Content.ToString();
        }

        private void cmboBoxFinish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.txtBoxNote != null)
                this.txtBoxNote.Text = ((ComboBox)sender).SelectedValue). + " is selected";
        }
    }
}
