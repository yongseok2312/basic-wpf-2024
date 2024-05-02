﻿using System.ComponentModel;

namespace ex05_wpf_bikeshop
{
    public class Notifier : INotifyPropertyChanged
    {
        // 우리가 만드는 클래스의 속성값이 변경되는 것을 알려주는 이벤트핸들러
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnpropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
