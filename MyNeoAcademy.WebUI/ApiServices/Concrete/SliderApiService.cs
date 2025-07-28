using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers; 
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class SliderApiService : ISliderApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public SliderApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultSliderDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("sliders");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultSliderDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultSliderDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"sliders/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultSliderDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateSliderWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("sliders", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateSliderWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.SliderID.ToString()), "SliderID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("sliders", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"sliders/{id}");
            return response.IsSuccessStatusCode;
        }

        private MultipartFormDataContent GetFormData(CreateSliderDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.SubTitle ?? ""), "SubTitle" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.ButtonText ?? ""), "ButtonText" },
                { new StringContent(dto.ButtonUrl ?? ""), "ButtonUrl" },
                { new StringContent(dto.ImageUrl ?? ""), "ImageUrl" }
            };
        }

        private StreamContent GetStreamContent(IFormFile file)
        {
            var content = new StreamContent(file.OpenReadStream());
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            return content;
        }
    }
}