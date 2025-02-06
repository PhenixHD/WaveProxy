// RegexProvider.cs - Stores compiled regex patterns for proxy validation.

using System.Text.RegularExpressions;

namespace WaveProxy.src.RegexUtils {

    public enum RegexType {
        Proxy_Port,
        IPv4,
        URL
    }

    internal class RegexProvider {
        public Regex CompiledRegex { get; private set; }

        public RegexProvider(RegexType regexType) {
            CompiledRegex = RegexBuilder(regexType);
        }

        private Regex RegexBuilder(RegexType regexType) {
            Regex compiledRegex = regexType switch {
                RegexType.Proxy_Port => new Regex(@"\b(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\.(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\.(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\.(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2}):\d{1,5}\b", RegexOptions.Compiled),
                RegexType.IPv4 => new Regex(@"\b(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\.(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\.(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\.(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\b", RegexOptions.Compiled),
                RegexType.URL => new Regex(@"https?:\/\/[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}([\/\w.-]*)*(\?[^\s]*)?", RegexOptions.Compiled),
                _ => throw new ArgumentException("Invalid RegexType")
            };

            return compiledRegex;
        }
    }
}