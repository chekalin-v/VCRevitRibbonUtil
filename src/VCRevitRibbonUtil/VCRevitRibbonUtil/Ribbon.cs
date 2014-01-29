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
using Autodesk.Revit.UI;
using Autodesk.Windows;
using UIFramework;

namespace VCRevitRibbonUtil
{
    public class Ribbon
    {
        private readonly UIControlledApplication _application;
        private readonly RibbonControl _ribbonControl;

        public Ribbon(UIControlledApplication application)
        {
            _application = application;
            _ribbonControl = (RibbonControl) RevitRibbonControl.RibbonControl;
            if (_ribbonControl == null)
                throw new NotSupportedException("Could not initialize Revit ribbon control");
        }

        public static Ribbon GetApplicationRibbon(UIControlledApplication application)
        {
            return new Ribbon(application);
        }

        internal UIControlledApplication Application
        {
            get { return _application; }
        }

        public Tab Tab(string tabTitle)
        {            
            foreach (var tab in _ribbonControl.Tabs)
            {
                if (tab.Title.Equals(tabTitle))
                {
                    

                    return new Tab(this, tabTitle);
                }
            }

            //RibbonTab ribbonTab =
            //    new RibbonTab()
            //        {
            //            Title = tabTitle,
            //            IsVisible = true,
            //            Name = tabTitle,

            //        };
            //_ribbonControl.Tabs.Add(ribbonTab);

            _application.CreateRibbonTab(tabTitle);
            return new Tab(this, tabTitle);
        }


        public Tab Tab(Autodesk.Revit.UI.Tab systemTab)
        {
            return new Tab(this, systemTab);
        }
    }
}