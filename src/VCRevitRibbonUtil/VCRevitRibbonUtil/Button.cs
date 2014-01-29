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
using System.Windows.Media;
using Autodesk.Revit.UI;
using VCRevitRibbonUtil.Helpers;

namespace VCRevitRibbonUtil
{
    public class Button
    {
        private readonly string _name;
        private readonly string _text;
        private readonly string _className;
        private ImageSource _largeImage;
        private ImageSource _smallImage;
        private string _description;
        private string _assemblyLocation;

        public Button(string name, 
                      string text, 
                      Type externalCommandType)
        {
            _name = name;
            _text = text;
            _className = externalCommandType.FullName;
            _assemblyLocation =
                externalCommandType.Assembly.Location;
        }
       

        public Button SetLargeImage (ImageSource largeImage)
        {
            _largeImage = largeImage;
            return this;
        }

        public Button SetLargeImage(Bitmap largeImage)
        {
            _largeImage = BitmapSourceConverter.ConvertFromImage(largeImage);
            return this;
        }

        public Button SetSmallImage(ImageSource smallImage)
        {
            _smallImage = smallImage;
            return this;
        }

        public Button SetSmallImage(Bitmap smallImage)
        {
            _smallImage = BitmapSourceConverter.ConvertFromImage(smallImage);
            return this;
        }

        internal PushButtonData Finish()
        {
           PushButtonData pushButtonData = 
                new PushButtonData(_name,
                                   _text,
                                   _assemblyLocation,
                                   _className);

            if (_largeImage != null)
            {
                pushButtonData.LargeImage = _largeImage;
            }

            if (_smallImage != null)
            {
                pushButtonData.Image = _smallImage;
            }

            if (_description != null)
            {
                pushButtonData.LongDescription = _description;
            }

            //_panel.Source.AddItem(pushButtonData);

            return pushButtonData;
        }

        public Button SetLongDescription(string description)
        {
            _description = description;

            return this;
        }
    }
}