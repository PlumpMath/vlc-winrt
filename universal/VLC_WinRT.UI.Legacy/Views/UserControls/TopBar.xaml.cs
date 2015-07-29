﻿using Microsoft.Xaml.Interactivity;
using VLC_WinRT.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VLC_WinRT.Views.UserControls
{
    public sealed partial class TopBar : UserControl
    {
        public TopBar()
        {
            this.InitializeComponent();
            this.Loaded += TopBar_Loaded;
        }

        private void TopBar_Loaded(object sender, RoutedEventArgs e)
        {
            Responsive(Window.Current.Bounds.Width);
            Window.Current.SizeChanged += Current_SizeChanged;
            this.Unloaded += MusicPaneButtons_Unloaded;
        }

        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            Responsive(e.Size.Width);
        }

        void MusicPaneButtons_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Current_SizeChanged;
        }

        void Responsive(double width)
        {
            if (width <= 700)
                VisualStateUtilities.GoToState(this, "Minimal", false);
            else if(width <= 1050)
                VisualStateUtilities.GoToState(this, "Medium", false);
            else
                VisualStateUtilities.GoToState(this, "Normal", false);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Locator.NavigationService.GoBack_Specific();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(MusicSearchBox.Text) && !string.IsNullOrEmpty(Locator.SearchVM.SearchTag))
            {
                Locator.NavigationService.GoBack_HideFlyout();
            }
            Locator.SearchVM.SearchTag = MusicSearchBox.Text;
        }
    }
}
