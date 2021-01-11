using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GUI.modules
{
    internal class DummyDateModule : DateDepedantModule
    {
        private Label label;

        public DummyDateModule()
        {
            Title = "Dummy date module";
            label = new Label();
            label.Content = "date";
        }

        public override bool Closeable => false;
        public override UIElement Content => label;
        public AnneeUniv? CurrDate { get; private set; }
        public string LabelString { get => (string)label.Content; set => label.Content = value; }
        public override string Title { get; }

        public override void DateChanged(AnneeUniv? year) => CurrDate = year;

        public override async Task RefreshAsync()
        {
            await Task.Delay(500);
            LabelString += CurrDate?.ToString() ?? "null";
        }
    }

    internal class DummyModule : Module
    {
        private Label label;

        public DummyModule()
        {
            Title = "Dummy module";
            label = new Label();
            label.Content = "label";
        }

        public override bool Closeable => true;
        public override UIElement Content => label;

        public string LabelString { get => (string)label.Content; set => label.Content = value; }
        public override string Title { get; }

        public override async Task RefreshAsync()
        {
            await Task.Delay(2500);
            LabelString += '+';
        }
    }
}