using System.Text.RegularExpressions;

namespace WaveProxy.src.RegexFactory {

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
                RegexType.Proxy_Port => new Regex(@"(\d{1,3}(?:\.\d{1,3}){3}:\d{1,5})", RegexOptions.Compiled),
                RegexType.IPv4 => new Regex(@"\d{1,3}(?:\.\d{1,3}){3}", RegexOptions.Compiled),
                RegexType.URL => new Regex(@"https?:\/\/[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}([\/\w.-]*)*(\?[^\s]*)?", RegexOptions.Compiled),
                _ => throw new ArgumentException("Invalid RegexType")
            };

            return compiledRegex;
        }
    }
}