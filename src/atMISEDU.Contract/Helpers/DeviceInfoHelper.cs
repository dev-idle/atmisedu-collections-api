using System.Text.RegularExpressions;

namespace atMISEDU.Contract.Helpers
{
    public static class DeviceInfoHelper
    {
        public static DeviceInfo? GetDeviceInfoFromUserAgent(this string? userAgentString)
        {
            if (string.IsNullOrEmpty(userAgentString))
                return null;

            var uaLower = userAgentString.ToLower();

            string deviceType = "Desktop";
            if (uaLower.Contains("mobile") || uaLower.Contains("android") || uaLower.Contains("iphone") || uaLower.Contains("ipad"))
                deviceType = "Mobile";
            else if (uaLower.Contains("tablet"))
                deviceType = "Tablet";

            string deviceModel = "Unknown";
            var modelRegex = new Regex(@"(iphone|ipad|ipod|pixel|samsung|huawei|oneplus|xiaomi)[\s/]?([a-z0-9._-]+)?", RegexOptions.IgnoreCase);
            var modelMatch = modelRegex.Match(userAgentString);
            if (modelMatch.Success)
            {
                deviceModel = modelMatch.Groups[1].Value +
                              (modelMatch.Groups[2].Success ? " " + modelMatch.Groups[2].Value : "");
            }

            string osFamily = "Unknown";
            string osVersion = "Unknown";

            var osPatterns = new[]
            {
                new { Pattern = @"windows nt ([\d.]+)", Family = "Windows" },
                new { Pattern = @"mac os x ([\d._]+)", Family = "macOS" },
                new { Pattern = @"android ([\d.]+)", Family = "Android" },
                new { Pattern = @"iphone os ([\d_]+)", Family = "iOS" },
                new { Pattern = @"linux", Family = "Linux" }
            };

            foreach (var pattern in osPatterns)
            {
                var match = Regex.Match(uaLower, pattern.Pattern);
                if (match.Success)
                {
                    osFamily = pattern.Family;
                    osVersion = match.Groups.Count > 1
                        ? match.Groups[1].Value.Replace("_", ".")
                        : "Unknown";
                    break;
                }
            }

            return new DeviceInfo
            {
                DeviceType = deviceType,
                DeviceModel = deviceModel,
                OSFamily = osFamily,
                OSVersion = osVersion,
                IsMobile = deviceType == "Mobile"
            };
        }

        public static string GetBrowserFromUserAgent(this string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            var ua = userAgent.ToLower();

            var browserPatterns = new[]
            {
                new { Name = "Edge", Pattern = @"edg/([\d\.]+)" },
                new { Name = "Chrome", Pattern = @"chrome/([\d\.]+)" },
                new { Name = "Firefox", Pattern = @"firefox/([\d\.]+)" },
                new { Name = "Safari", Pattern = @"version/([\d\.]+).*safari" },
                new { Name = "Opera", Pattern = @"opr/([\d\.]+)" },
                new { Name = "IE", Pattern = @"msie ([\d\.]+)|trident/.*rv:([\d\.]+)" }
            };

            foreach (var browser in browserPatterns)
            {
                var match = Regex.Match(ua, browser.Pattern);
                if (match.Success)
                {
                    var version = match.Groups[1].Success
                        ? match.Groups[1].Value
                        : match.Groups.Count > 2 && match.Groups[2].Success
                            ? match.Groups[2].Value
                            : "Unknown";

                    return $"{browser.Name} {version}";
                }
            }

            return "Unknown";
        }

        public static string GetFlatForm(this string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "x86";

            var ua = userAgent.ToLower();

            if (ua.Contains("x64") || ua.Contains("win64") || ua.Contains("amd64"))
                return "x64";

            if (ua.Contains("x86") || ua.Contains("wow64"))
                return "x86";

            return "x86"; // default
        }
    }

    public class DeviceInfo
    {
        public string DeviceType { get; set; } // e.g., "Mobile", "Desktop"
        public string DeviceModel { get; set; } // e.g., "iPhone 14"
        public bool IsMobile { get; set; }
        public string OSFamily { get; set; } // e.g., "iOS"
        public string OSVersion { get; set; } // e.g., "17.0"
    }
}