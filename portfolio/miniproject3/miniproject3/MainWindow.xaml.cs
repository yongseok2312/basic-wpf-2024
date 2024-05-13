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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using System.Diagnostics;
using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Newtonsoft.Json;

namespace miniproject3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        PointLatLng start;
        PointLatLng end;
        GMapMarker currentMarker;
        List<GMapMarker> Circles = new List<GMapMarker>();
        public MainWindow()
        {
            InitializeComponent();
            GMapProvider.WebProxy = System.Net.WebRequest.DefaultWebProxy;
            GMapProvider.WebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            GMapProvider.Language = GMap.NET.LanguageType.Korean;
            GoogleMapProvider.Instance.ApiKey = "AIzaSyBk30B3_u4uTU-kQUhx5JgobmR8ZG6J7tg";

            // 지도 설정
            mapControl.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly; // 지도 모드 설정
            mapControl.Position = new PointLatLng(35.168177885875565, 129.05738353729248); 
            mapControl.MinZoom = 2;
            mapControl.MaxZoom = 20;
            mapControl.Zoom = 14;
            mapControl.ShowCenter = false;
            mapControl.DragButton = MouseButton.Left;
        }

        private void SearchAddr_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddFavorite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnViewFavorite_Click(object sender, RoutedEventArgs e)
        {

        }


        private void BtnDelFavorite_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Addrcurrent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}