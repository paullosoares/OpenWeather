using System;
using Weather.Core.Extensions;
using Xunit;

namespace Weather.Tests.Integration.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void DateTimeExtensions_ConvertToUnix_MustBeValid()
        {
            var dateNow = Convert.ToDateTime("20/01/2021 13:00:00");
            var universalTime = dateNow.ToUnixTimestamp();
            Assert.Equal(1611158400, universalTime);
        }
    }
}
