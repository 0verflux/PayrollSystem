using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PayrollSystem.UI.Helpers
{
    public static class AlertBox
    {
        public static void ShowError(Exception exception)
        {
            if (exception is null)
                throw new ArgumentNullException(message: "Exception is Null", paramName: nameof(exception));

            var sb = new StringBuilder();

            sb.AppendLine(exception.Message);
            sb.AppendLine();
            sb.AppendLine(exception.InnerException?.Message);

            MessageBox.Show(sb.ToString(), "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowError(Dictionary<string, List<string>> errors)
        {
            if (errors is null)
                throw new ArgumentNullException(message: "errors is null!", paramName: nameof(errors));

            var sb = new StringBuilder();
            foreach (var kvp in errors)
            {
                foreach (var err in kvp.Value)
                {
                    sb.AppendLine(err);
                }
            }

            MessageBox.Show(sb.ToString(), "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowWarning(string message)
        {
            return MessageBox.Show(message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }
    }
}
