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
            InitComboDateFromDB();
        }

        private void InitComboDateFromDB()
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
        }


        private async Task BtnReqRealtime_ClickAsync(object sender, RoutedEventArgs e)
        {
            string openApiUri = "http://apis.data.go.kr/6260000/FoodService/getFoodEn";
            string result = string.Empty;

            // WebRequest, WebResponse 객체
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
                await this.ShowMessageAsync("오류", $"OpenAPI 조회오류 {ex.Message}");
            }

            var jsonResult = JObject.Parse(result);
            var status = Convert.ToInt32(jsonResult["status"]);

            if (status == 200)
            {
                var data = jsonResult["data"];
                var jsonArray = data as JArray; // json자체에서 []안에 들어간 배열데이터만 JArray 변환가능

                var dustSensors = new List<Foodmap>();
                foreach (var item in jsonArray)
                {
                    dustSensors.Add(new Foodmap()
                    {
                        MAINT_TILTE = Convert.ToString(item["MAIN_TITLE"]),
                        homepage = Convert.ToString(item["MAIN_IMG_THUMB"]),
                        TEL = Convert.ToString(item["CNTCT_TEL"]),
                        Coordx = Convert.ToDouble(item["LNG"]),
                        Coordy = Convert.ToDouble(item["LAT"]),
                        addr = Convert.ToString(item)
                    });
                }

                this.DataContext = dustSensors;
                StsResult.Content = $"OpenAPI {dustSensors.Count}건 조회완료!";

            }
        }
    }
}