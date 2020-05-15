using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailButtonManager : INotifyCollectionChanged, INotifyPropertyChanged
    {
        private ObservableCollection<RetailButton> retailButtons = new ObservableCollection<RetailButton>();

        public ObservableCollection<RetailButton> RetailButtons
        {
            get { return retailButtons; }
            set { retailButtons = value; }
        }

        public void InitializeRetailButtons() //Call at beginning of program once
        {
            RetailButton newButton = new RetailButton();//Blank button to initialize each collection

            for (int i = 0; i < 50; ++i) // 50 button limit for UI
            {
                RetailButtons.Add(newButton);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, args);
            }
        }
    }
}
