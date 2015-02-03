﻿using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using VLC_WINRT_APP.Helpers;

namespace VLC_WINRT_APP.Views.MusicPages
{
    public sealed partial class ArtistFlyout : SettingsFlyout
    {
        public ArtistFlyout()
        {
            this.InitializeComponent();
        }

        private void AppBar_loaded(object sender, RoutedEventArgs e)
        {
            var buttons = AppBarHelper.SetArtistPageButtons(new List<ICommandBarElement>());
            customAppBar.PrimaryCommands = buttons;
        }

        private void RootGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
