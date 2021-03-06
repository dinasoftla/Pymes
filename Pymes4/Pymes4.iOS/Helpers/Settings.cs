// Helpers/Settings.cs This file was automatically added when you installed the Settings Plugin. If you are not using a PCL then comment this file back in to use it.
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Pymes4.iOS.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
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
}
}
