namespace API.Shared;

public record Pagination<T>(int PageIndex, int PageSize, int Count, IReadOnlyList<T> Data) {

}
