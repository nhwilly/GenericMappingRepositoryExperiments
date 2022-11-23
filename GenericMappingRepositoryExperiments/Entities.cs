/// <summary>
/// A document entity which can be mapped to and from a domain entity.
/// </summary>
public class InvitationDocument : IDocumentAggregateRoot
{
    public string Id { get; init; }
    public string Name { get; init; }
    public override string ToString()
    {
        return $"{this.GetType().Name} - Name: '{Name}' - Id: '{Id}'";
    }
}

/// <summary>
/// A sample domain entity.
/// </summary>
public class Invitation : IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{this.GetType().Name} - Name: '{Name}' - Id: '{Id}'";
    }

}

/// <summary>
/// A strongly typed mapper to handle the mapping of invitations to invitation documents.
/// </summary>
public class InvitationMapper : IMapper<Invitation, InvitationDocument>
{
    public InvitationDocument MapToDocument(Invitation aggregateRoot)
    {
        return new InvitationDocument { Name = aggregateRoot.Name, Id = aggregateRoot.Id.ToString() };
    }

    public Invitation MapToDomain(InvitationDocument documentRoot)
    {
        return new Invitation { Name = documentRoot.Name, Id = Guid.Parse(documentRoot.Id) };
    }
}

/// <summary>
/// A generic mapping interface.
/// </summary>
/// <typeparam name="TAggregateRoot"></typeparam>
/// <typeparam name="TDocumentAggregateRoot"></typeparam>
public interface IMapper<TAggregateRoot, TDocumentAggregateRoot>
    where TAggregateRoot : IAggregateRoot
    where TDocumentAggregateRoot : IDocumentAggregateRoot
{
    TDocumentAggregateRoot MapToDocument(TAggregateRoot aggregateRoot);
    TAggregateRoot MapToDomain(TDocumentAggregateRoot documentRoot);
}

/// <summary>
/// A marker interface for domain entities.
/// </summary>
public interface IAggregateRoot { }

/// <summary>
/// A marker interface for document entiies.
/// </summary>
public interface IDocumentAggregateRoot { }