using System;
using System.Collections.Generic;

using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex05_wpf_bikeshop
{
    // Notifier를 상속받으면 AutoProperty {get;set;}을 사용하지 못함
    public class Bike : Notifier
    {
        private double speed; // 멤버 변수
        private Color color;

        public double Speed { 
            get { return speed; } 
            set { 
               speed = value;
               OnpropertyChanged(nameof(speed)); // 속성값이 변경되는 것을 알려주려면 이 작업이 반드시 필요
            } 
        } // 속성
        public Color Color { get { return color; } set { color = value; OnpropertyChanged(nameof(color)); } }

    }
}
