namespace Smartwyre.DeveloperTest.Data.DbModels;

public abstract class DbModelBase : IDbModel
{
    public string? Id { get; set; }
    public string? Identifier { get; set; }
}
