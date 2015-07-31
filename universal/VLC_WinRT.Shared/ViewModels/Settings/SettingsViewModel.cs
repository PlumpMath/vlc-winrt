﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using VLC_WinRT.Database;
using VLC_WinRT.Helpers;
using VLC_WinRT.Helpers.MusicLibrary;
using VLC_WinRT.Model.Music;
using VLC_WinRT.Model.Video;
using VLC_WinRT.Model;
using VLC_WinRT.Views.MusicPages;
using Windows.Storage;
using VLC_WinRT.Commands.Settings;
using VLC_WinRT.Utils;

namespace VLC_WinRT.ViewModels.Settings
{
    public class SettingsViewModel : BindableBase
    {
#if WINDOWS_APP
        private bool _isSidebarAlwaysMinimized = false;
        private List<StorageFolder> _musicFolders;
        private List<StorageFolder> _videoFolders;
        private bool _notificationOnNewSong;
        private bool _notificationOnNewSongForeground;
        private bool _continueVideoPlaybackInBackground;
#endif
        private OrderType _albumsOrderType;
        private OrderListing _albumsOrderListing;
        private MusicView _musicView;
        private VideoView _videoView;
        private VLCPage _homePage;
        private string _lastFmUserName;
        private string _lastFmPassword;
        private bool _lastFmIsConnected = false;
        private bool _hardwareAcceleration;
        private KeyboardActionDatabase _keyboardActionDatabase;

        public KeyboardActionDatabase KeyboardActionDatabase
        {
            get
            {
                _keyboardActionDatabase = new KeyboardActionDatabase();

                var actions = _keyboardActionDatabase.GetAllKeyboardActions();
                if (!actions.Any())
                {
                    // never set before, we need to do it ...
                    var actionsToSet = new List<KeyboardAction>()
                    {
                        new KeyboardAction()
                        {
                            Action = VLCAction.FullscreenToggle,
                            MainKey = VirtualKey.F
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.LeaveFullscreen,
                            MainKey = VirtualKey.Escape,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.PauseToggle,
                            MainKey = VirtualKey.Space
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.Faster,
                            MainKey = VirtualKey.Add
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.Slow,
                            MainKey = VirtualKey.Subtract,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.NormalRate,
                            MainKey = VirtualKey.Execute,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.Next,
                            MainKey = VirtualKey.N,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.Previous,
                            MainKey = VirtualKey.P,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.Stop,
                            MainKey = VirtualKey.S,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.Quit,
                            MainKey = VirtualKey.Q,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.VolumeUp,
                            MainKey = VirtualKey.Control,
                            SecondKey = VirtualKey.Add,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.VolumeDown,
                            MainKey = VirtualKey.Control,
                            SecondKey = VirtualKey.Subtract,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.Mute,
                            MainKey = VirtualKey.M,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.ChangeAudioTrack,
                            MainKey = VirtualKey.B,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.ChangeSubtitle,
                            MainKey = VirtualKey.V
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.OpenFile,
                            MainKey = VirtualKey.Control,
                            SecondKey = VirtualKey.O,
                        },
                        new KeyboardAction()
                        {
                            Action = VLCAction.OpenNetwork,
                            MainKey = VirtualKey.Control,
                            SecondKey = VirtualKey.N
                        }
                    };
                    _keyboardActionDatabase.AddKeyboardActions(actionsToSet);
                }
                return _keyboardActionDatabase;
            }
        }

#if WINDOWS_APP
        public bool ContinueVideoPlaybackInBackground
        {
            get { return _continueVideoPlaybackInBackground; }
            set
            {
                SetProperty(ref _continueVideoPlaybackInBackground, value);
                ApplicationSettingsHelper.SaveSettingsValue("ContinueVideoPlaybackInBackground", value);
            }
        }
        public bool IsSidebarAlwaysMinimized
        {
            get { return _isSidebarAlwaysMinimized; }
            set
            {
                SetProperty(ref _isSidebarAlwaysMinimized, value);
                ApplicationSettingsHelper.SaveSettingsValue("IsSidebarAlwaysMinimized", value);
            }
        }
#endif

        public List<VLCPage> HomePageCollection { get; set; } = new List<VLCPage>()
        {
            VLCPage.MainPageHome,
            VLCPage.MainPageVideo,
            VLCPage.MainPageMusic
        };

