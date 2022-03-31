using KonnecticTestCoreUtility.Models.Common;
using Microsoft.AspNetCore.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KonnecticTestCoreUtility.Handlers
{
    public class BaseHttpHandler
    {
        //private static readonly HttpClient _httpClient = new HttpClient();

        private string baseAddress;

        public BaseHttpHandler(string baseAddressOverride = null)
        {
            baseAddress = string.IsNullOrWhiteSpace(baseAddressOverride) ? KonnecticTestAppSettings.KonnecticTestBaseUrl : baseAddressOverride;
        }

        /// <summary>
        /// Make a GET request that returns a result object where T is the expected value return.
        /// </summary>
        /// <typeparam name="T">The expected type returned</typeparam>
        /// <param name="endpoint"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<ReturnResult<T>> GetAsync<T>(string endpoint, string parameters = null)
        {
            var _httpClient = new HttpClient();
            //_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            try
            {
                //var getMessage = new HttpRequestMessage(HttpMethod.Get);
                using (var response = await _httpClient.GetAsync($"{baseAddress}{endpoint}{parameters ?? ""}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent httpContent = response.Content)
                        {
                            var content = await httpContent.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<ReturnResult<T>>(content);

                            return result;
                        }
                    }
                    else
                    {
                        return new ReturnResult<T>($"HTTP CODE: {response.StatusCode} | {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Build Error object that tells us what endpoint was called, the parameters used, and the exception message
                return new ReturnResult<T>(ex.ToString());
            }
               
        }

        /// <summary>
        /// Makes a POST Request to the extended URL and returns the expected K
        /// </summary>
        /// <typeparam name="T">The type of model parameter</typeparam>
        /// <typeparam name="K">The expected return type from the request</typeparam>
        /// <param name="endpoint"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<ReturnResult<K>> PostAsync<T, K>(string endpoint, T model) 
            //where T : class 
            //where K : class
        {
            var _httpClient = new HttpClient();
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var str = await jsonContent.ReadAsStringAsync();
                using (var response = await _httpClient.PostAsync($"{baseAddress}{endpoint}", jsonContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent httpContent = response.Content)
                        {
                            var content = await httpContent.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<ReturnResult<K>>(content);

                            return result;
                        }
                    }
                    else
                    {
                        return new ReturnResult<K>($"HTTP CODE: {response.StatusCode} | {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Build Error object that tells us what endpoint was called, the parameters used, and the exception message
                return new ReturnResult<K>(ex.ToString());
            }
        }
    }
}
