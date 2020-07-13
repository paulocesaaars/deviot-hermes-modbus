using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deviot.Hermes.Common.BaseClientHttp
{
    public abstract class BaseHttpClient
    {
        protected readonly HttpClient _httpClient;

        public BaseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public BaseHttpClient(HttpClient httpClient, Uri baseUrl)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseUrl;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public bool ValidReponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<ModelView> DeserializeAsync<ModelView>(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<ResponseData<ModelView>>(await response.Content.ReadAsStreamAsync(), options);
            return result.Data;
        }

        public async Task<ResponseError> DeserializeResponseErrorAsync(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return await JsonSerializer.DeserializeAsync<ResponseError>(await response.Content.ReadAsStreamAsync(), options);
        }

        public Exception ConvertException(ResponseError responseError)
        {
            Exception exception = new Exception(responseError.Message);
            foreach (var error in responseError.Errors)
                exception = new Exception(error, exception);

            return exception;
        }
    }
}
