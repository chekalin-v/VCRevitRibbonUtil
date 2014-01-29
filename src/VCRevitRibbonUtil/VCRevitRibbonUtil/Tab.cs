/* 
 * Copyright 2012 © Victor Chekalin
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 * 
 */

using System.Collections.Generic;
using Autodesk.Revit.UI;

namespace VCRevitRibbonUtil
{
    public class Tab
    {
        private readonly Ribbon _ribbon;
        private readonly Autodesk.Revit.UI.Tab? _systemTab;
        private readonly string _name;
        //private readonly RibbonTab _tab;

        public Tab(Ribbon ribbon, string name)
        {
            _ribbon = ribbon;
            _name = name;
        }

        public Tab(Ribbon ribbon, Autodesk.Revit.UI.Tab systemTab)
        {
            _ribbon = ribbon;
            _systemTab = systemTab;            
        }

        internal Ribbon Ribbon
        {
            get { return _ribbon; }
        }

        //public string Title { get { return _tab.Title; }}

        public Panel Panel(string panelTitle)
        {
            //foreach (var panel in _tab.Panels)
            //{
            //    if (panel.Source.Title.Equals(panelTitle))
            //    {
                    
            //        return new Panel(this, panel);
            //    }
            //}


            List<RibbonPanel> panels;
            if (_systemTab == null)
            {
                panels = _ribbon.Application.GetRibbonPanels(_name);
            }
            else
            {
                panels = _ribbon.Application.GetRibbonPanels(_systemTab.Value);
            }
            foreach (var panel in panels)
            {
                if (panel.Name.Equals(panelTitle))
                {
                    panel.AddSeparator();
                    return new Panel(this, panel);
                }
            }
            
            RibbonPanel ribbonPanel;
            if (_systemTab == null)
                ribbonPanel = _ribbon.Application.CreateRibbonPanel(_name, panelTitle);
            else
                ribbonPanel = _ribbon.Application.CreateRibbonPanel(_systemTab.Value, panelTitle);
                
            
            

            return new Panel(this, ribbonPanel);
        }
    }
}