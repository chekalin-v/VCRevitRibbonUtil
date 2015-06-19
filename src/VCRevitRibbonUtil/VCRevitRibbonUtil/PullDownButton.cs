using System;
using System.Collections.Generic;
using System.ComponentModel;
using Autodesk.Revit.UI;


namespace VCRevitRibbonUtil
{
    public class PulldownButton : Button
    {
        private readonly IList<Button> _buttons = new List<Button>();

        public PulldownButton(string name, string text) :
            base(name, text, null)
        {
        }

        internal override ButtonData Finish()
        {
            PulldownButtonData pulldownButtonData =
                new PulldownButtonData(_name,
                    _text);


            if (_largeImage != null)
            {
                pulldownButtonData.LargeImage = _largeImage;
            }

            if (_smallImage != null)
            {
                pulldownButtonData.Image = _smallImage;
            }

            if (_description != null)
            {
                pulldownButtonData.LongDescription = _description;
            }

            if (_contextualHelp != null)
            {
                pulldownButtonData.SetContextualHelp(_contextualHelp);
            }

            //pulldownButtonData.

            //_panel.Source.AddItem(pushButtonData);

            return pulldownButtonData;
        }

        public PulldownButton CreateButton<TExternalCommandClass>(string name,
                          string text)
                        where TExternalCommandClass : class, IExternalCommand
        {
            var commandClassType = typeof(TExternalCommandClass);

            return CreateButton(name, text, commandClassType, null);
        }

        public PulldownButton CreateButton<TExternalCommandClass>(string name,
                                  string text,
                                  Action<Button> action)
            where TExternalCommandClass : class, IExternalCommand
        {
            var commandClassType = typeof(TExternalCommandClass);

            return CreateButton(name, text, commandClassType, action);
        }

        public PulldownButton CreateButton(string name,
                                  string text,
                                  Type externalCommandType)
        {
            return CreateButton(name, text, externalCommandType, null);
        }

        public PulldownButton CreateButton(string name,
                                   string text,
                                   Type externalCommandType,
                                   Action<Button> action)
        {

            var button = new Button(name,
                              text,
                              externalCommandType);
            if (action != null)
            {
                action.Invoke(button);
            }

            Buttons.Add(button);

            return this;
        }

        //public PulldownButton CreateSeparator()
        //{
        //    return this;
        //}

        public int ItemsCount
        {
            get { return Buttons.Count; }
        }

        public IList<Button> Buttons
        {
            get { return _buttons; }
        }

        internal void BuildButtons(Autodesk.Revit.UI.PulldownButton pulldownButton)
        {
            foreach (var button in Buttons)
            {
                pulldownButton.AddPushButton(button.Finish() as PushButtonData);
            }
        }
    }
}