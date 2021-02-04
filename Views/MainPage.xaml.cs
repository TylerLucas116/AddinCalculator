using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AddInCalculator2._0.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AddInCalculator2._0.Views
{
    /// <summary>
    /// The main page for the UI
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;

            StackPanel newTitleBar = new StackPanel(); //UI element to allow button's to have full PointerOver VisualState field
            Window.Current.SetTitleBar(newTitleBar);  //Set UI element behavior as defined above as the titlebar
        }

        public MainViewModel mainViewModel { get; set; }
        //Test
        public Frame NavigationFrame => ContentFrame;

    }
}
