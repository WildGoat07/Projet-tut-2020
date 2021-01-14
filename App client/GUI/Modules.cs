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
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        internal ComboBox years;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public AnneeUniv CurrentYear => ((ToStringOverrider<AnneeUniv>)years.SelectedItem).Value;

        public abstract void DateChanged();
    }

    public abstract class Module
    {
        public event Action? OnClose;

        public abstract bool Closeable { get; }

        public abstract UIElement Content { get; }

        public abstract string Title { get; }

        public void CloseModule() => OnClose?.Invoke();

        public abstract Task RefreshAsync();

        public override string ToString() => Title;
    }
}