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
using System.IO.Pipes;

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
            notifyIcon.ContextMenuStrip.Items.Add("Start Scheduler", null, StartScheduler_Click);
            notifyIcon.ContextMenuStrip.Items.Add("Stop Scheduler", null, ShutdownScheduler_Click);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, Exit_Click);
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

        private async void StartScheduler_Click(object sender, EventArgs e)
        {
            try
            {
                //string exePath = @"C:\Users\dyann\Documents\Development\Scheduler\SchedulerProcesser.exe";
                string currDirectory = AppDomain.CurrentDomain.BaseDirectory;
                Debug.WriteLine(currDirectory);
                string exePath = System.IO.Path.Combine(currDirectory, "SchedulerProcessor.exe");
                string arguments = "true"; // Pass true to start the application

                if (File.Exists(exePath))
                {
                    if (System.IO.Path.GetExtension(exePath).ToLower() == ".exe")
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = exePath,
                            Arguments = arguments,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };

                        using (Process process = new Process { StartInfo = startInfo })
                        {
                            process.Start();

                            string output = await process.StandardOutput.ReadToEndAsync();
                            string error = await process.StandardError.ReadToEndAsync();

                            await process.WaitForExitAsync();

                            MessageBox.Show("Scheduler shutdown.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"The file at '{exePath}' is not an executable.");
                    }
                }
                else
                {
                    Debug.WriteLine($"Cannot find: {exePath}");
                    MessageBox.Show($"Executable file '{exePath}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error starting scheduler process: {ex.Message}");
                });
            }
        }

        private void OpenEditWindow_Click(object sender, EventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Show();
        }
        private async void ShutdownScheduler_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new NamedPipeClientStream("SchedulerProcessorPipe"))
                {
                    await client.ConnectAsync(5000); // Wait for up to 5 seconds to connect

                    using (var writer = new StreamWriter(client))
                    {
                        await writer.WriteLineAsync("shutdown");
                        await writer.FlushAsync();
                    }
                }

                //MessageBox.Show("Shutdown signal sent to SchedulerProcessor.");
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error sending shutdown signal: {ex.Message}");
                });
            }
        }



        private void Exit_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            Application.Current.Shutdown();
        }

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

        private void Run_button(Object sender, RoutedEventArgs e)
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
    }
}
