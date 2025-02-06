// PatternValidator.cs - Uses regex to validate pattern

using System.Text.RegularExpressions;

namespace WaveProxy.src.Validation {
    internal class PatternValidator {
        public static bool ValidatePattern(string patternInput, Regex regex) {
            return regex.IsMatch(patternInput);
        }
    }
}