using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusanFoodmap.Models
{
    public class Foodmap
    {
        public string  MAINT_TILTE { get; set; } // 제목
        public double Coordx { get; set; } // 경도
        public double Coordy { get; set; } // 위도

        public string TEL { get; set; } // 휴대폰

        public string homepage { get; set; } // 홈페이지
        
        public string addr { get ; set; }   //  주소

        public string RPP_Menu { get; set; } // 메인

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
