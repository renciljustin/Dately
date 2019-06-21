namespace Dately.Persistence.QueryDtos
{
    public class BaseQueryDto
    {
        public string SortBy { get; set; }
        public bool IsOrderDescending { get; set; }
        public int Page { get; set; }
        public short PageSize { get; set; }
    }
}