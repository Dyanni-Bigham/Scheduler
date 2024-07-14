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
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public ObservableCollection<Utils.Entry> Schedules { get; set; }
        public EditWindow()
        {

            InitializeComponent();
            Schedules = new ObservableCollection<Utils.Entry>();

            string currDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFilePath = System.IO.Path.Combine(currDirectory, "config.json");

            try
            {
                if (!File.Exists(configFilePath))
                {
                    MessageBox.Show("File does not exist. Please create a schedule entry.");
                    return;
                }

                string jsonData = File.ReadAllText(configFilePath);
                var schedulesFromJson = JsonConvert.DeserializeObject<List<Utils.Entry>>(jsonData);

                foreach (var schedule in schedulesFromJson)
                {
                    Schedules.Add(schedule);
                }

                dataGrid.ItemsSource = Schedules;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the configuration: {ex.Message}");
            }
        }

        private void SaveChanges()
        {
            string json = JsonConvert.SerializeObject(Schedules, Formatting.Indented);
            string currDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFilePath = System.IO.Path.Combine(currDirectory, "config.json");
            File.WriteAllText(configFilePath, json);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
            MessageBox.Show("Changes saved successfully.");
        }

        private void DeleteButton_Click(object obj, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Schedules.Remove((Utils.Entry)dataGrid.SelectedItem);
            }
        }
    }
}
