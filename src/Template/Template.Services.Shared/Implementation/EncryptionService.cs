using Template.Services.Shared.Interfaces;

namespace Template.Services.Shared.Implementation;

internal class EncryptionService : IEncryptionService
{
    public string ComputeHash(string value)
        => BCrypt.Net.BCrypt.HashPassword(value);

    public bool Validate(string hash, string password)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}