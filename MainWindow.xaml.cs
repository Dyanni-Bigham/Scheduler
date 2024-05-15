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
using Forms = System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Scheduler.Utils;
using Scheduler.exceptions;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> selectedDays = new List<string>();
        private Entry entry = new Entry();
        private Forms.NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            notifyIcon = new Forms.NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("./icon.ico");
            notifyIcon.Text = "Scheduler";
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.Visible = true;
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal; // Restore the window
                this.Show();
                this.Activate(); // Activate the window
            }
            else
            {
                this.WindowState = WindowState.Minimized; // Minimize the window
            }
        }

        private void Summon_CustomInterval(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Is this working?");
            CustomInterval customIntervvalWindow = new CustomInterval();
            customIntervvalWindow.Show();

        }

        private void DaysListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ensure entry.Days is initalized
            entry.Days ??= new List<string>();

            // clear existing items in the list
            entry.Days.Clear();

            // get all of the selcted days
            foreach (var selectedDay in daysListBox.SelectedItems) 
            {
                if (selectedDay is ListBoxItem listBoxItem) 
                {
                    string day = listBoxItem.Content.ToString();
                    entry.Days.Add(day);
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

        private void appListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            entry.Apps ??= new List<string>();
            entry.Apps.Clear();

            foreach (var selectedApp in appListBox.SelectedItems)
            {
                if (selectedApp is ListBoxItem listBoxItem)
                {
                    string app = listBoxItem.Content.ToString();
                    entry.Apps.Add(app);
                }
            }
        }

        private void Test_button(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (entry.Days == null)
                {
                    throw new
                        DaysMissingException("Zero days are selected. Please select a day.");
                }
                if (entry.Interval == null)
                {
                    throw new IntervalMissingException("Missing an interval. Please select an interval.");
                }
                if (entry.Apps == null)
                {
                    throw new MissingApplicationException("Please select an application.");
                }

                Processor.handleMethod(entry);
            }
            catch (DaysMissingException ex)
            {
                ErrorHandler.handleException(ex);
            }
            catch (IntervalMissingException ex)
            {
                ErrorHandler.handleException(ex);
            }
            catch (MissingApplicationException ex)
            {
                ErrorHandler.handleException(ex);
            }
        }

    }
}
