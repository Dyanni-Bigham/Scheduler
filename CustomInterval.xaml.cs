using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Scheduler.Utils;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for CustomInterval.xaml
    /// </summary>
    public partial class CustomInterval : Window
    {
        private Entry entry = new Entry();
        public CustomInterval()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxItem selectedComboBoxItem = myComboBox.SelectedItem as ComboBoxItem;

                if (selectedComboBoxItem != null)
                {
                    entry.TimeUnit = selectedComboBoxItem.Content.ToString();
                }
                else
                {
                    throw new MissingUnitException
                        ("Unit of time is missing. Please select a unit.");
                }

                entry.Interval = timeInput.Text;

                if (entry.TimeUnit.Equals("Minute"))
                {
                    int interval = Int32.Parse(entry.Interval);

                    if (interval < 1 || interval > 59)
                    {
                        throw new IncorrectNumberRangeException
                         ("Number inputted is not witin range 0 - 59 minutes");
                    }
                }
                else if (entry.TimeUnit.Equals("Hour"))
                {
                    int interval = Int32.Parse(entry.Interval);

                    if (interval < 1 || interval > 15)
                    {
                        throw new IncorrectNumberRangeException
                            ("Number inputted is within range 1 - 15 hours.");
                    }
                }

                entry.testEntry();
            }
            catch (MissingUnitException ex)
            {
                ErrorHandler.handleException(ex);
            }
            catch (IncorrectNumberRangeException ex)
            {
                ErrorHandler.handleException(ex);
            }
            catch (Exception ex)
            {
                ErrorHandler.handleException(ex);
            }
        }
    }
}
