﻿using Windows.UI.Core;
using VLC_WINRT.Common;
using VLC_WinRT.Views.MusicPages;
using System;

namespace VLC_WinRT.Commands.Music
{
    public class ShowCreateNewPlaylistPane : AlwaysExecutableCommand
    {
        public override async void Execute(object parameter)
        {
            await App.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var createPlaylist = new CreateNewPlaylist();
#if WINDOWS_PHONE_APP
                await createPlaylist.ShowAsync();
#else
                createPlaylist.ShowIndependent();
#endif
            });
        }
    }
}