        public List<OrderType> AlbumsOrderTypeCollection
        { get; set; }
        = new List<OrderType>()
        {
            OrderType.ByArtist,
            OrderType.ByDate,
            OrderType.ByAlbum,
        };

        public List<OrderListing> AlbumsListingTypeCollection
        { get; set; }
        = new List<OrderListing>()
        {
            OrderListing.Ascending,
            OrderListing.Descending
        };


        public List<MusicView> MusicViewCollection
        { get; set; }
        = new List<MusicView>()
        {
            MusicView.Artists,
            MusicView.Albums,
            MusicView.Songs,
            MusicView.Playlists
        };

        public List<VideoView> VideoViewCollection
        { get; set; }
        = new List<VideoView>()
        {
            VideoView.Videos,
            VideoView.Shows,
            VideoView.CameraRoll
        };
#if WINDOWS_APP
        public List<StorageFolder> MusicFolders
        {
            get { return _musicFolders; }
            set { SetProperty(ref _musicFolders, value); }
        }

        public List<StorageFolder> VideoFolders
        {
            get { return _videoFolders; }
            set { SetProperty(ref _videoFolders, value); }
        }

        public AddFolderToLibrary AddFolderToLibrary { get; set; }
        public RemoveFolderFromVideoLibrary RemoveFolderFromVideoLibrary { get; set; }
        public RemoveFolderFromMusicLibrary RemoveFolderFromMusicLibrary { get; set; }
        public KnownLibraryId MusicLibraryId { get; set; }
        public KnownLibraryId VideoLibraryId { get; set; }

        public bool NotificationOnNewSong
        {
            get { return _notificationOnNewSong; }
            set
            {
                SetProperty(ref _notificationOnNewSong, value);
                ApplicationSettingsHelper.SaveSettingsValue("NotificationOnNewSong", value);
            }
        }

        public bool NotificationOnNewSongForeground
        {
            get { return _notificationOnNewSongForeground; }
            set
            {
                SetProperty(ref _notificationOnNewSongForeground, value);
                ApplicationSettingsHelper.SaveSettingsValue("NotificationOnNewSongForeground", value);
            }
        }
#endif
        public OrderType AlbumsOrderType
        {
            get
            {
                var albumsOrderType = ApplicationSettingsHelper.ReadSettingsValue("AlbumsOrderType");
                if (albumsOrderType == null)
                {
                    _albumsOrderType = OrderType.ByArtist;
                }
                else
                {
                    _albumsOrderType = (OrderType)albumsOrderType;
                }
                return _albumsOrderType;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("AlbumsOrderType", (int)value);
                if ((int)value == 0 || value != _albumsOrderType)
                    MusicLibraryManagement.OrderAlbums();
                SetProperty(ref _albumsOrderType, value);
            }
        }

        public OrderListing AlbumsOrderListing
        {
            get
            {
                var albumsOrderListing = ApplicationSettingsHelper.ReadSettingsValue("AlbumsOrderListing");
                if (albumsOrderListing == null)
                {
                    _albumsOrderListing = OrderListing.Ascending;
                }
                else
                {
                    _albumsOrderListing = (OrderListing)albumsOrderListing;
                }
                return _albumsOrderListing;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("AlbumsOrderListing", (int)value);
                if (value != _albumsOrderListing)
                    MusicLibraryManagement.OrderAlbums();
                SetProperty(ref _albumsOrderListing, value);
            }
        }

        public MusicView MusicView
        {
            get
            {
                var musicView = ApplicationSettingsHelper.ReadSettingsValue("MusicView");
                if (musicView == null)
                {
                    _musicView = MusicView.Artists;
                }
                else
                {
                    _musicView = (MusicView)musicView;
                }
                return _musicView;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("MusicView", (int)value);
                if (value != _musicView)
                {
                    Locator.MainVM.ChangeMainPageMusicViewCommand.Execute((int)value);
                }
                SetProperty(ref _musicView, value);
            }
        }

        public VideoView VideoView
        {
            get
            {
                var videoView = ApplicationSettingsHelper.ReadSettingsValue("VideoView");
                if (videoView == null)
                {
                    _videoView = VideoView.Videos;
                }
                else
                {
                    _videoView = (VideoView)videoView;
                }
                return _videoView;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("VideoView", (int)value);
                if (value != _videoView)
                {
                    Locator.MainVM.ChangeMainPageVideoViewCommand.Execute((int)value);
                }
                SetProperty(ref _videoView, value);
            }
        }

