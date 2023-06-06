using InfiniteScrollWithBindableLayout.ViewModels;
using System.Diagnostics;

namespace InfiniteScrollWithBindableLayout;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = _viewModel = viewModel;
    }

    private double currentScrollYPosition = 0;
    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        if (!(sender is ScrollView scrollView)) return;

        var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;
        if ((scrollSpace - 20) > e.ScrollY || currentScrollYPosition > e.ScrollY) return;

        currentScrollYPosition = e.ScrollY;

        //Load more items
        _viewModel.LoadMoreAsync();
    }
}

