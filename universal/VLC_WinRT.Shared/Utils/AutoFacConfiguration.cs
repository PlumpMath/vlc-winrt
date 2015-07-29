﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/

using Autofac;
using Slide2D;
using VLC_WinRT.Services.Interface;
using VLC_WinRT.Services.RunTime;
using VLC_WinRT.ViewModels;
using VLC_WinRT.ViewModels.MusicVM;
using VLC_WinRT.ViewModels.Others;
using VLC_WinRT.ViewModels.RemovableDevicesVM;
using VLC_WinRT.ViewModels.Settings;
using VLC_WinRT.ViewModels.VideoVM;
using DesignTime = VLC_WinRT.Services.DesignTime;
using ThumbnailService = VLC_WinRT.Services.RunTime.ThumbnailService;

namespace VLC_WinRT.Utils
{
    public static class AutoFacConfiguration
    {
        /// <summary>
        /// Configures the AutoFac IoC Container for VLC_WINRT.
        /// </summary>
        /// <returns>Built container with all the </returns>
        /// TODO: Make a design time and a runtime configure?
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Register View Models
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                builder.RegisterType<DesignTime.ThumbnailService>().As<IThumbnailService>().SingleInstance();
            }
            else
            {
                // TODO: These should not be SingleInstance
                builder.RegisterType<MainVM>().SingleInstance();
                builder.RegisterType<MediaPlaybackViewModel>().SingleInstance();
                builder.RegisterType<MusicLibraryVM>().SingleInstance();
                builder.RegisterType<MusicPlayerVM>().SingleInstance();
                builder.RegisterType<VideoLibraryVM>().SingleInstance();
                builder.RegisterType<VideoPlayerVM>().SingleInstance();
                builder.RegisterType<SettingsViewModel>().SingleInstance();
                builder.RegisterType<SearchViewModel>().SingleInstance();

                builder.RegisterType<VLCExplorerViewModel>().SingleInstance();

                // Register Services
                builder.RegisterType<MetroSlideshow>().SingleInstance();
                builder.RegisterType<VLCService>().SingleInstance();
                builder.RegisterType<MFService>().SingleInstance();
#if WINDOWS_PHONE_APP
                builder.RegisterType<BGPlayerService>().SingleInstance();
#endif
                builder.RegisterType<NavigationService>().SingleInstance();
                builder.RegisterType<MusicMetaService>().SingleInstance();
                builder.RegisterType<KeyboardListenerService>().SingleInstance();
                builder.RegisterType<NetworkListenerService>().SingleInstance();

                builder.RegisterType<MouseService>().SingleInstance();

                builder.RegisterType<ExternalDeviceService>().SingleInstance();
                builder.RegisterType<SpecialThanksViewModel>().SingleInstance();
                builder.RegisterType<ThumbnailService>().As<IThumbnailService>().SingleInstance();
            }

            return builder.Build();
        }
    }
}
