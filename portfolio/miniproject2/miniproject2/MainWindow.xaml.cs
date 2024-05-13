using System.IO;
using System.Net.Http;
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
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using miniproject2.Models;

namespace miniproject2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
            mapControl.Position = new PointLatLng(35.168177885875565, 129.05738353729248); // 초기 위치 서울로 설정
            mapControl.MinZoom = 2;
            mapControl.MaxZoom = 20;
            mapControl.Zoom = 14;
            mapControl.ShowCenter = false;
            mapControl.DragButton = MouseButton.Left;

        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 사용자가 입력한 검색어 가져오기
                string searchQuery = txtSearch.Text;

                // 주소 검색 요청 생성
                var request = new GeocodingRequest();
                request.Address = searchQuery;

                // 구글 지오코딩 서비스에 접속하여 주소를 좌표로 변환
                var response = await new GeocodingService().GetResponseAsync(request);
                var result = response.Results.FirstOrDefault();

                if (result != null)
                {
                    // 검색 결과에서 좌표 추출
                    var location = result.Geometry.Location;
                    PointLatLng point = new PointLatLng(location.Latitude, location.Longitude);

                    // 검색된 위치를 지도 중심으로 이동
                    mapControl.Position = point;

                    // 검색된 위치에 마커 표시
                    GMapMarker marker = new GMapMarker(point);
                    marker.Shape = new Ellipse
                    {
                        Width = 10,
                        Height = 10,
                        Stroke = Brushes.Red,
                        StrokeThickness = 1.5
                    };
                    mapControl.Markers.Add(marker);
                }
                else
                {
                    MessageBox.Show("검색 결과를 찾을 수 없습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public class GeocodingService
        {
            private const string BaseUrl = "https://maps.googleapis.com/maps/api/geocode/json";

            public async Task<GeocodingResponse> GetResponseAsync(GeocodingRequest request)
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{BaseUrl}?address={Uri.EscapeDataString(request.Address)}&key=AIzaSyBk30B3_u4uTU-kQUhx5JgobmR8ZG6J7tg";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<GeocodingResponse>(json);
                    }
                    else
                    {
                        throw new Exception($"Geocoding API request failed with status code {(int)response.StatusCode}");
                    }
                }
            }
        }
        public class GeocodingRequest
        {
            public string Address { get; set; }
        }

        public class GeocodingResponse
        {
            public GeocodingResult[] Results { get; set; }
        }

        public class GeocodingResult
        {
            public Geometry Geometry { get; set; }
        }

        public class Geometry
        {
            public Location Location { get; set; }
        }

        public class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
