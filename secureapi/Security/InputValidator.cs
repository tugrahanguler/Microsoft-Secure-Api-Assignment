using System;
using System.Text.RegularExpressions;

namespace SafeVault.Security
{
    public static class InputValidator
    {
        private static readonly Regex UsernameRegex =
            new(@"^[a-zA-Z0-9._-]{3,30}$", RegexOptions.Compiled);

        private static readonly Regex EmailRegex =
            new(@"^[^@\s]{1,64}@[^@\s]{1,255}$", RegexOptions.Compiled);

        public static string ValidateUsername(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");

            username = username.Trim();

            if (!UsernameRegex.IsMatch(username))
                throw new ArgumentException("Username contains invalid characters or length.");

            return username;
        }

        public static string ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            email = email.Trim();

            if (!EmailRegex.IsMatch(email))
                throw new ArgumentException("Email format is invalid.");

            return email;
        }
    }
}
