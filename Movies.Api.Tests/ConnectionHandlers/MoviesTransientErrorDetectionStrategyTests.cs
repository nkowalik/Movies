using Movies.Api.ConnectionHandlers;
using NUnit.Framework;
using System.Net;

namespace Movies.Api.Tests.ConnectionHandlers
{
    public class MoviesTransientErrorDetectionStrategyTests
    {
        private MoviesTransientErrorDetectionStrategy _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new MoviesTransientErrorDetectionStrategy();
        }

        [Test]
        public void WhenTimeoutExceptionOccurs_ThenDetectorReturnsTrue()
        {
            var result = _sut.IsTransient(new TimeoutException());

            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenWebExceptionOccurs_ThenDetectorReturnsTrue()
        {
            var result = _sut.IsTransient(new WebException());

            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenUnauthorizedAccessExceptionOccurs_ThenDetectorReturnsFalse()
        {
            var result = _sut.IsTransient(new UnauthorizedAccessException());

            Assert.That(result, Is.False);
        }

        [Test]
        public void WhenSampleExceptionOccurs_ThenDetectorReturnsFalse()
        {
            var result = _sut.IsTransient(new Exception());

            Assert.That(result, Is.False);
        }
    }
}
