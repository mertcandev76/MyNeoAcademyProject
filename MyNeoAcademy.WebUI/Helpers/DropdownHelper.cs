using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Helpers
{
    public static class DropdownHelper
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task<List<SelectListItem>> GetDropdownItemsAsync<T>(
      HttpClient client,
      string requestUri,
      Func<T, string> textSelector,
      Func<T, string> valueSelector)
        {
            var response = await client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                return new List<SelectListItem>();

            var jsonData = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<T>>(jsonData, _jsonOptions) ?? new List<T>();

            return items.Select(item => new SelectListItem
            {
                Text = textSelector(item),
                Value = valueSelector(item)
            }).ToList();
        }
    }
}
