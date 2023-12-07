using System;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scheduler.Utils
{
    public enum ErrorCode
    {
        UnknownError = 1000,
        IncorrectNumberRangeError = 1001,
        MissingUnitError = 1002,
        MissingTimeError = 1003,
        // Add more error codes as needed
    }
    /**
     * This should have some form of logging to a log file for errors
     * Should use error codes and produce on the screen for the user
     */
    public class ErrorHandler
    {
        public static string errorMessage;

        static ErrorHandler()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("test.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void LogError(Exception ex)
        {
            // Log the error details
            Log.Error(ex, "An error occured {ErrorMessage}", ex.Message);
        }

        public static void handleException(Exception ex)
        {
            LogError(ex);

            // Handle specific types of exceptions
            if (ex is IncorrectNumberRangeException exception)
            {
                errorMessage = $"An error occured. Error Code: {ErrorCode.IncorrectNumberRangeError:D}" +
                   $"\n\n Number inputted is not witin range 0 - 59 minutes";
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            if (ex is MissingUnitException exceptionTwo)
            {
                errorMessage = $"An error occured. Error Code: {ErrorCode.MissingUnitError:D}" +
                    $"\n\n Unit of time is missing. Please select a unit.";
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            if (ex is Exception exceptionThree)
            {
                errorMessage = $"An error occured. Error Code: {ErrorCode.UnknownError:D}";
            }

        }

    }
}
