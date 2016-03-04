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

namespace JPServFormatWin32Portable
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            big.IsChecked = true;
        }
        private Dictionary<string, int> sizes { get; } = new Dictionary<string, int>() { ["small"] = 10, ["medium"] = 20, ["big"] = 30 };
        public int size
        {
            get
            {
                foreach (MenuItem item in Size.Items)
                {
                    if (item.IsChecked)
                    {
                        return sizes[item.Name];
                    }
                }
                return 30;
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        
        {
            if (ip.Text.Contains("ws://"))
            {
                ip.Text = ip.Text.ToString().Substring(ip.Text.IndexOf(@"//") + 2);
            }
            results.Text = $@"[size={size}][url]http://agar.io/?ip=" + ip.Text + "[/url][/size]";
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(results.Text);
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Owner = this;
            about.ShowDialog();
        }

        private void paste_Click(object sender, RoutedEventArgs e)
        {
            ip.Text = Clipboard.GetText();

        }

        private void ip_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void big_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void medium_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void small_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

    // ---------------------------------------------------------


    public class MenuItemExtensions : DependencyObject
    {
        public static Dictionary<MenuItem, string> ElementToGroupNames = new Dictionary<MenuItem, string>();

        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.RegisterAttached("GroupName",
                                         typeof(string),
                                         typeof(MenuItemExtensions),
                                         new PropertyMetadata(string.Empty, OnGroupNameChanged));

        public static void SetGroupName(MenuItem element, string value)
        {
            element.SetValue(GroupNameProperty, value);
        }

        public static string GetGroupName(MenuItem element)
        {
            return element.GetValue(GroupNameProperty).ToString();
        }

        private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Add an entry to the group name collection
            var menuItem = d as MenuItem;

            if (menuItem != null)
            {
                string newGroupName = e.NewValue.ToString();
                string oldGroupName = e.OldValue.ToString();
                if (string.IsNullOrEmpty(newGroupName))
                {
                    //Removing the toggle button from grouping
                    RemoveCheckboxFromGrouping(menuItem);
                }
                else
                {
                    //Switching to a new group
                    if (newGroupName != oldGroupName)
                    {
                        if (!string.IsNullOrEmpty(oldGroupName))
                        {
                            //Remove the old group mapping
                            RemoveCheckboxFromGrouping(menuItem);
                        }
                        ElementToGroupNames.Add(menuItem, e.NewValue.ToString());
                        menuItem.Checked += MenuItemChecked;
                    }
                }
            }
        }

        private static void RemoveCheckboxFromGrouping(MenuItem checkBox)
        {
            ElementToGroupNames.Remove(checkBox);
            checkBox.Checked -= MenuItemChecked;
        }


        static void MenuItemChecked(object sender, RoutedEventArgs e)
        {
            var menuItem = e.OriginalSource as MenuItem;
            foreach (var item in ElementToGroupNames)
            {
                if (item.Key != menuItem && item.Value == GetGroupName(menuItem))
                {
                    item.Key.IsChecked = false;
                }
            }
        }
    }
}
