using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ex05_wpf_bikeshop
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bike myBike = new Bike
            {
                Speed = 60,
                Color = Colors.Aqua //Color.FromArgb(255, 255, 0, 0);

            };
            StsBike.DataContext = myBike;
            TxtMyBikeSpeed.Text = myBike.Speed.ToString();
        }

        private void TxtCopySpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TxtCopySpeed.Text = TxtMyBikeSpeed.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("버튼 클릭");
        }
    }
}