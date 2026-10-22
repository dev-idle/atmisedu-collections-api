namespace atMISEDU.Contract.Helpers
{
    public static class BrowserInfoHelper
    {
        public static string GetBrowserInfo(string userAgent)
        {
            string browser = DetectBrowser(userAgent);
            string os = DetectOS(userAgent);

            return $"{browser} on {os}";
        }

        private static string DetectBrowser(string ua)
        {
            // Edge & Opera fake Chrome
            // Chrome fake Safari

            if (ua.Contains("Edg")) return "Edge";
            if (ua.Contains("OPR") || ua.Contains("Opera")) return "Opera";
            if (ua.Contains("Chrome")) return "Chrome";
            if (ua.Contains("Safari")) return "Safari";
            if (ua.Contains("Firefox")) return "Firefox";
            if (ua.Contains("MSIE") || ua.Contains("Trident")) return "Internet Explorer";

            return "Unknown Browser";
        }

        private static string DetectOS(string ua)
        {
            // iOS UA has "like Mac OS X" 
            // Android UA has "Linux"

            if (ua.Contains("iPhone") || ua.Contains("iPad")) return "iOS";
            if (ua.Contains("Android")) return "Android";
            if (ua.Contains("Windows")) return "Windows";
            if (ua.Contains("Mac OS")) return "macOS";
            if (ua.Contains("Linux")) return "Linux";

            return "Unknown OS";
        }
    }
}