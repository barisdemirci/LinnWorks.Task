﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LinnWorks.Task.Core.Network
{
    public interface IHttpClientWrapper
    {
        string BaseUrl { get; set; }

        Task<TDto> PostAsync<TDto>(string endpoint, TDto dto);

        Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(string endpoint, TRequestDto dto);

        Task<TDto> GetAsync<TDto>(string endpoint);

        Task<TDto> PutAsync<TDto>(string endpoint, TDto dto);

        Task<TResponseDto> PutAsync<TRequest, TResponseDto>(string endpoint, TRequest dto);

        Task<bool> DeleteAsync(string endpoint);

        Task<bool> DeleteAsync<TDto>(string endpoint, TDto dto);

        System.Threading.Tasks.Task<string> UploadFileAsync(string endpoint, IFormFile file);
    }
}