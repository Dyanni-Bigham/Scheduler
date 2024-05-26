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
using Microsoft.Win32;
using System.Diagnostics;
using Scheduler.Utils;
using Scheduler.exceptions;
using System.Runtime.InteropServices;
using System.IO;

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

            /////////////////// Tray Icon setup ///////////////////
            notifyIcon = new Forms.NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("./icon.ico");
            notifyIcon.Text = "Scheduler";
            notifyIcon.MouseUp += NotifyIcon_MouseUp;

            // Context Menu Strip setup
            notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();

            // Add "Run Scheduler" menu item
            Forms.ToolStripMenuItem runSchedulerMenuItem =
                new Forms.ToolStripMenuItem("Run Scheduler");
            runSchedulerMenuItem.Click += RunScheduler_Click;
            notifyIcon.ContextMenuStrip.Items.Add(runSchedulerMenuItem);

            // Add "Stop Scheduler" menu item
            Forms.ToolStripMenuItem stopSchedulerMenuItem =
                new Forms.ToolStripMenuItem("Stop Scheduler");
            stopSchedulerMenuItem.Click += StopScheduler_Click;
            notifyIcon.ContextMenuStrip.Items.Add(stopSchedulerMenuItem);

            notifyIcon.Visible = true;

            //////////////////////////////////////////////////////
        }

        private void NotifyIcon_MouseUp(object sender, Forms.MouseEventArgs e)
        {
            if (e.Button == Forms.MouseButtons.Right)
            {
                // Show the context menu when right-clicked
                notifyIcon.ContextMenuStrip.Show();
            }
            else if (e.Button == Forms.MouseButtons.Left)
            {
                // Restore or minimize the window when left-clicked
                if (this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal; // Restore window
                    this.Show();
                    this.Activate();
                    this.Activate();
                }
                else
                {
                    this.WindowState = WindowState.Minimized;
                }
            }
        }
        /*
        private void Summon_CustomInterval(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Is this working?");
            CustomInterval customIntervvalWindow = new CustomInterval();
            customIntervvalWindow.Show();

        }
        */

        private void RunScheduler_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Scheduler is now running");
        }

        private void StopScheduler_Click(Object sender, EventArgs e)
        {
            MessageBox.Show("Scheduler has stopped.");
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
        /*
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
        */

        private void Save_button(Object sender, RoutedEventArgs e)
        {

            //MessageBox.Show("Days, intervals, and apps are being sent to the backend");

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

                Debug.WriteLine(String.Join(" ", entry.Apps)); //TODO: Bug that when the user opens up file explorer and doesn't select this is treated as a success when it is not
                Debug.WriteLine(entry.Interval);
                Debug.WriteLine(String.Join(", ", entry.Days));
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

        private void cb1_Checked(object sender, RoutedEventArgs e)
        {
            // Select all days
            foreach (ListBoxItem item in daysListBox.Items)
            {

                item.IsSelected = true;
                entry.Days.Add(item.Content.ToString());
            }
        }

        private void cb1_Unchecked(object sender, RoutedEventArgs e)
        {
            // Deselect all days
            foreach (ListBoxItem item in daysListBox.Items)
            {
                item.IsSelected = false;
                entry.Days.Clear();
            }
        }

        private void App_Select(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Shortcut files (*.lnk)|*.lnk";

            entry.Apps ??= new List<string>();
            entry.Apps.Clear();

            if (openFileDialog.ShowDialog() == true)
            {
                string shortcutPath = openFileDialog.FileName;
                string targetPath = ResolveShortcut(shortcutPath);
                if (!string.IsNullOrEmpty(targetPath))
                {
                    //MessageBox.Show("Shortcut target: " + targetPath);
                    entry.Apps.Add(targetPath);
                    ;
                }
                else
                {
                    MessageBox.Show("Failed to resolve shortcut.");
                }
            }
        }

        private string ResolveShortcut(string shortcutPath)
        {
            try
            {
                var shell = new IWshRuntimeLibrary.WshShell();
                var shortcut = shell.CreateShortcut(shortcutPath) as IWshRuntimeLibrary.IWshShortcut;
                return shortcut?.TargetPath;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
