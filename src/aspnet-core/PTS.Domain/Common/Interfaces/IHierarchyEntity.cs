namespace PTS.Domain.Common.Interfaces
{
    public interface IHierarchyEntity
    {
        int? ParentId { get; set; }
        int? DisplayOrder { get; set; }
        string Path { get; set; }
    }
}
