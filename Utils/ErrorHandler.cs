﻿using System;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Scheduler.exceptions;

namespace Scheduler.Utils
{
    public enum ErrorCode
    {
        UnknownError = 1000,
        IncorrectNumberRangeError = 1001,
        MissingUnitError = 1002,
        FormatError = 1003,
        MissingDaysError = 1004,
        IntervalMissingError = 1005,
        MissingApplicationError = 1006
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

        public static void displayErrorMessage(Exception ex) 
        {
            string errorMessage;

            if (ex is IncorrectNumberRangeException exception)
            {
                errorMessage = ex.Message + 
                    $" Error Code: {ErrorCode.IncorrectNumberRangeError:D}";
            }

            else if (ex is MissingUnitException exceptionTwo)
            {
                errorMessage = ex.Message + 
                    $" Error Code: {ErrorCode.MissingUnitError:D}";
            }

            else if (ex is FormatException exceptionThree)
            {
                errorMessage = ex.Message + 
                    $"Error Code: {ErrorCode.FormatError:D}";
            }

            else if (ex is DaysMissingException exceptionFour)
            {
                errorMessage = ex.Message + 
                    $"Error Code: {ErrorCode.MissingDaysError:D}";
            }

            else if (ex is IntervalMissingException exceptionFive)
            {
                errorMessage = ex.Message + 
                    $"Error Code: {ErrorCode.IntervalMissingError:D}";
            }

            else if (ex is MissingApplicationException exceptionSix)
            {
                errorMessage = ex.Message +
                    $"Error Code: {ErrorCode.MissingApplicationError:D}";
            }

            else
            {
                errorMessage = $"An unexpected error occured. " +
                    $"Error Code: {ErrorCode.UnknownError:D}";
            }

            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }

        public static void handleException(Exception ex)
        {
            LogError(ex);

            // Handle specific types of exceptions
            displayErrorMessage(ex);
        }

    }
}
