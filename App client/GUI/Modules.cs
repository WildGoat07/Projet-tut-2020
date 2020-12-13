using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    public abstract class DateDepedantModule : Module
    {
        internal ComboBox? years;
        public AnneeUniv? CurrentYear => years?.SelectedItem as AnneeUniv;

        public abstract void DateChanged(AnneeUniv? year);
    }

    public abstract class Module
    {
        protected Module()
        {
            Content = null;
            Title = "New Module";
        }

        public event Action? OnClose;

        public UIElement? Content { get; private set; }

        public string Title { get; private set; }

        public void CloseModule() => OnClose?.Invoke();

        public abstract Task RefreshAsync();

        public override string ToString() => Title;
    }
}