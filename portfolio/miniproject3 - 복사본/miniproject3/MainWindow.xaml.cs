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
using System.Net.Http;
using System.Collections.ObjectModel;

namespace miniproject3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ObservableCollection<string> searchResults = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            StartMap();
            InitializeListView();
        }
        private void InitializeListView()
        {
            // ListView와 searchResults 컬렉션을 바인딩
            SearchResultListView.ItemsSource = searchResults;
        }
        private void StartMap()
        {
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

        private async void SearchAddr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        private async Task<PointLatLng?> GetLocationFromAddress(string address)
        {
            try
            {
                string apiKey = "AIzaSyBk30B3_u4uTU-kQUhx5JgobmR8ZG6J7tg"; // Google Geocoding API 키
                string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key=AIzaSyBk30B3_u4uTU-kQUhx5JgobmR8ZG6J7tg"; // Geocoding API 요청 URL

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        dynamic data = JObject.Parse(jsonResponse);

                        if (data.status == "OK")
                        {
                            double lat = data.results[0].geometry.location.lat; // 위도
                            double lng = data.results[0].geometry.location.lng; // 경도
                            return new PointLatLng(lat, lng);
                        }
                        else
                        {
                            // 주소를 찾을 수 없는 경우
                            return null;
                        }
                    }
                    else
                    {
                        // HTTP 응답 실패 처리
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // 예외 발생 시 처리
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string address = SearchAddr.Text; // 검색할 주소 가져오기
            PointLatLng? position = await GetLocationFromAddress(address); // 주소를 좌표로 변환
            if (position != null)
            {
                searchResults.Add($"{address}");
                mapControl.Position = position.Value; // 지도의 중심 위치 설정
            }
            else
            {
                MessageBox.Show("주소를 찾을 수 없습니다.");
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

        

        private async void TabItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TabItem tabItem = sender as TabItem;

                string address = tabItem.Tag.ToString();
                PointLatLng? position = await GetLocationFromAddress(address);
                mapControl.Position = position.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}