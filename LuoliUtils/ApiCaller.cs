using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuoliUtils
{
    public static class ApiCaller
    {
        // 重用 HttpClient 和 HttpClientHandler 实例
        private static readonly HttpClientHandler _httpClientHandler = CreateHandler();
        private static readonly HttpClient _httpClient = CreateHttpClient(_httpClientHandler);


        private static HttpClientHandler CreateHandler()
        {
            return new HttpClientHandler
            {
                MaxConnectionsPerServer = 100,
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
        }

        private static HttpClient CreateHttpClient(HttpClientHandler handler)
        {
            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(600) // 设置超时时间
            };

            return client;
        }


        /// <summary>
        /// 通用HTTP请求方法
        /// </summary>
        /// <param name="method">HTTP方法（GET/POST/PUT/DELETE等）</param>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求数据（POST/PUT等方法使用）</param>
        /// <param name="headers">自定义请求头</param>
        /// <returns>HTTP响应结果</returns>
        public static async Task<HttpResponseMessage>  SendRequestAsync(
            HttpMethod method,
            string url,
            string data = null,
            Dictionary<string, dynamic> headers = null,
            bool isFormUrlEncoded = false
            )
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("请求地址不能为空", nameof(url));

            // 创建请求消息
            using (var request = new HttpRequestMessage(method, url))
            {
                // 添加自定义请求头
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        // 尝试添加请求头，跳过受限制的头部
                        if (!request.Headers.TryAddWithoutValidation(header.Key, header.Value) )
                        {
                            // 可以根据需要处理添加失败的情况
                            Console.WriteLine($"无法添加请求头: {header.Key}");
                        }
                    }
                }

                // 添加请求体（适用于POST/PUT等方法）
                if (!string.IsNullOrEmpty(data))
                {
                    if (isFormUrlEncoded)
                    {
                        // 处理form-urlencoded格式
                        // 假设data是"key1=value1&key2=value2"格式的字符串
                        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, string>>(data);

                        request.Content = new FormUrlEncodedContent(keyValuePairs);
                    }
                    else
                    {
                        // 默认使用JSON格式
                        request.Content = new StringContent(
                            data,
                            Encoding.UTF8,
                            "application/json");
                    }

                }

                // 发送请求并返回响应
                return await _httpClient.SendAsync(request);
            }
        }

        // 常用方法的快捷封装

        /// <summary>
        /// GET请求快捷方法
        /// </summary>
        public static async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, dynamic> headers = null)
        {
            return await SendRequestAsync(HttpMethod.Get, url, null , headers);
        }

        /// <summary>
        /// POST请求快捷方法
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsync(string url, string data, Dictionary<string, dynamic> headers = null, bool isFormUrlEncode=false)
        {
            return await SendRequestAsync(HttpMethod.Post, url, data, headers, isFormUrlEncode);
        }

        /// <summary>
        /// PUT请求快捷方法
        /// </summary>
        public static async Task<HttpResponseMessage> PutAsync(string url, string data, Dictionary<string, dynamic> headers = null)
        {
            return await SendRequestAsync(HttpMethod.Put, url, data ,headers);
        }

        /// <summary>
        /// DELETE请求快捷方法
        /// </summary>
        public static async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, dynamic> headers = null)
        {
            return await SendRequestAsync(HttpMethod.Delete, url, null, headers);
        }


        public static async Task<Dictionary<string, HttpResponseMessage>> NotifyAsync(string content, List<string> users =null)
        {
            if(users is null)
                users = new List<string>() { "@all"};


            Dictionary<string, HttpResponseMessage> dic = new(users.Count);
            foreach (var user in users)
            {
                var msg = new
                {
                    msgType = "text",
                    content = content,
                    toUser = user
                };
                var msgStr = System.Text.Json.JsonSerializer.Serialize(msg);
                var result = await PostAsync("https://www.asynspetfood.top/api/notification/notify?sign=luoli&timestamp=123", msgStr);
                dic[user] = result;
            }

            return dic;
            
        }

    }
}
