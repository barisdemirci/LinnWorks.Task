﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LinnWorks.Task.Core.Network
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly Encoding Encoding = Encoding.UTF8;
        private const string ApplicationJson = "application/json";
        private readonly IConfiguration configuration;

        public string BaseUrl { get; set; }

        public HttpClientWrapper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<bool> DeleteAsync(string endpoint)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync<TDto>(string endpoint, TDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<TDto> GetAsync<TDto>(string endpoint)
        {
            endpoint = string.Concat(BaseUrl, endpoint);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(endpoint);
            // Parse the response body.
            var dataObjects = await response.Content.ReadAsStringAsync();
            client.Dispose();
            return JsonConvert.DeserializeObject<TDto>(dataObjects);
        }

        public async Task<TDto> PostAsync<TDto>(string endpoint, TDto dto)
        {
            return await PostAsync<TDto, TDto>(endpoint, dto);
        }

        public async Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(string endpoint, TRequestDto dto)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            endpoint = string.Concat(BaseUrl, endpoint);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);

            string content = JsonConvert.SerializeObject(dto);
            HttpContent httpContent = new StringContent(content, Encoding, ApplicationJson);
            HttpResponseMessage responseMessage = await client.PostAsync(endpoint, httpContent);
            var data = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponseDto>(data);
        }

        public System.Threading.Tasks.Task PostAsync(string endpoint)
        {
            throw new NotImplementedException();
        }

        public async Task<TDto> PutAsync<TDto>(string endpoint, TDto dto)
        {
            return await PutAsync<TDto, TDto>(endpoint, dto);
        }

        public async Task<TResponseDto> PutAsync<TRequest, TResponseDto>(string endpoint, TRequest dto)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            HttpClient client = new HttpClient();
            endpoint = string.Concat(BaseUrl, endpoint);
            client.BaseAddress = new Uri(endpoint);
            string content = JsonConvert.SerializeObject(dto);
            HttpContent httpContent = new StringContent(content, Encoding, ApplicationJson);
            HttpResponseMessage response = await client.PutAsync(endpoint, httpContent);

            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponseDto>(data);
        }

        public async System.Threading.Tasks.Task<string> UploadFileAsync(string endpoint, IFormFile file)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));
            if (file == null) throw new ArgumentNullException(nameof(endpoint));

            HttpClient client = new HttpClient();
            endpoint = string.Concat(BaseUrl, endpoint);
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);

            ByteArrayContent bytes = new ByteArrayContent(data);
            MultipartFormDataContent multiContent = new MultipartFormDataContent();
            multiContent.Add(bytes, "excel", file.FileName);

            var result = await client.PostAsync(endpoint, multiContent);
            return "Service is called";
        }
    }
}