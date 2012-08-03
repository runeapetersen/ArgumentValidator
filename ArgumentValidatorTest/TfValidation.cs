using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ArgumentValidator;

namespace AgumentValidatorTests
{
    public class TfValidation
    {
        [Fact]
        public void InternalExceptions_AddAndCheck_ShouldPass()
        {
            Validation val = new Validation();
            val.AddException(new Exception());
            Assert.NotEmpty(val.Exceptions);
        }

        [Fact]
        public void InternalExceptions_ConstructAndCheck_ShouldPass()
        {
            Validation val = new Validation();
            Assert.Empty(val.Exceptions);
        }

        [Fact]
        public void InternalExceptions_AddMultipleExceptions_ShouldPass()
        {
            Validation val = new Validation();
            val.AddException(new Exception());
            val.AddException(new Exception());
            Assert.True(val.Exceptions.Count() == 2);
        }
    }
}
