using SimpleMcRecords.Dto;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Helper;

public class AdvancementMapper : IMapper<Advancement, AdvancementDto>
{
    private readonly ICategoryRepository _categoryRepo;
    public AdvancementMapper(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }
    public async Task<List<AdvancementDto>> MapList(ICollection<Advancement> list)
    {
        var newList = new List<AdvancementDto>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<AdvancementDto> Map(Advancement model)
    {
        var dto = new AdvancementDto() { Name = model.Name };
        if (model.Category != null)
            dto.CategoryName = model.Category.Name;
        return Task.FromResult(dto);
    }
    public async Task<List<Advancement>> MapList(ICollection<AdvancementDto> list)
    {
        var newList = new List<Advancement>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public async Task<Advancement> Map(AdvancementDto dto)
    {
        var model = new Advancement() { Name = dto.Name };
        var category = await _categoryRepo.GetCategoryByNameAsync(dto.CategoryName!);
        if (category != null)
            model.Category = category;
        return model;
    }
}