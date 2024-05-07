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
            // 객체 생성시 프로퍼티(변수) 값을 설정하는 방법, 1번.
            Bike myBike = new Bike() { Speed = 60, Color = Colors.Aqua };

            // 객체 생성시 속성초기화, 2번.
            Bike yourBike = new Bike();
            yourBike.Speed = 50;
            yourBike.Color = Colors.Black; // Color.FromArgb(255, 255, 0, 0);

            StsBike.DataContext = yourBike; // WPF 방식

            TxtMyBikeSpeed.Text = myBike.Speed.ToString(); // 전동적인 윈폼방식
        }

        private void TxtMyBikeSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtCopySpeed.Text = TxtMyBikeSpeed.Text; 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("버튼 클릭!!!");
        }
    }
}