        public string LastFmUserName
        {
            get
            {
                var username = ApplicationSettingsHelper.ReadSettingsValue("LastFmUserName");
                if (username == null)
                {
                    _lastFmUserName = "";
                }
                else
                {
                    _lastFmUserName = username.ToString();
                }
                return _lastFmUserName;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("LastFmUserName", value);
                SetProperty(ref _lastFmUserName, value);
            }
        }

        public string LastFmPassword
        {
            get
            {
                var password = ApplicationSettingsHelper.ReadSettingsValue("LastFmPassword");
                if (password == null)
                {
                    _lastFmPassword = "";
                }
                else
                {
                    _lastFmPassword = password.ToString();
                }
                return _lastFmPassword;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("LastFmPassword", value);
                SetProperty(ref _lastFmPassword, value);
            }
        }

        public bool LastFmIsConnected
        {
            get
            {
                var lastFmIsConnected = ApplicationSettingsHelper.ReadSettingsValue("LastFmIsConnected");
                if (lastFmIsConnected == null)
                {
                    _lastFmIsConnected = false;
                }
                else
                {
                    _lastFmIsConnected = (bool) lastFmIsConnected;
                }
                return _lastFmIsConnected;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("LastFmIsConnected", value);
                SetProperty(ref _lastFmIsConnected, value);
            }
        }

        public bool HardwareAccelerationEnabled
        {
            get
            {
                var hardwareAccelerationEnabled = ApplicationSettingsHelper.ReadSettingsValue("HardwareAccelerationEnabled");
                if(hardwareAccelerationEnabled == null)
                {
                    _hardwareAcceleration = true;
                }
                else
                {
                    _hardwareAcceleration = (bool)hardwareAccelerationEnabled;
                }
                return _hardwareAcceleration;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("HardwareAccelerationEnabled", value);
                SetProperty(ref _hardwareAcceleration, value);
            }
        }

        public VLCPage HomePage
        {
            get
            {
                var homePage = ApplicationSettingsHelper.ReadSettingsValue("Homepage");
                if (homePage == null)
                {
                    _homePage = VLCPage.MainPageHome;
                }
                else
                {
                    _homePage = (VLCPage)homePage;
                }
                return _homePage;
            }
            set
            {
                ApplicationSettingsHelper.SaveSettingsValue("Homepage", (int)value);
                SetProperty(ref _homePage, value);
            }
        }

        public SettingsViewModel()
        {
            Initialize();
        }
        
        public async Task Initialize()
        {
#if WINDOWS_APP
            App.Dispatcher?.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                MusicLibraryId = KnownLibraryId.Music;
                VideoLibraryId = KnownLibraryId.Videos;

                AddFolderToLibrary = new AddFolderToLibrary();
                RemoveFolderFromMusicLibrary = new RemoveFolderFromMusicLibrary();
                RemoveFolderFromVideoLibrary = new RemoveFolderFromVideoLibrary();

                var notificationOnNewSong = ApplicationSettingsHelper.ReadSettingsValue("NotificationOnNewSong");
                NotificationOnNewSong = notificationOnNewSong != null && (bool)notificationOnNewSong;

                var notificationOnNewSongForeground = ApplicationSettingsHelper.ReadSettingsValue("NotificationOnNewSongForeground");
                NotificationOnNewSongForeground = notificationOnNewSongForeground != null && (bool)notificationOnNewSongForeground;
                var sidebar = ApplicationSettingsHelper.ReadSettingsValue("IsSidebarAlwaysMinimized");
                if (sidebar != null) IsSidebarAlwaysMinimized = (bool)sidebar;

                var continuePlaybackInBackground = ApplicationSettingsHelper.ReadSettingsValue("ContinueVideoPlaybackInBackground");
                if (continuePlaybackInBackground != null)
                    ContinueVideoPlaybackInBackground = (bool)continuePlaybackInBackground;
                await GetLibrariesFolders();
            });
#endif
        }

#if WINDOWS_APP
        public async Task GetLibrariesFolders()
        {
            var musicLib = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Music);
            MusicFolders = musicLib.Folders.ToList();

            var videosLib = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Videos);
            VideoFolders = videosLib.Folders.ToList();
        }
#endif
    }
}
