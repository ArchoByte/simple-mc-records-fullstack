using SimpleMcRecords.Dto;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Helper;

public class CategoryMapper : IMapper<Category, CategoryDto>
{
    public async Task<List<CategoryDto>> MapList(ICollection<Category> list)
    {
        var newList = new List<CategoryDto>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<CategoryDto> Map(Category model)
    {
        return Task.FromResult(new CategoryDto() { Name = model.Name });
    }
    public async Task<List<Category>> MapList(ICollection<CategoryDto> list)
    {
        var newList = new List<Category>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<Category> Map(CategoryDto dto)
    {
        return Task.FromResult(new Category() { Name = dto.Name });
    }
}