using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode2020.Services
{
    public static class Http 
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? headers = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            if (headers != null) req._addRequestHeaders(headers);
            var response = await _client.SendAsync(req);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<HttpResponseMessage> PostAsync(string url, StringContent body, Dictionary<string, string>? headers = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Content = body;
            if (headers != null) req._addRequestHeaders(headers);
            var response = await _client.SendAsync(req);
            response.EnsureSuccessStatusCode();
            return response;
        }

        // overload for MultipartFormDataContent
        public static async Task<HttpResponseMessage> PostAsync(string url, MultipartFormDataContent form, Dictionary<string, string>? headers = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Content = form;
            if (headers != null) req._addRequestHeaders(headers);
            var response = await _client.SendAsync(req);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<HttpResponseMessage> PutAsync(string url, StringContent body, Dictionary<string, string>? headers = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, url);
            req.Content = body;
            if (headers != null) req._addRequestHeaders(headers);
            var response = await _client.SendAsync(req);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static void AddDefaultBaseAddress(string uri)
        {
            _client.BaseAddress = new Uri(uri);
        }

        public static void AddDefaultAuthorizationHeader(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        private static void _addAuthHeader(this HttpRequestMessage req, string token)
        {
            var h = new Dictionary<string, string>();
            h.Add("Authorization", token);
            req._addRequestHeaders(h);
        }

        private static HttpRequestMessage _addRequestHeaders(this HttpRequestMessage req, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                req.Headers.Add(header.Key, header.Value);
            }
            return req;
        }
    }
}
