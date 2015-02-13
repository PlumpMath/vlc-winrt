﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace VLC_WINRT_APP.Views.UserControls
{
    public sealed partial class PivotHeaderControl : UserControl
    {
        public PivotHeaderControl()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Width < 600)
            {
                VisualStateUtilities.GoToState(this, "Snap", false);
            }
            else if (Window.Current.Bounds.Width < 800)
            {
                VisualStateUtilities.GoToState(this, "HalfSnap", false);
            }
            else
            {
                VisualStateUtilities.GoToState(this, "Normal", false);
            }
        }
    }
}