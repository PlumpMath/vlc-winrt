﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/

using libVLCX;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace VLC_WinRT.Services.Interface
{
    public interface IThumbnailService
    {
        Task<StorageItemThumbnail> GetThumbnail(StorageFile file);
        Task<PreparseResult> GetScreenshot(StorageFile file);
    }
}