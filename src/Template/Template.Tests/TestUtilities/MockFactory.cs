using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace Template.Tests.TestUtilities;

internal static class MockFactory
{
    internal static Mock<ILogger<T>> CreateLoggerFor<T>()
    {
        var logger = new Mock<ILogger<T>>();
        
        logger.Setup(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.IsAny<object>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<object, Exception, string>>()));

        return logger;
    }
}