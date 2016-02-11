using System;
using System.Text.RegularExpressions;

namespace smlib
{
  public static class PasswordHelper {
    public static string HashPassword(string password) {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static string HashToken(string token) {
      return BCrypt.Net.BCrypt.HashPassword(token, "$2a$10$xj/cv8nuTHbA5UEdx8rGpu");
    }

    public static bool VerifyPassword(string password, string hash) {
      try {
        return BCrypt.Net.BCrypt.Verify(password, hash);
      } catch (Exception) {
        return false;
      }
    }

    public static bool IsValidPassword(string password) {
      if (password.Length < 8) {
        return false;
      }
      if (!new Regex(@"\W").IsMatch(password)) {
        return false;
      }
      return true;
    }
  }
}