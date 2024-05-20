using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject3.Map
{
    public class Dbquery
    {
        public string 주소 {  get; set; } 
        public string 위도 { get; set; }

        public string 경도 { get; set; }  

        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[map1]
                                                                       ([주소]
                                                                       ,[위도]
                                                                       ,[경도])
                                                                 VALUES
                                                                       (@주소
                                                                       ,@위도
                                                                       ,@경도)";

        public static readonly string SELECT_QUERY = @"
                                                        SELECT [주소]
                                                              ,[위도]
                                                              ,[경도]
                                                          FROM [dbo].[map1]";

        public static readonly string DELETE_QUERY = @"DELETE FROM [dbo].[map1]
                                                         WHERE [주소] = [주소]";
    }
}
