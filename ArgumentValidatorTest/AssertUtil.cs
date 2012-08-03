using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UtilityLibraryTest
{
    internal static class AssertUtil
    {
        internal static void ThrowsNamedException(String fullExceptionTypeName, Assert.ThrowsDelegate del)
        {
            bool expectedExceptionWasThrown = false;

            try
            {
                del();
            }
            catch (Exception e)
            {
                if (e.GetType().FullName.Equals(fullExceptionTypeName))
                    expectedExceptionWasThrown = true;
            }

            Assert.True(expectedExceptionWasThrown);
        }
    }
}
