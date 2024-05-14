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
   
        public MainWindow()
        {
            InitializeComponent();
            StartMap();
        }
        private void StartMap()
        {
            GMapProvider.WebProxy = System.Net.WebRequest.DefaultWebProxy;
            GMapProvider.WebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            GMapProvider.Language = GMap.NET.LanguageType.Korean;
            GoogleMapProvider.Instance.ApiKey = "1";

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
            string address = SearchAddr.Text; // 사용자가 입력한 주소 가져오기
            PointLatLng? pos = GetLocationFromAddress(address); // 주소를 좌표로 변환
            if (pos != null)
            {
                mapControl.Position = pos.Value; // 지도의 중심 위치 설정
            }
            else
            {
                // 주소를 찾을 수 없는 경우 메시지 표시
                MessageBox.Show("주소를 찾을 수 없습니다.");
            }
        }
        private PointLatLng? GetLocationFromAddress(string address)
        {
            GeoCoderStatusCode status;
            PointLatLng? pos = GMap.NET.MapProviders.GoogleMapProvider.Instance.GetPoint(address, out status);
            if (status == GeoCoderStatusCode.OK && pos != null)
            {
                return pos;
            }
            else
            {
                return null;
            }
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