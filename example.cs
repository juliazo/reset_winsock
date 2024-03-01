using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using WSAinterop;

public class RestApiClientFactory
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static HttpClient CreateHttpClient(string baseUrl)
        {
            ServicePointManager.DefaultConnectionLimit = 100000;

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            // Configuraciones adicionales si es necesario
            httpClient.DefaultRequestHeaders.ConnectionClose = true;
            return httpClient;
        }

    }

    public class RestApiClient : IDisposable
    {
        public readonly HttpClient httpClient;
        private CancellationTokenSource cancellationTokenSource;

        public RestApiClient(string baseUrl="http://172.17.77.1/")
        {

            ServicePointManager.DefaultConnectionLimit = 100000;
            WSAinterop.SocketInitialize(); //Reinitiate Socket before each use---------------------------------------------------------------
            httpClient = RestApiClientFactory.CreateHttpClient(baseUrl);
            cancellationTokenSource = new CancellationTokenSource();
        }
        public void Dispose()
        {
            //
            httpClient.Dispose(); //one of the solutions i tried before reinitiating sockets was to dispose the httpclient after each use, it dint work for me
        }
        public async Task<string> CallApi(string get_request)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), get_request);
            try
            {
                // Sending the request and getting the response
                HttpResponseMessage response = await httpClient.SendAsync(request);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Read the response content and return it as a string
                Mouse.OverrideCursor = null;
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // Handle any exceptions related to the HTTP request
                // Console.WriteLine($"Error: {ex.Message}");
                // Mouse.OverrideCursor = null;
                Mouse.OverrideCursor = null;
                return null;
            }
        }
    }
