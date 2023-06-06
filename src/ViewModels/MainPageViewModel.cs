using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using InfiniteScrollWithBindableLayout.Utils;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace InfiniteScrollWithBindableLayout.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        #region Propertise
        [ObservableProperty]
        private ObservableRangeCollection<string> displayStringList;

        [ObservableProperty]
        bool isBusy;

        private List<string> displayList;
        private int batchSize = 50;
        private int currentIndex = 1;
        #endregion

        #region Constructor
        public MainPageViewModel()
        {
            Init();
        }
        #endregion

        #region Methods

        private void Init()
        {
            Random randomNumber = new Random();
            displayList = new List<string>();

            for (var i = 0; i <= 1000; i++)
            {
                displayList.Add(randomNumber.Next(10000, 99999).ToString());
            }
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DisplayStringList = new ObservableRangeCollection<string>(displayList.Take(batchSize).ToList());
            });
        }

        public async void LoadMoreAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await Task.Delay(1000); // Fake and wait API call delay

            DisplayStringList.AddRange(
                displayList.Skip(batchSize * currentIndex).Take(batchSize)
            );
            currentIndex++;

            IsBusy = false;
        }
        #endregion
    }
}
