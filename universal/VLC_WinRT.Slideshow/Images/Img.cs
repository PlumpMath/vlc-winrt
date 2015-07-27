﻿using System;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.Effects;

namespace Slide2D.Images
{
    public class Img
    {
        public CanvasBitmap Bmp { get; private set; }
        
        public GaussianBlurEffect GaussianBlurCache { get; set; }

        public string Src { get; private set; }
        public double Height { get { return Bmp.SizeInPixels.Height; } }
        public double Width { get { return Bmp.SizeInPixels.Width; } }
        public float Scale { get; set; }
        public float Opacity { get; set; }
        public Img(string source)
        {
            Src = source;
            Opacity = 0;
        }

        public async Task Initialize(CanvasAnimatedControl cac)
        {
            Bmp = await CanvasBitmap.LoadAsync(cac, new Uri(Src));
        }
    }
}