using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class TestimonialApiService : ITestimonialApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TestimonialApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultTestimonialDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("testimonials");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultTestimonialDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultTestimonialDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"testimonials/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultTestimonialDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateTestimonialWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("testimonials", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateTestimonialWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.TestimonialID.ToString()), "TestimonialID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("testimonials", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"testimonials/{id}");
            return response.IsSuccessStatusCode;
        }

        private MultipartFormDataContent GetFormData(CreateTestimonialDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.FullName), "FullName" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.ImageUrl ?? ""), "ImageUrl" },
                { new StringContent(dto.Content ?? ""), "Content" },
                { new StringContent(dto.Rating.ToString()), "Rating" }
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
