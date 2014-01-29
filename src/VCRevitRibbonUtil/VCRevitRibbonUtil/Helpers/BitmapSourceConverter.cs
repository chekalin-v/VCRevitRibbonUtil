/* 
 * Copyright 2012 © Victor Chekalin
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 * 
 */

using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace VCRevitRibbonUtil.Helpers
{
    public static class BitmapSourceConverter
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource ConvertFromImage(Bitmap image)
        {
            IntPtr hBitmap = image.GetHbitmap();

            try
            {
                var bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    //Properties.Resources.logoITC_Revit.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

                return bs;

            }
            finally
            {
                DeleteObject(hBitmap);
            }

        }

        public static BitmapSource ConvertFromIcon(Icon icon)
        {

            try
            {
                var bs = Imaging
                    .CreateBitmapSourceFromHIcon(icon.Handle,
                                                 new Int32Rect(0, 0, icon.Width, icon.Height),
                                                 BitmapSizeOptions.FromWidthAndHeight(icon.Width,
                                                                                      icon.Height));
                return bs;
            }
            finally
            {
                DeleteObject(icon.Handle);
            }
        }
    }
}