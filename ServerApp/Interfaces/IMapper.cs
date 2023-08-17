namespace SimpleMcRecords.Interfaces;

public interface IMapper<T, R>
{
    Task<List<R>> MapList(ICollection<T> list);
    Task<R> Map(T model);
    Task<List<T>> MapList(ICollection<R> list);
    Task<T> Map(R dto);
}