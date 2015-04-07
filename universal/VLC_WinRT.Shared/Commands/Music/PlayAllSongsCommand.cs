﻿using System.Linq;
using Windows.UI.Xaml.Controls;
using VLC_WINRT.Common;
using VLC_WinRT.Helpers.MusicPlayer;
using VLC_WinRT.Model;
using VLC_WinRT.Model.Music;
using VLC_WinRT.ViewModels;

namespace VLC_WinRT.Commands.Music
{
    public class PlayAllSongsCommand : AlwaysExecutableCommand
    {
        public override async void Execute(object parameter)
        {
            if (Locator.MusicLibraryVM.Tracks == null || !Locator.MusicLibraryVM.Tracks.Any()) return;
            var itemClickArgs = parameter as ItemClickEventArgs;
            if (itemClickArgs != null)
            {
                var selectedTrack = itemClickArgs.ClickedItem as TrackItem;
                var index = Locator.MusicLibraryVM.Tracks.IndexOf(selectedTrack);
                if (selectedTrack != null)
                {
                    await PlayMusicHelper.AddTrackCollectionToPlaylistAndPlay(Locator.MusicLibraryVM.Tracks, true, index);
                    Locator.NavigationService.Go(VLCPage.MusicPlayerPage);
                }
            }
        }
    }
}
