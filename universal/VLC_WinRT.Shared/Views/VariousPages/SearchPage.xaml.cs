﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using VLC_WinRT.Helpers;
using VLC_WinRT.ViewModels;
#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;
#endif

namespace VLC_WinRT.Views.VariousPages
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
        }
#if WINDOWS_PHONE_APP
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtonsOnBackPressed;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            HardwareButtons.BackPressed -= HardwareButtonsOnBackPressed;
        }

        private void HardwareButtonsOnBackPressed(object sender, BackPressedEventArgs backPressedEventArgs)
        {
            backPressedEventArgs.Handled = true;
            if (App.ApplicationFrame.CanGoBack)
                App.ApplicationFrame.GoBack();
        }
#endif
        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Locator.MainVM.SearchTag = SearchBox.Text;
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
#if WINDOWS_APP
            this.Margin = new Thickness(24, 0, 24, 0);
#else
            this.Margin = new Thickness(12, 26, 12, 0);
#endif
        }

        private void CheckBox_Loaded(object sender, RoutedEventArgs e)
        {
#if WINDOWS_PHONE_APP
            (sender as CheckBox).Style = CheckBoxStyleCentered;
#else
            (sender as CheckBox).Margin = new Thickness(0,8,12,0);
#endif
        }
    }
}