using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GUI.modules
{
    internal class DummyModule : Module
    {
        private Label label;

        public DummyModule()
        {
            Title = "Dummy module";
            label = new Label();
            label.Content = "label";
        }

        public override UIElement? Content => label;
        public string LabelString { get => (string)label.Content; set => label.Content = value; }
        public override string Title { get; }

        public override async Task RefreshAsync()
        {
            await Task.Delay(2500);
            LabelString += '+';
        }
    }
}