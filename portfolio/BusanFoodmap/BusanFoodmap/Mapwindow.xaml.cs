using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BusanFoodmap
{
    /// <summary>
    /// Mapwindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Mapwindow : Window
    {
        public Mapwindow()
        {
            InitializeComponent();
        }
        public Mapwindow(double coordy, double coordx) : this()
        {
            BrsLoc.Address = $"https://google.com/maps/place/{coordy},{coordx}";
        }
    }
}
