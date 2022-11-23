# GenericMappingRepositoryExperiments
This sample was created to solve a very specific problem - a problem which may not exist by the time you read this.

## The Problem We're Trying To Solve

Following a domain driven design approach (DDD) suggests that collection properties inside domain entities should be readonly.  In fact, to update the contents of a collection specific methods to do just that have to be created, e.g. `AddEItem`, `RemoveItem` and so on.  This leaves the entity in full control of the collection, as it should be.

When using Entity Framework Core (EF Core) for a relational database this works just fine.  But when using EF Core for a document database (CosmosDb) the entity is serialized and deserialized using `System.Text.Json` on its way to and from the store.  

And as it turns out, `System.Text.Json` will not populate a readonly collection unless it finds a constructor that uses `public` or `init` setters.  And that breaks the encapsulation we set up for the collection inside the entity.  No bueno.

## A Reasonable Solution

Since this is primarily an infrastructure issue DDD experts (@ardalis) suggest introducing a mapping functionality in that layer, thereby insulating the core domain project from any knowledge about storage concerns.  With a bit of additional work it's even possible to insulate the API layer from knowing about storage issues as well.

To do this, a series of interfaces and generic repositories can be created to effectively manage this.

This example shows have I've done this.

Given that I swim in the shallow end of the coding pool, I'm pretty sure there are better ways to accomplish this.

But it worked for me.

YMMV and I HTH
