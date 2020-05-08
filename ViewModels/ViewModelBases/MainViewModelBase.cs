﻿using AddInCalculator2._0.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AddInCalculator2._0.ViewModels.ViewModelBases
{
    public class MainViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /******************************************************************/
        /*************************** Navigation ***************************/
        /******************************************************************/
        public string CurrentPageKey => throw new NotImplementedException();

        public void GoBack()
        {
            var rootFrame = Window.Current.Content as Frame;
            var mainPage = rootFrame.Content as MainPage;
            mainPage.NavigationFrame.GoBack();
        }

        public void NavigateTo(Type viewType)
        {
            var rootFrame = Window.Current.Content as Frame;
            var mainPage = rootFrame.Content as MainPage;
            mainPage.NavigationFrame.Navigate(viewType);
        }

        public void NavigateTo(string pageKey)
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
