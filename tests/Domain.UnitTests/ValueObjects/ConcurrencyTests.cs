﻿namespace iot.Domain.UnitTests.ValueObjects
{
    public class ConcurrencyTests
    {
        [Fact]
        public void Should_Not_Be_Null_Or_Empty()
        {
            var resultConcurrency = Concurrency.NewToken();

            Assert.NotEmpty(resultConcurrency);
            Assert.NotNull(resultConcurrency);
        }

        [Fact]
        public void Should_Token_Has_Sixteen_Characters()
        {
            var resultConcurrency = Concurrency.NewToken();

            Assert.True(resultConcurrency.Length < 17);
            Assert.True(resultConcurrency.Length > 15);
        }
    }
}