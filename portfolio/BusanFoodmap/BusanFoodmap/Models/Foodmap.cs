using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusanFoodmap.Models
{
    public class Foodmap
    {
        public string MAIN_TITLE { get; set; } // 제목
        public string RPRSNTV_MENU {  get; set; }
        public string GUGUN_NM { get; set; }
        public string addr { get ; set; }   //  주소
        public string USAGE_DAY_WEEK_AND_TIME { get; set; }
        public string CNTCT_TEL { get; set; } // 휴대폰
        public string MAIN_IMG_THUMB { get; set; } // 홈페이지
        public double LNG { get; set; } // 경도
        public double LAT { get; set; } // 위도


        





        public static readonly string SELECT_QUERY = @"SELECT[Title]
                                                      ,[Tel]
                                                      ,[Homepage]
                                                      ,[coordx]
                                                      ,[coordy]
                                                      ,[addr]
                                                      ,[RPP_menu]
                                                        FROM[dbo].[foodmap]";

        
    }
}
