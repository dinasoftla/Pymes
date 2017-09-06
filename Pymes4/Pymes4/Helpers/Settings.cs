// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Pymes4.Helpers
{
    /// <summary>
    /// Agregar nudged en todos los proyectos, importante: limpiar y compilar todo
    /// NOTA: >>>> SETTINGS USADOS EN LA APLICACION (SE GUARDAN LOCALMENTE)
    /// 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsName = "Name";
        private static readonly string SettingsDefaultName = string.Empty;
        private const string SettingsEmail = "Email";
        private static readonly string SettingsDefaultEmail = string.Empty;
        private const string SettingsPhone = "Phone";
        private static readonly string SettingsDefaultPhone = string.Empty;
        private const string SettingsActiveUser = "ActiveUser";
        private static readonly string SettingsDefaultActiveUser = string.Empty;
        private const string SettingsAppointment = "Appointment";
        private static readonly string SettingsDefaultAppointment = string.Empty;
        private const string SettingsAppointmentStatus = "AppointmentStatus";
        private static readonly string SettingsDefaultAppointmentStatus = string.Empty;
        private const string SettingsOffert = "Offert";
        private static readonly string SettingsDefaultOffert = string.Empty;
        private const string SettingsApiAddress = "ApiAddress";
        private static readonly string SettingsDefaultApiAddress = string.Empty;
        #endregion



        public static string Name
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsName, SettingsDefaultName);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsName, value);
            }

        }
        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsEmail, SettingsDefaultEmail);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsEmail, value);
            }

        }
        public static string Phone
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsPhone, SettingsDefaultPhone);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsPhone, value);
            }

        }
        public static string ActiveUser
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsActiveUser, SettingsDefaultActiveUser);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsActiveUser, value);
            }

        }
        public static string Appointment
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsAppointment, SettingsDefaultAppointment);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsAppointment, value);
            }

        }
        public static string AppointmentStatus
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsAppointmentStatus, SettingsDefaultAppointmentStatus);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsAppointmentStatus, value);
            }

        }
        public static string Offert
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsOffert, SettingsDefaultOffert);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsOffert, value);
            }

        }
        public static string ApiAddress
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsApiAddress, SettingsDefaultApiAddress);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsApiAddress, value);
            }

        }

    }
}
