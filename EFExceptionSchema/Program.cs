using EFExceptionSchema;
using EntityFramework.Exceptions.Common;

using var db = new TestDbContext();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

try
{
    db.IncidentCategories.Add(new EFExceptionSchema.Entities.Incidents.Category
    {
        Name = "Rope Access"
    });
    db.IncidentCategories.Add(new EFExceptionSchema.Entities.Incidents.Category
    {
        Name = "Rope Access"
    });
    db.SaveChanges();
}
catch (UniqueConstraintException)
{
    // Exception is expected here but does not get caught.
}
catch (ArgumentException ex)
{
    // Exception gets caught here but is noted the expected 'UniqueConstraintException'.
    // It thrown by a dictionary because a index with name 'IX_Category_Name' exist for both tables.    
}
catch (Exception ex)
{

}