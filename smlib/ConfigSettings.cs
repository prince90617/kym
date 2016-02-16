using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smlib {
  public static class ConfigSettings {
    public static string GetEnvironmentSetting(string settingName) {
      var variableName = ConfigurationManager.AppSettings[settingName];
      if (!string.IsNullOrWhiteSpace(variableName)) {
        return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Machine);
      }
      return null;
    }

    public static string GetEnvironmentSetting(string settingName, string defaultValue) {
      var variableValue = GetEnvironmentSetting(settingName);
      return string.IsNullOrWhiteSpace(variableValue) ? defaultValue : variableValue;
    }
  }
}
