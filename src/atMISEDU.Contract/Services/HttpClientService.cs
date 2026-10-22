using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace atMISEDU.Contract.Services
{
    public class HttpClientService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        public HttpClientService()
        {
            _httpClient = new HttpClient();
        }

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> SendHttpRequestAsync(HttpMethod method,
                                                       string url,
                                                       Dictionary<string, string> headers = null,
                                                       object content = null,
                                                       Dictionary<string, string> queryParams = null,
                                                       CancellationToken cancellationToken = default)
        {
            //try
            //{
            // Kiểm tra URL hợp lệ
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));

            // Khởi tạo HttpRequestMessage
            var request = new HttpRequestMessage(method, url);

            // Thêm headers nếu có
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // Thêm query parameters nếu có
            if (queryParams != null && queryParams.Count > 0)
            {
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var param in queryParams)
                {
                    query[param.Key] = param.Value;
                }
                uriBuilder.Query = query.ToString();
                request.RequestUri = uriBuilder.Uri;
            }

            // Thêm content dạng JSON nếu có
            if (content != null)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsonBody = JsonConvert.SerializeObject(content, settings);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            // Gửi request
            HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            // Đọc và trả về nội dung phản hồi
            string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return responseContent;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _httpClient?.Dispose();
            _disposed = true;
        }
    }
}