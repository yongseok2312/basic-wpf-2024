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

namespace WpfApp1
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

        private void CboFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chagefont();
        }

        void chagefont()
        {
            if (CboFonts.SelectedIndex < 0) { return; }
            //MessageBox.Show(CboFonts.SelectedIndex.ToString());
            TxtSampleText.FontFamily = CboFonts.SelectedItem as FontFamily;
            if (ChkItalic.IsChecked == true) TxtSampleText.FontStyle = FontStyles.Italic;
            else TxtSampleText.FontStyle = FontStyles.Normal;

            if (ChkBold.IsChecked == true) TxtSampleText.FontWeight = FontWeights.Bold;
            else TxtSampleText.FontWeight = FontWeights.Normal;
        }

    
        private void ChkBold_Checked(object sender, RoutedEventArgs e)
        {
            chagefont();
        }
        

        private void ChkItalic_Checked(object sender, RoutedEventArgs e)
        {
            chagefont();
        }

        private void FrmMain_Loaded(object sender, RoutedEventArgs e)
        {
            var fonts = Fonts.SystemFontFamilies;
            foreach (var font in fonts)
            {
                CboFonts.Items.Add(font);
            }
        }

        private void ChkBold_Unchecked(object sender, RoutedEventArgs e)
        {
            chagefont();
        }

        private void ChkItalic_Unchecked(object sender, RoutedEventArgs e)
        {
            chagefont();
        }
    }
}