using System;
using System.ComponentModel;
using System.Configuration;
using PerforcePuller.Constants;

namespace PerforcePuller.Helpers
{
    public static class AppSettingsHelper
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings.Get(key);
            if(string.IsNullOrWhiteSpace(appSetting))
            {
                throw new SettingsPropertyNotFoundException(string.Format(ErrorStrings.UnableToLocateAppKey,key));
            }
            var converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                var convertedObject = (T)converter.ConvertFromInvariantString(appSetting);
                return convertedObject;
            }
            catch
            {
                throw new InvalidCastException(key + " " + typeof(T).ToString());
            }
        }
    }
}
