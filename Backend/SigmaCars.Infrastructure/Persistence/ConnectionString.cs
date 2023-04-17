using Npgsql;

namespace SigmaCars.Infrastructure.Persistence;

public record ConnectionString(string Value)
{
    public NpgsqlConnection CreateNpgsqlConnection() => new NpgsqlConnection(Value);
}