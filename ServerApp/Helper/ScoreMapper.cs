using SimpleMcRecords.Dto;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Helper;

public class ScoreMapper : IMapper<Score, ScoreDto>
{
    public async Task<List<ScoreDto>> MapList(ICollection<Score> list)
    {
        var newList = new List<ScoreDto>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<ScoreDto> Map(Score model)
    {
        return Task.FromResult(new ScoreDto()
        {
            Name = model.Name,
            Value = model.Value
        });
    }
    public async Task<List<Score>> MapList(ICollection<ScoreDto> list)
    {
        var newList = new List<Score>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<Score> Map(ScoreDto dto)
    {
        return Task.FromResult(new Score()
        {
            Name = dto.Name,
            Value = dto.Value
        });
    }
}