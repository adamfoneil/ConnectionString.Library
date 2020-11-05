using System;
using System.Collections.Generic;
using System.Linq;

namespace AO.ConnectionStrings
{
    public static class ConnectionString
    {
        public const string DefaultRedaction = "&lt;redacted&gt;";

        public static string Token(string input, string token, string defaultValue = default)
        {
            var dictionary = ToDictionary(input);

            return dictionary.ContainsKey(token) ? dictionary[token] : defaultValue;
        }

        public static string Token(string input, IEnumerable<string> tokens, string defaultValue = default)
        {
            var dictionary = ToDictionary(input);

            var key = tokens.FirstOrDefault(item => dictionary.ContainsKey(item));

            return (key != default) ? dictionary[key] : defaultValue;
        }

        public static string Server(string input) => Token(input, new string[] { "Server", "Data Source" });

        public static string Database(string input) => Token(input, new string[] { "Initial Catalog", "Database" });

        public static bool IsWellFormed(string input)
        {
            try
            {
                var dictionary = ToDictionary(input);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static Dictionary<string, string> ToDictionary(string input) =>
            input
                .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item =>
                {
                    var parts = item.Split('=');
                    return new
                    {
                        Name = parts[0],
                        Value = parts[1]
                    };
                }).ToDictionary(item => item.Name, item => item.Value);

        public static string Redact(string input, string replaceWith = DefaultRedaction)
        {
            return (IsSensitive(input, out Dictionary<string, string> dictionary, replaceWith)) ?
                string.Join(";", dictionary.Select(kp => $"{kp.Key}={kp.Value}")) :
                input;
        }

        public static bool IsSensitive(string input, out Dictionary<string, string> redacted, string replaceWith = DefaultRedaction)
        {
            try
            {
                var sensitiveTokens = new HashSet<string>(new string[]
                {
                    "password",
                    "user id"
                });
                
                bool result = false;
                var original = ToDictionary(input);
                redacted = ToDictionary(input);

                foreach (var kp in original)
                {
                    if (sensitiveTokens.Contains(kp.Key.ToLower()))
                    {
                        result = true;
                        redacted[kp.Key] = replaceWith;
                    }
                }

                return result;
            }
            catch
            {
                redacted = null;
                return false;
            }
        }
    }
}
