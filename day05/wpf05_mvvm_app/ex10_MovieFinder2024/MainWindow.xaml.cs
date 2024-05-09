using ex10_MovieFinder2024.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ex10_MovieFinder2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool isFavorite = false; // 즐겨찾기인지/API로 검색한건지 false => openAPI, true=> 즐겨찾기 보기
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //await this.ShowMessageAsync("검색", "검색을 시작합니다");
            if(string.IsNullOrEmpty(TxtMovieName.Text))
            {
                await this.ShowMessageAsync("검색", "검색할 영화명을 입력하세요!!");
                return;
            }
            SearchMovie(TxtMovieName.Text);
        }

        private async void SearchMovie(string moviname)
        {
            string tmdb_apiKey = "8115706b08ec0b1799ff8fff3ae7f7bf"; //TMDB사이트에서 제공받은 API키
            string encoding_moviename = HttpUtility.UrlEncode(moviname, Encoding.UTF8);
            string openApiUri = $"https://api.themoviedb.org/3/search/movie?api_key={tmdb_apiKey}" +
                                $"&language=ko-KR&page=1&include_adult=false&query={encoding_moviename}";

            //Debug.WriteLine(openApiUri);
            string result = string.Empty;

            // Openapi 실행 객체
            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;
            try
            {
                // tmdb api 요청
                req = WebRequest.Create(openApiUri); // URL을 넣어서 객체를 생성
                res = await req.GetResponseAsync(); // 요청한 URL의 결과를 비동기 응답
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                // TODO : 메시지 박스로 출력
            }
            finally
            {
                reader.Close();
                res.Close();
            }

            // result string을 json으로 변경
            var jsonResult = JObject.Parse(result); // type.Parse(string)
            var total = Int32.Parse(jsonResult["total_results"].ToString());
            //await this.ShowMessageAsync("검색수",total.ToString());
            var results = jsonResult["results"];
            var jsonArray = results as JArray;

            var movieItems = new List<MovieItem>();
            foreach(var item in jsonArray)
            {
                var movieItem = new MovieItem()
                {
                    // 프로퍼티라서 대문자로 시작, json 자체 키가 adult
                    // convert.toboolean(object) == boolean.parse(string)
                    Adult = Boolean.Parse(item["adult"].ToString()),
                    Id = Int32.Parse(item["id"].ToString()),
                    Original_Language = item["original_language"].ToString(),
                    Original_Title = item["original_title"].ToString(),
                    Overview = item["overview"].ToString(),
                    Popularity = Double.Parse(item["popularity"].ToString()),
                    Poster_Path = item["poster_path"].ToString(),
                    Release_Date = item["release_date"].ToString(),
                    Title = item["title"].ToString(),
                    Vote_average = Double.Parse(item["vote_average"].ToString()),
                    Vote_Count = Int32.Parse(item["vote_count"].ToString())

                };
                movieItems.Add(movieItem);
            }
            this.DataContext = movieItems;


        }

        private async void TxtMovieName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        private async void BtnAddFavorite_Click(object sender, RoutedEventArgs e)
        {
            if(GrbResult.SelectedItems.Count == 0)
            {
                await this.ShowMessageAsync("즐겨찾기", "추가할 영화를 선택하세요(복수선택 가능).");
                return;
            }
            var addmovieItems = new List<MovieItem>();
            foreach(MovieItem item in GrbResult.SelectedItems)
            {
                addmovieItems.Add(item);
            }
            Debug.WriteLine(addmovieItems.Count);
            try
            {
                    var insRes = 0;
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();
                    foreach(MovieItem item in addmovieItems)
                    {
                        SqlCommand cmd = new SqlCommand(Models.MovieItem.INSERT_QUERY, conn);
                        cmd.Parameters.AddWithValue("@Id",item.Id);
                        cmd.Parameters.AddWithValue("@Title",item.Title);
                        cmd.Parameters.AddWithValue("@Original_Title", item.Original_Title);
                        cmd.Parameters.AddWithValue("@Release_Date",item.Release_Date);
                        cmd.Parameters.AddWithValue("@Original_Language", item.Original_Language);
                        cmd.Parameters.AddWithValue("@Adult", item.Adult);
                        cmd.Parameters.AddWithValue("@Popularity", item.Popularity);
                        cmd.Parameters.AddWithValue("@Vote_Average", item.Vote_average);
                        cmd.Parameters.AddWithValue("@Vote_Count", item.Vote_Count);
                        cmd.Parameters.AddWithValue("@Poster_Path", item.Poster_Path);
                        cmd.Parameters.AddWithValue("@Overview", item.Overview);

                        insRes += cmd.ExecuteNonQuery();
                    }
                }
                if(insRes == addmovieItems.Count)
                {
                    await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 저장성공");
                }

            }
            catch (Exception ex) { await this.ShowMessageAsync("오류", $"{ex.Message}"); }
        }

        private async void BtnViewFavorite_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            TxtMovieName.Text = string.Empty;

            List<MovieItem> favMovieItems = new List<MovieItem>();

            try
            {
                using(SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();
                    var cmd = new SqlCommand(Models.MovieItem.SELECT_QUERY, conn);
                    var adapter = new SqlDataAdapter(cmd);
                    var dSet = new DataSet();
                    adapter.Fill(dSet, "MovieItem");
                    foreach(DataRow row in dSet.Tables["MovieItem"].Rows)
                    {
                        var movieItem = new MovieItem()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Title = Convert.ToString(row["Title"]),
                            Original_Title = Convert.ToString(row["Original_Title"]),
                            Release_Date = Convert.ToString(row["Release_Date"]),
                            Original_Language = Convert.ToString(row["Original_Language"]),
                            Adult = Convert.ToBoolean(row["Adult"]),
                            Vote_average = Convert.ToDouble(row["Vote_Average"]),
                            Vote_Count = Convert.ToInt32(row["Vote_Count"]),
                            Poster_Path = Convert.ToString(row["Poster_Path"]),
                            Overview = Convert.ToString(row["Overview"]),
                            Popularity = Convert.ToDouble(row["Popularity"]),
                            Reg_Date = Convert.ToDateTime(row["Reg_Date"])
                        };
                        favMovieItems.Add(movieItem);
                    }
                    this.DataContext = favMovieItems;
                    isFavorite = true;
                    StsResult.Content = $"즐겨찾기 {favMovieItems.Count}건 조회 완료";
                    ImgPoster.Source = new BitmapImage(new Uri("/NoPicture.png", UriKind.RelativeOrAbsolute));

                }
            }
            catch (Exception ex){ await this.ShowMessageAsync("오류", $"{ex.Message}"); }
            //await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 확인합니다");
        }

        private async void BtnDelFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (GrbResult.SelectedItems.Count == 0)
            {
                await this.ShowMessageAsync("삭제", "삭제할 영화를 선택하세요");
                return;
            }
            var addmovieItems = new List<MovieItem>();
            foreach (MovieItem item in GrbResult.SelectedItems)
            {
                addmovieItems.Add(item);
            }
            Debug.WriteLine(addmovieItems.Count);
            try
            {
                var insRes = 0;
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();
                    foreach (MovieItem item in addmovieItems)
                    {
                        SqlCommand cmd = new SqlCommand(Models.MovieItem.DELETE_QUERY, conn);
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Title", item.Title);
                        cmd.Parameters.AddWithValue("@Original_Title", item.Original_Title);
                        cmd.Parameters.AddWithValue("@Release_Date", item.Release_Date);
                        cmd.Parameters.AddWithValue("@Original_Language", item.Original_Language);
                        cmd.Parameters.AddWithValue("@Adult", item.Adult);
                        cmd.Parameters.AddWithValue("@Popularity", item.Popularity);
                        cmd.Parameters.AddWithValue("@Vote_Average", item.Vote_average);
                        cmd.Parameters.AddWithValue("@Vote_Count", item.Vote_Count);
                        cmd.Parameters.AddWithValue("@Poster_Path", item.Poster_Path);
                        cmd.Parameters.AddWithValue("@Overview", item.Overview);

                        insRes += cmd.ExecuteNonQuery();
                    }
                }
                if (insRes == addmovieItems.Count)
                {
                    await this.ShowMessageAsync("삭제", "즐겨찾기 삭제성공");
                }

            }
            catch (Exception ex) { await this.ShowMessageAsync("오류", $"{ex.Message}"); }
            this.DataContext = null;
            TxtMovieName.Text = string.Empty;

            List<MovieItem> favMovieItems = new List<MovieItem>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();
                    var cmd = new SqlCommand(Models.MovieItem.SELECT_QUERY, conn);
                    var adapter = new SqlDataAdapter(cmd);
                    var dSet = new DataSet();
                    adapter.Fill(dSet, "MovieItem");
                    foreach (DataRow row in dSet.Tables["MovieItem"].Rows)
                    {
                        var movieItem = new MovieItem()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Title = Convert.ToString(row["Title"]),
                            Original_Title = Convert.ToString(row["Original_Title"]),
                            Release_Date = Convert.ToString(row["Release_Date"]),
                            Original_Language = Convert.ToString(row["Original_Language"]),
                            Adult = Convert.ToBoolean(row["Adult"]),
                            Vote_average = Convert.ToDouble(row["Vote_Average"]),
                            Vote_Count = Convert.ToInt32(row["Vote_Count"]),
                            Poster_Path = Convert.ToString(row["Poster_Path"]),
                            Overview = Convert.ToString(row["Overview"]),
                            Popularity = Convert.ToDouble(row["Popularity"]),
                            Reg_Date = Convert.ToDateTime(row["Reg_Date"])
                        };
                        favMovieItems.Add(movieItem);
                    }
                    this.DataContext = favMovieItems;
                    isFavorite = true;
                    StsResult.Content = $"즐겨찾기 {favMovieItems.Count}건 조회 완료";
                    ImgPoster.Source = new BitmapImage(new Uri("/NoPicture.png", UriKind.RelativeOrAbsolute));

                }
            }
            catch (Exception ex) { await this.ShowMessageAsync("오류", $"{ex.Message}"); }
            //await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 확인합니다");
        }

        private async void BtnWatchTrailer_Click(object sender, RoutedEventArgs e)
        {
            if(GrbResult.SelectedItems.Count == 0)
            {
                await this.ShowMessageAsync("예고편보기", "영화를 선택하세요");
                return;
            }
            if(GrbResult.SelectedItems.Count>1){
                await this.ShowMessageAsync("예고편보기", "영화를 하나만 선택하세요");
                return;
            }
            var movieName = (GrbResult.SelectedItem as MovieItem).Title;
            var trailerWindow = new TrailerWindow(movieName);
            trailerWindow.Owner = this;
            trailerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            trailerWindow.ShowDialog();
        }

        private async void GrbResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            // 그리드 결과가 재검색시 다시 발생
            try
            {
                var movie = GrbResult.SelectedItem as MovieItem;
                var poster_path = movie.Poster_Path;
                //await this.ShowMessageAsync("포스터", poster_path);
                if (string.IsNullOrEmpty(poster_path))
                {
                    ImgPoster.Source = new BitmapImage(new Uri("/NoPicture.png", UriKind.RelativeOrAbsolute));

                }
                else
                {
                    var base_url = "https://image.tmdb.org/t/p/w300_and_h450_bestv2";
                    ImgPoster.Source = new BitmapImage(new Uri($"{base_url}{poster_path}",UriKind.Absolute));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TxtMovieName.Focus();
        }

        /*private void BtnNaverMovie_Click(object sender, RoutedEventArgs e)
        {

        }*/
    }
}