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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using CefSharp.DevTools.Network;
using System.Net;
using System.IO;
using System.Diagnostics;
using BusanFoodmap.Models;
using System.Net.Http;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using Microsoft.VisualBasic.Logging;

namespace BusanFoodmap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //InitComboDateFromDB();
        }

        /*private void InitComboDateFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Helplers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.Foodmap.SELECT_QUERY, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet);
                List<string> saveDates = new List<string>();

                foreach (DataRow row in dSet.Tables[0].Rows)
                {
                    saveDates.Add(Convert.ToString(row["Save_Date"]));
                }

                CboReqDate.ItemsSource = saveDates;
            }
        }*/

        private async void BtnReqRealtime_Click(object sender, RoutedEventArgs e)
        {
            string openApiUri = "https://apis.data.go.kr/6260000/FoodService/getFoodKr?serviceKey=WDeiD%2FbkKC0qewX%2BCWWJNFo1aT7K%2B%2F%2FUP5tshjyEEN%2BF9mYBVT78QrkE3wS6KrscaUK7F4o4%2B2pNxIGcMfnsnQ%3D%3D&numOfRows=1000&pageNo=1&resultType=json";
            string result = string.Empty;

            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;
            try
            {
                req = WebRequest.Create(openApiUri);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

                //await this.ShowMessageAsync("결과", result);
                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"OpenApi 조회 오류 {ex.Message}");
                return;
            }


            try
            {
                var jsonResult = JObject.Parse(result);
                var data = jsonResult["getFoodKr"]["item"];
                var jsonArray = data as JArray;

                var foodsearch = new List<Foodmap>();
                foreach (var item in jsonArray)
                {
                    foodsearch.Add(new Foodmap()
                    {
                        MAIN_TITLE = Convert.ToString(item["MAIN_TITLE"]),
                        GUGUN_NM = Convert.ToString(item["GUGUN_NM"]),
                        addr = Convert.ToString(item["ADDR1"]),
                        RPRSNTV_MENU = Convert.ToString(item["RPRSNTV_MENU"]),
                        MAIN_IMG_THUMB = Convert.ToString(item["MAIN_IMG_THUMB"]),
                        CNTCT_TEL = Convert.ToString(item["CNTCT_TEL"]),
                        USAGE_DAY_WEEK_AND_TIME = Convert.ToString(item["USAGE_DAY_WEEK_AND_TIME"]),
                        LNG = Convert.ToDouble(item["LNG"]),
                        LAT = Convert.ToDouble(item["LAT"])
                    });
                }

                // Dispatcher.Invoke를 사용하여 UI 스레드에서 UI 업데이트
                Dispatcher.Invoke(() =>
                {
                    this.DataContext = foodsearch;
                    StsResult.Content = $"OpenAPI {foodsearch.Count}건 조회 완료!";
                });
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"JSON 처리 오류 {ex.Message}");
            }
        }

        private void GrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var curItem = GrdResult.SelectedItem as Foodmap;

            var mapWindow = new Mapwindow(curItem.LAT, curItem.LNG);
            mapWindow.Owner = this;
            mapWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mapWindow.ShowDialog();

        }

        private async void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            if (GrdResult.Items.Count == 0)
            {
                await this.ShowMessageAsync("저장오류", "실시간 조회후 저장하십시오.");
                return;
            }


            try
            {
                using (SqlConnection conn = new SqlConnection(Helplers.Common.CONNSTRING))
                {
                    conn.Open();

                    var insRes = 0;
                    foreach (Foodmap item in GrdResult.Items)
                    {
                        SqlCommand cmd = new SqlCommand(Models.Foodmap.Insert_QUERY, conn);
                        cmd.Parameters.AddWithValue("@MAINT_TILTE", item.MAIN_TITLE);
                        cmd.Parameters.AddWithValue("@RPRSNTV_MENU", item.RPRSNTV_MENU);
                        cmd.Parameters.AddWithValue("@GUGUN_NM", item.GUGUN_NM);
                        cmd.Parameters.AddWithValue("@addr", item.addr);
                        cmd.Parameters.AddWithValue("@USAGE_DAY_WEEK_AND_TIME", item.USAGE_DAY_WEEK_AND_TIME);
                        cmd.Parameters.AddWithValue("@CNTCT_TEL", item.CNTCT_TEL);
                        cmd.Parameters.AddWithValue("@MAIN_IMG_THUMB", item.MAIN_IMG_THUMB);
                        cmd.Parameters.AddWithValue("@LNG", item.LNG);
                        cmd.Parameters.AddWithValue("@LAT", item.LAT);
                        insRes += cmd.ExecuteNonQuery();
                    }
                    if (insRes > 0)
                    {
                        await this.ShowMessageAsync("저장", "DB저장성공!");
                        StsResult.Content = $"DB저장 {insRes}건 성공!";
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("저장오류", $"저장오류 {ex.Message}");
            }
        }

    }
}   

