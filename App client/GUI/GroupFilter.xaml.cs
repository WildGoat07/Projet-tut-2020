using GUI.filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Logique d'interaction pour GroupFilter.xaml
    /// </summary>
    public partial class GroupFilter : UserControl
    {
        private IFilter filter;
        private int mode;
        private string name;

        public GroupFilter(IFilter filter)
        {
            InitializeComponent();
            this.filter = filter;
            header.Text = filter.Name;
            name = filter.Name;
            visibleZone.Visibility = Visibility.Collapsed;
            if (filter is Values v)
            {
                mode = 0;
                foreach (var item in v.Filters)
                {
                    visibleZone.Visibility = Visibility.Visible;
                    var panel = new StackPanel { Orientation = Orientation.Horizontal };
                    var text = new TextBox
                    {
                        Margin = new Thickness(5),
                        MinWidth = 120,
                        MaxWidth = 200,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = item
                    };
                    panel.Children.Add(text);
                    panel.Tag = text;
                    var button = new Button
                    {
                        Style = addFilter.Style,
                        Margin = new Thickness(5),
                        Content = new Image
                        {
                            Stretch = Stretch.None,
                            Source = App.DeleteFilter
                        },
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    panel.Children.Add(button);
                    button.Click += (sender, e) => filters.Children.Remove(panel);
                    filters.Children.Add(panel);
                }
            }
            else if (filter is ListValues l)
            {
                mode = 3;
                foreach (var item in l.Filters)
                {
                    visibleZone.Visibility = Visibility.Visible;
                    var panel = new StackPanel { Orientation = Orientation.Horizontal };
                    var values = new ComboBox
                    {
                        Margin = new Thickness(5),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    foreach (var item2 in l.Values)
                        values.Items.Add(item2);
                    values.SelectedIndex = item;
                    panel.Children.Add(values);
                    panel.Tag = values;
                    var button = new Button
                    {
                        Style = addFilter.Style,
                        Margin = new Thickness(5),
                        Content = new Image
                        {
                            Stretch = Stretch.None,
                            Source = App.DeleteFilter
                        },
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    panel.Children.Add(button);
                    button.Click += (sender, e) => filters.Children.Remove(panel);
                    filters.Children.Add(panel);
                }
            }
            else if (filter is IntRange i)
            {
                mode = 1;
                if (i.Min != null || i.Max != null)
                {
                    visibleZone.Visibility = Visibility.Visible;
                    var panel = new StackPanel { Orientation = Orientation.Horizontal };
                    panel.Children.Add(new TextBlock
                    {
                        Text = "De :",
                        Margin = new Thickness(5),
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    var min = new TextBox
                    {
                        Margin = new Thickness(5),
                        MinWidth = 50,
                        MaxWidth = 75,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = i.Min != null ? i.Min.ToString() : ""
                    };
                    panel.Children.Add(min);
                    panel.Children.Add(new TextBlock
                    {
                        Text = "à",
                        Margin = new Thickness(5),
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    var max = new TextBox
                    {
                        Margin = new Thickness(5),
                        MinWidth = 50,
                        MaxWidth = 75,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = i.Max != null ? i.Max.ToString() : ""
                    };
                    panel.Children.Add(max);
                    panel.Tag = (min, max);
                    var button = new Button
                    {
                        Style = addFilter.Style,
                        Margin = new Thickness(5),
                        Content = new Image
                        {
                            Stretch = Stretch.None,
                            Source = App.DeleteFilter
                        },
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    panel.Children.Add(button);
                    button.Click += (sender, e) =>
                    {
                        filters.Children.Remove(panel);
                        addFilter.IsEnabled = true;
                    };
                    filters.Children.Add(panel);
                    addFilter.IsEnabled = false;
                }
            }
            else if (filter is FloatRange f)
            {
                mode = 2;
                if (f.Min != null || f.Max != null)
                {
                    visibleZone.Visibility = Visibility.Visible;
                    var panel = new StackPanel { Orientation = Orientation.Horizontal };
                    panel.Children.Add(new TextBlock
                    {
                        Text = "De :",
                        Margin = new Thickness(5),
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    var min = new TextBox
                    {
                        Margin = new Thickness(5),
                        MinWidth = 50,
                        MaxWidth = 75,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = f.Min != null ? f.Min.ToString() : ""
                    };
                    panel.Children.Add(min);
                    panel.Children.Add(new TextBlock
                    {
                        Text = "à",
                        Margin = new Thickness(5),
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    var max = new TextBox
                    {
                        Margin = new Thickness(5),
                        MinWidth = 50,
                        MaxWidth = 75,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = f.Max != null ? f.Max.ToString() : ""
                    };
                    panel.Children.Add(max);
                    panel.Tag = (min, max);
                    var button = new Button
                    {
                        Style = addFilter.Style,
                        Margin = new Thickness(5),
                        Content = new Image
                        {
                            Stretch = Stretch.None,
                            Source = App.DeleteFilter
                        },
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    button.Click += (sender, e) =>
                    {
                        filters.Children.Remove(panel);
                        addFilter.IsEnabled = true;
                    };
                    filters.Children.Add(panel);
                    addFilter.IsEnabled = false;
                }
            }
            else
                throw new ArgumentException("Invalid filter type");
            header.Text += visibleZone.Visibility == Visibility.Collapsed ? " ↑" : " ↓";
        }

        public IFilter GetFilter()
        {
            switch (mode)
            {
                case 0:
                    return new Values
                    {
                        Name = name,
                        Filters = (from item in filters.Children.Cast<FrameworkElement>()
                                   where !string.IsNullOrWhiteSpace(((TextBox)item.Tag).Text)
                                   select ((TextBox)item.Tag).Text).ToArray()
                    };

                case 1:
                    {
                        int? min = null;
                        int? max = null;
                        if (filters.Children.Count == 1)
                        {
                            var tuple = ((TextBox, TextBox))((FrameworkElement)filters.Children[0]).Tag;
                            if (!string.IsNullOrWhiteSpace(tuple.Item1.Text))
                                min = int.Parse(tuple.Item1.Text);
                            if (!string.IsNullOrWhiteSpace(tuple.Item2.Text))
                                max = int.Parse(tuple.Item2.Text);
                        }
                        return new IntRange
                        {
                            Name = name,
                            Min = min,
                            Max = max
                        };
                    }
                case 2:
                    {
                        float? min = null;
                        float? max = null;
                        if (filters.Children.Count == 1)
                        {
                            var tuple = ((TextBox, TextBox))((FrameworkElement)filters.Children[0]).Tag;
                            if (!string.IsNullOrWhiteSpace(tuple.Item1.Text))
                                min = float.Parse(tuple.Item1.Text);
                            if (!string.IsNullOrWhiteSpace(tuple.Item2.Text))
                                max = float.Parse(tuple.Item2.Text);
                        }
                        return new FloatRange
                        {
                            Name = name,
                            Min = min,
                            Max = max
                        };
                    }
                case 3:
                    return ((ListValues)filter) with
                    {
                        Name = name,
                        Filters = (from item in filters.Children.Cast<FrameworkElement>()
                                   where ((ComboBox)item.Tag).SelectedIndex != -1
                                   select ((ComboBox)item.Tag).SelectedIndex).ToArray()
                    };

                default:
                    throw new NotSupportedException();
            };
        }

        public string? Validate()
        {
            if (mode != 0 && mode != 3)
            {
                foreach (StackPanel item in filters.Children.Cast<StackPanel>())
                {
                    var minmax = ((TextBox, TextBox))item.Tag;
                    if (mode == 1)
                    {
                        int dummy;
                        if (!string.IsNullOrWhiteSpace(minmax.Item1.Text) && !int.TryParse(minmax.Item1.Text, out dummy))
                            return $"Valeur minimale de {name} incorrecte";
                        if (!string.IsNullOrWhiteSpace(minmax.Item1.Text) && !int.TryParse(minmax.Item2.Text, out dummy))
                            return $"Valeur maximale de {name} incorrecte";
                    }
                    else
                    {
                        float dummy;
                        if (!string.IsNullOrWhiteSpace(minmax.Item1.Text) && !float.TryParse(minmax.Item1.Text, out dummy))
                            return $"Valeur minimale de {name} incorrecte";
                        if (!string.IsNullOrWhiteSpace(minmax.Item1.Text) && !float.TryParse(minmax.Item2.Text, out dummy))
                            return $"Valeur maximale de {name} incorrecte";
                    }
                }
            }
            return null;
        }

        private void addFilter_Click(object sender, RoutedEventArgs e)
        {
            switch (mode)
            {
                case 0:
                    {
                        var panel = new StackPanel { Orientation = Orientation.Horizontal };
                        var text = new TextBox
                        {
                            Margin = new Thickness(5),
                            MinWidth = 120,
                            MaxWidth = 200,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        panel.Children.Add(text);
                        panel.Tag = text;
                        var button = new Button
                        {
                            Style = addFilter.Style,
                            Margin = new Thickness(5),
                            Content = new Image
                            {
                                Stretch = Stretch.None,
                                Source = App.DeleteFilter
                            },
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        panel.Children.Add(button);
                        button.Click += (sender, e) => filters.Children.Remove(panel);
                        filters.Children.Add(panel);
                    }
                    break;

                case 1:
                case 2:
                    {
                        var panel = new StackPanel { Orientation = Orientation.Horizontal };
                        panel.Children.Add(new TextBlock
                        {
                            Text = "De :",
                            Margin = new Thickness(5),
                            VerticalAlignment = VerticalAlignment.Center
                        });
                        var min = new TextBox
                        {
                            Margin = new Thickness(5),
                            MinWidth = 50,
                            MaxWidth = 75,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        panel.Children.Add(min);
                        panel.Children.Add(new TextBlock
                        {
                            Text = "à",
                            Margin = new Thickness(5),
                            VerticalAlignment = VerticalAlignment.Center
                        });
                        var max = new TextBox
                        {
                            Margin = new Thickness(5),
                            MinWidth = 50,
                            MaxWidth = 75,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        panel.Children.Add(max);
                        panel.Tag = (min, max);
                        var button = new Button
                        {
                            Style = addFilter.Style,
                            Margin = new Thickness(5),
                            Content = new Image
                            {
                                Stretch = Stretch.None,
                                Source = App.DeleteFilter
                            },
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        panel.Children.Add(button);
                        button.Click += (sender, e) =>
                        {
                            filters.Children.Remove(panel);
                            addFilter.IsEnabled = true;
                        };
                        filters.Children.Add(panel);
                        addFilter.IsEnabled = false;
                    }
                    break;

                case 3:
                    {
                        var panel = new StackPanel { Orientation = Orientation.Horizontal };
                        var values = new ComboBox
                        {
                            Margin = new Thickness(5),
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        foreach (var item2 in ((ListValues)filter).Values)
                            values.Items.Add(item2);
                        panel.Children.Add(values);
                        panel.Tag = values;
                        var button = new Button
                        {
                            Style = addFilter.Style,
                            Margin = new Thickness(5),
                            Content = new Image
                            {
                                Stretch = Stretch.None,
                                Source = App.DeleteFilter
                            },
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        panel.Children.Add(button);
                        button.Click += (sender, e) => filters.Children.Remove(panel);
                        filters.Children.Add(panel);
                    }
                    break;

                default:
                    return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            visibleZone.Visibility = visibleZone.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            header.Text = name + (visibleZone.Visibility == Visibility.Collapsed ? " ↑" : " ↓");
        }
    }
}