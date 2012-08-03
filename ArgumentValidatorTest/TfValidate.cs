using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ArgumentValidator;

namespace AgumentValidatorTests
{
    public class TfValidate
    {
        [Fact]
        public void Begin_ReturnNull_PassOk()
        {
            Validation val = Validate.Begin();
            Assert.Null(val);
        }
    }
}
