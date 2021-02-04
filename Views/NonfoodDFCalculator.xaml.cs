using AddInCalculator2._0.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AddInCalculator2._0.Views
{
    /// <summary>
    /// The nonfood drug fact calculator
    /// </summary>
    public sealed partial class NonfoodDFCalculator : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the input focus to ensure that keyboard events are raised.
            this.Loaded += delegate { this.Focus(FocusState.Programmatic); };
        }

        public NonfoodDFCalculator()
        {
            this.InitializeComponent();
            ViewModel = new CalculatorViewModel();
            this.DataContext = ViewModel;
        }

        public CalculatorViewModel ViewModel { get; set; }
        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {

        }

        #region Keyboard Accelerator's

        private void KeyboardAccelerator_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {

            VisualStateManager.GoToState(OneButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick;
            timer.Stop();
            VisualStateManager.GoToState(OneButton, "Normal", true);
        }

        private void KeyboardAccelerator_Invoked_1(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(TwoButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick2;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }
        private void Timer_Tick2(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick2;
            timer.Stop();
            VisualStateManager.GoToState(TwoButton, "Normal", true);
        }

        private void Accelerator3(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(ThreeButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick3;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }
        private void Timer_Tick3(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick3;
            timer.Stop();
            VisualStateManager.GoToState(ThreeButton, "Normal", true);
        }

        private void Accelerator4(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(FourButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick4;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }
        private void Timer_Tick4(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick4;
            timer.Stop();
            VisualStateManager.GoToState(FourButton, "Normal", true);
        }

        private void Accelerator5(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(FiveButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick5;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }
        private void Timer_Tick5(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick5;
            timer.Stop();
            VisualStateManager.GoToState(FiveButton, "Normal", true);
        }

        private void Accelerator6(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(SixButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick6;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }
        private void Timer_Tick6(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick6;
            timer.Stop();
            VisualStateManager.GoToState(SixButton, "Normal", true);
        }

        private void Accelerator7(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(SevenButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick7;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }
        private void Timer_Tick7(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick7;
            timer.Stop();
            VisualStateManager.GoToState(SevenButton, "Normal", true);
        }

        private void Accelerator8(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(EightButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick8;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }
        private void Timer_Tick8(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick8;
            timer.Stop();
            VisualStateManager.GoToState(EightButton, "Normal", true);
        }

        private void Accelerator9(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(NineButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick9;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick9(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick9;
            timer.Stop();
            VisualStateManager.GoToState(NineButton, "Normal", true);
        }

        private void AcceleratorDecimal(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(PeriodButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick10;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick10(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick10;
            timer.Stop();
            VisualStateManager.GoToState(PeriodButton, "Normal", true);
        }

        private void AcceleratorMultiply(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(MultiplyButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick11;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick11(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick11;
            timer.Stop();
            VisualStateManager.GoToState(MultiplyButton, "Normal", true);
        }

        private void AcceleratorSubtract(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(SubtractButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick12;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick12(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick12;
            timer.Stop();
            VisualStateManager.GoToState(SubtractButton, "Normal", true);
        }

        private void AcceleratorAdd(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(AddButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick13;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick13(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick13;
            timer.Stop();
            VisualStateManager.GoToState(AddButton, "Normal", true);
        }

        private void AcceleratorDivide(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(DivideButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick14;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick14(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick14;
            timer.Stop();
            VisualStateManager.GoToState(DivideButton, "Normal", true);
        }

        private void AcceleratorBackspace(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(BackspaceButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick15;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick15(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick15;
            timer.Stop();
            VisualStateManager.GoToState(BackspaceButton, "Normal", true);
        }

        private void AcceleratorEqualTo(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            VisualStateManager.GoToState(EqualToButton, "Pressed", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick16;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        private void Timer_Tick16(object sender, object e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Tick -= Timer_Tick16;
            timer.Stop();
            VisualStateManager.GoToState(EqualToButton, "Normal", true);
        }
        #endregion
    }
}
