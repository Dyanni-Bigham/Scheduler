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

            // Tray Icon setup //
            notifyIcon = new Forms.NotifyIcon();
            string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.ico");
            notifyIcon.Icon = new System.Drawing.Icon(iconPath);
            notifyIcon.Text = "Scheduler";
            notifyIcon.MouseClick += NotifyIcon_MouseClick;

            // menu actions
            notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Run Scheduler");
            notifyIcon.ContextMenuStrip.Items.Add("Stop Scheduler");
            //////////////////////////////////////////////////////

            notifyIcon.Visible = true;

            // Interval set up //
            // Add hours from 00:00 to 23:45 //
            for (int hour = 0; hour < 24; hour++)
            {
                for (int minute = 0; minute < 60; minute += 15)
                {
                    string hourStr = hour.ToString().PadLeft(2, '0');
                    string minuteStr = minute.ToString().PadLeft(2, '0');
                    intervalsListBox.Items.Add($"{hourStr}:{minuteStr}");
                }
            }
            ///////////////////////////////////
        }
        /*
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
        */

        private void NotifyIcon_MouseClick(object sender, Forms.MouseEventArgs e)
        {
            if (e.Button == Forms.MouseButtons.Left)
            {
                if (this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal; // Restores window
                    this.Show();
                    this.Activate(); // Activate the window
                }
                else
                {
                    this.WindowState = WindowState.Minimized; // Minimize the window
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

        /*
        private void IntervalsListBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            var selectedInterval = intervalsListBox.SelectedItem;

            if (selectedInterval is ListBoxItem listBoxItem)
            {
                entry.Interval = listBoxItem.Content.ToString();
            }
            else
            {
                Debug.WriteLine("This is a null value even though an interval is selected");
            }

        }
        */

        private void IntervalsListBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            var selectedInterval = intervalsListBox.SelectedItem as string;

            if (selectedInterval != null)
            {
                entry.Interval = selectedInterval;
            }
            else
            {
                Debug.WriteLine("No interval selected.");
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

        private void Test_button(Object sender, RoutedEventArgs e)
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
                
                //Debug.WriteLine(entry.Apps);
                //Debug.WriteLine(entry.Interval);
                //Debug.WriteLine(entry.Days);
                Processor.HandleMethod(entry);
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

                    //ExecuteFile(targetPath);
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

        private void ExecuteFile(string filePath)
        {
            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing file: " + ex.Message);
            }
        }
    }
}
