using Template.Services.Shared.Implementation;
using Xunit;

namespace Template.Tests.Services.Shared.Implementation;

public class EncryptionServiceTests
{
    [Theory]
    [InlineData("12345678")]
    [InlineData("11111111")]
    [InlineData("7C^&2Cg9")]
    [InlineData("qM4=%#j96e5KdGGH")]
    public void CanValidateComputed(string password)
    {
        #region Arrange

        var encryptionService = new EncryptionService();
        var hash = encryptionService.ComputeHash(password);

        #endregion

        #region Act

        var equals = encryptionService.Validate(hash, password);

        #endregion

        #region Assert
        
        Assert.True(equals);

        #endregion
    }
}