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
using System.Diagnostics;
using Scheduler.Utils;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> selectedDays = new List<string>();
        private List<string> selectedIntervals = new List<string>();
        private Entry entry = new Entry();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Summon_CustomInterval(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Is this working?");
            CustomInterval customIntervvalWindow = new CustomInterval();
            customIntervvalWindow.Show();

        }
        
        private void DaysListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // clear existing items in arrary
            selectedDays.Clear();

            // get all of the selected days
            foreach (var selectedDay in daysListBox.SelectedItems)
            {
                if (selectedDay is ListBoxItem listBoxItem)
                {
                    string day = listBoxItem.Content.ToString();
                    selectedDays.Add(day);
                }
            }

        }

        private void IntervalsListBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            var selectedInterval = intervalsListBox.SelectedItem;

            if (selectedInterval is ListBoxItem listBoxItem)
            {
                entry.Interval = listBoxItem.Content.ToString();
            }

        }
        
        private void Test_button(Object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("Selected Days: " + string.Join(", ", selectedDays));
            //string.Join(",", entry.Days);
            entry.Days = selectedDays;
            entry.formatDays();
            entry.testEntry();
        }

    }
}
