﻿using System;
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

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> selectedDays = new List<string>();
        private List<string> selectedIntervals = new List<string>();

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
                string day = selectedDay.ToString();
                selectedDays.Add(day);
            }

        }

        private void IntervalsListBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            // clear existing items in array
            selectedIntervals.Clear();

            // get all of the selected intervals
            foreach (var selectedInterval in intervalsListBox.SelectedItems) 
            {
                string interval = selectedInterval.ToString();
                selectedIntervals.Add(interval);
            }
        }
        
        private void Test_button(Object sender, RoutedEventArgs e)
        {   
            // Printing to console to see if the days show
            foreach (var day in selectedDays)
            {
                Debug.WriteLine(day);
            }

            Debug.WriteLine("============================");

            // printing to console to see if the intervals show
            foreach (var interval in selectedIntervals)
            {
                Debug.WriteLine(interval);
            }
        }

    }
}
