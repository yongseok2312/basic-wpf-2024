﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
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
           


        }

        private async void TxtMovieName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private async void BtnAddFavorite_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 추가합니다");
        }

        private async void BtnViewFavorite_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 확인합니다");
        }

        private async void BtnDelFavorite_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 삭제합니다");
        }

        private async void BtnWatchTrailer_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("유튜브 예고", "예고");
        }

        private async void GrbResult_CurrentCellChanged(object sender, System.EventArgs e)
        {
            await this.ShowMessageAsync("포스터", "포스터를 처리합니다");
        }

        /*private void BtnNaverMovie_Click(object sender, RoutedEventArgs e)
        {

        }*/
    }
}