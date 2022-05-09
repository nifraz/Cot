using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Utilities
{
    public static class BsAlert
    {
        public static string Show(string title, string message, AlertType type)
        {
            string alertTypeClass = type switch
            {
                AlertType.Primary => "alert-primary",
                AlertType.Secondary => "alert-secondary",
                AlertType.Success => "alert-success",
                AlertType.Danger => "alert-danger",
                AlertType.Warning => "alert-warning",
                AlertType.Info => "alert-info",
                AlertType.Light => "alert-light",
                AlertType.Dark => "alert-dark",
                _ => string.Empty,
            };

            return $"<div class='m-0 alert {alertTypeClass} alert-dismissible fade show' role='alert'><strong>{title}</strong> {message}<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
        }
    }

    public enum AlertType : byte
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Danger = 4,
        Warning = 5,
        Info = 6,
        Light = 7,
        Dark = 8,
    }
}
