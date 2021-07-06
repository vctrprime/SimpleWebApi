using System;

namespace SimpleWebApi.UnitTests
{
    internal class BasicCredentials
    {
        internal static string ValidUsername => "valid";

        internal static string ValidPassword => "valid";
        
        
        internal static string BasicValidAuthorizationHeaderText => GetHeaderValue();

        internal static string BasicInvalidAuthorizationHeaderText => GetHeaderValue("invalid");

        private static string GetHeaderValue(string password = null)
        {
            byte[] toEncodeAsBytes
                = System.Text.Encoding.ASCII.GetBytes($"{ValidUsername}:{password ?? ValidPassword}");
            string base64
                = Convert.ToBase64String(toEncodeAsBytes);
            return $"Basic {base64}";
        }
    }
}