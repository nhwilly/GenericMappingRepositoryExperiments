using System.Diagnostics;

/// <summary>
/// A strongly typed mapping repository that allows strongly typed document repository and a strongly typed 
/// mapper to be injected.
/// </summary>
public class InvitationRepository : GenericMappingRepositoryBase<Invitation, InvitationDocument>, IInvitationRepository
{
    public InvitationRepository(IDocumentRepository<InvitationDocument> repo, IMapper<Invitation, InvitationDocument> mapper) : base(repo, mapper)
    {

    }
}

/// <summary>
/// This interface is specific to Invitation - and allows it to be injected with a single type reference,
/// this allows the API project to reference the Infrastructure project without know about the mapping classes.
/// 
/// private readonly IInvitationRepository _repository;
/// public class InvitationEndpoint(IInvitationRepository repo)
/// {
///     _repository = repo;
/// }
/// 
/// By implementing an strongly typed IMappingRepository, it now knows the document type to map to.
/// </summary>
public interface IInvitationRepository : IMappingRepository<Invitation, InvitationDocument>
{

}

/// <summary>
/// An interface to define the contracts for a repository that knows about both types.
/// </summary>
/// <typeparam name="TDomain"></typeparam>
/// <typeparam name="TDocument"></typeparam>
public interface IMappingRepository<TDomain, TDocument> where TDomain : IAggregateRoot where TDocument : IDocumentAggregateRoot
{
    public TDomain Save(TDomain domain);
}

/// <summary>
/// The generic mapping implementation that accepts domain entities, maps them to document entities, saves them in the 
/// injected document repository then converts them back to domain entities and returns them.
/// </summary>
/// <typeparam name="TDomain"></typeparam>
/// <typeparam name="TDocument"></typeparam>
public abstract class GenericMappingRepositoryBase<TDomain, TDocument> : IMappingRepository<TDomain, TDocument> where TDomain : IAggregateRoot where TDocument : IDocumentAggregateRoot
{
    private IMapper<TDomain, TDocument> _mapper;
    private IDocumentRepository<TDocument> _documentRepo;
    public GenericMappingRepositoryBase(IDocumentRepository<TDocument> documentRepo, IMapper<TDomain, TDocument> mapper)
    {
        _mapper = mapper;
        _documentRepo = documentRepo;
    }

    public TDomain Save(TDomain entity)
    {
        Console.WriteLine($"original: {entity}");

        var document = _mapper.MapToDocument(entity);
        Console.WriteLine($"mapped to document: {document}");
        var result = _documentRepo.Save(document);
        var mappedResult = _mapper.MapToDomain(result);
        Console.WriteLine($"document mapped back to root: {mappedResult}");
        return mappedResult;
    }
}
