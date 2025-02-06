using System.Text.RegularExpressions;

namespace WaveProxy.src.RegexFactory {
    internal class PatternValidator {
        public static bool ValidatePattern(string patternInput, Regex regex) {
            return regex.IsMatch(patternInput);
        }
    }
}