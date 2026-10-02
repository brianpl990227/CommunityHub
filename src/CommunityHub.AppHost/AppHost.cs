var builder = DistributedApplication.CreateBuilder(args);

// Sin contenedores: la API usa SQLite, así que todo arranca sin Docker.
var api = builder.AddProject<Projects.CommunityHub_Api>("api");

builder.AddProject<Projects.CommunityHub_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
