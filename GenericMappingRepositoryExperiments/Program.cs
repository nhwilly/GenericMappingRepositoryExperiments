using Microsoft.Extensions.DependencyInjection;

public partial class Program
{
    /// <summary>
    /// This has been dummied together to show how to register interfaces and implementations
    /// in your startup code.
    /// 
    /// At the bottom, we simulate the endpoint getting a mapping repository based on the interface
    /// for the type.
    /// 
    /// Functionally this prevents the domain project from knowing anything about mapping entities, etc.
    /// 
    /// A project containing an endpoint need only know what type of repository they need and no longer
    /// needs to know anything about mapping at all.
    /// 
    /// Any implementation of this needs a lot more safeguards, etc.  This was just to get the types
    /// and dependency injection correctly demonstrated.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        // register the mapper for invitations
        services.AddTransient(typeof(IMapper<Invitation, InvitationDocument>), typeof(InvitationMapper));

        // register a generic document repository
        services.AddTransient(typeof(IDocumentRepository<>), typeof(DocumentRepository<>));

        // register the marker interface
        services.AddTransient(typeof(IInvitationRepository), typeof(InvitationRepository));

        // register the dbContext (normally and EF Core dbContext)
        services.AddTransient(typeof(FakeDbContext<>));

        var provider = services.BuildServiceProvider();

        // create instance of typed mapping repository from a single interface
        var invitationRepo = provider.GetRequiredService<IInvitationRepository>();

        // try it ...
        var invitation = new Invitation { Name = "bobo", Id = Guid.NewGuid() };
        var result = invitationRepo.Save(invitation);

        Console.WriteLine("Fini...");
        Console.ReadLine();
    }
}