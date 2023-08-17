using SimpleMcRecords.Dto;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Helper;

public class PlayerMapper : IMapper<Player, PlayerDto>
{
    public async Task<List<PlayerDto>> MapList(ICollection<Player> list)
    {
        var newList = new List<PlayerDto>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<PlayerDto> Map(Player model)
    {
        return Task.FromResult(new PlayerDto() { Name = model.Name });
    }
    public async Task<List<Player>> MapList(ICollection<PlayerDto> list)
    {
        var newList = new List<Player>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<Player> Map(PlayerDto dto)
    {
        return Task.FromResult(new Player() { Name = dto.Name });
    }
}