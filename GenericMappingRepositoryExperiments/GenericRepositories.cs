/// <summary>
/// An interface for a repository that saves documents to an EF Core Cosmos database
/// instead of a relational one.  Only 'Save' is shown as an example.
/// </summary>
/// <typeparam name="TDocument"></typeparam>
public interface IDocumentRepository<TDocument> where TDocument : IDocumentAggregateRoot
{
    public TDocument Save(TDocument document);
}

/// <summary>
/// A repository that implements the IDocumentRepository interface.  This repository
/// actually fronts the EF Core and would normally return things other than pure documents,
/// such as a Result<Invitation> or something like that.
/// </summary>
/// <typeparam name="TDocument"></typeparam>
public class DocumentRepository<TDocument> : IDocumentRepository<TDocument> where TDocument : IDocumentAggregateRoot
{
    private readonly FakeDbContext<TDocument> _context;
    public DocumentRepository(FakeDbContext<TDocument> context)
    {
        _context = context;
    }
    public TDocument Save(TDocument document)
    {
        var result = _context.Save(document);
        return document;
    }
}

/// <summary>
/// A faker.  This would normally be an EF Core dbContext instance.
/// </summary>
/// <typeparam name="TDocument"></typeparam>
public class FakeDbContext<TDocument>
{
    public TDocument Save(TDocument document)
    {
        return document;
    }
}