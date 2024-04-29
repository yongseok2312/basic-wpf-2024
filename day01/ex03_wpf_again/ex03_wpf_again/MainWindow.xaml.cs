﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ex03_wpf_again
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

        private void sldtemp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pgbtemp.Value = e.NewValue;
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(psbtemp.Password);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string mediaFileName = "sampleMP4.mp4";
            string mediaDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //MessageBox.Show(mediaDirectory);s
            string mediaFilePath = System.IO.Path.Combine(mediaDirectory, "Videos",mediaFileName);
            MdeMovie.Source = new Uri(mediaFilePath);

        }
    }
}