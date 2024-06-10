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


        





        public static readonly string SELECT_QUERY = @"SELECT [MAINT_TILTE]
                                                      ,[RPRSNTV_MENU]
                                                      ,[GUGUN_NM]
                                                      ,[addr]
                                                      ,[USAGE_DAY_WEEK_AND_TIME]
                                                      ,[CNTCT_TEL]
                                                      ,[MAIN_IMG_THUMB]
                                                      ,[LNG]
                                                      ,[LAT]
                                                  FROM [dbo].[Table_1]";

        public static readonly string Insert_QUERY = @"INSERT INTO [dbo].[Table_1]
                                                       ([MAINT_TILTE]
                                                       ,[RPRSNTV_MENU]
                                                       ,[GUGUN_NM]
                                                       ,[addr]
                                                       ,[USAGE_DAY_WEEK_AND_TIME]
                                                       ,[CNTCT_TEL]
                                                       ,[MAIN_IMG_THUMB]
                                                       ,[LNG]
                                                       ,[LAT])
                                                 VALUES
                                                       (@MAINT_TILTE
                                                       ,@RPRSNTV_MENU
                                                       ,@GUGUN_NM
                                                       ,@addr
                                                       ,@USAGE_DAY_WEEK_AND_TIME
                                                       ,@CNTCT_TEL
                                                       ,@MAIN_IMG_THUMB
                                                       ,@LNG
                                                       ,@LAT)";

        public static readonly string Delete_QUERY = @"DELETE FROM [dbo].[Table_1]
                                                         WHERE MAINT_TILTE=@MAINT_TILTE";



    }
}
