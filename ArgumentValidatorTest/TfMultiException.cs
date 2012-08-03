using System;
using System.Collections.Generic;
using ArgumentValidator;
using Xunit;

namespace AgumentValidatorTests
{
    public class TfMultiException
    {
        [Fact]
        public void Constructor_Default_ShouldPass()
        {
            MultiException me = new MultiException();
            Assert.NotNull(me);
        }

        [Fact]
        public void Constructor_PassExceptionColleciton_ShouldPass()
        {
            MultiException me = new MultiException(new Exception[] { new Exception() });
            Assert.NotNull(me);
            Assert.NotEmpty(me.InnerExceptions);
        }

        [Fact]
        public void Constructor_PassNull_ThrowsException()
        {
            Exception[] e = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MultiException(e);
            });
        }

        [Fact]
        public void Constructor_PassArrayContainingNullReference_ThrowsException()
        {
            Exception[] e = new Exception[] { null };

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MultiException(e);
            });
        }

        [Fact]
        public void Constructor_PassArrayContainingNullReferenceOverload_ThrowsException()
        {
            IEnumerable<Exception> e = new Exception[] { null };

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MultiException(e);
            });
        }

        [Fact]
        public void Constructor_PassNullOverLoad_ThrowsException()
        {
            IEnumerable<Exception> e = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MultiException(e);
            });
        }

        [Fact]
        public void InnerExceptions_DefaultConstructedInstanceReturnsEmptyIEnumerable_ShouldPass()
        {
            MultiException me = new MultiException();
            Assert.Empty(me.InnerExceptions);
        }

        [Fact]
        public void InnerExceptions_InstanceConstructedWithExceptionArrayReturnsThem_ShouldPass()
        {
            ArgumentException ae = new ArgumentException();
            ArgumentNullException ane = new ArgumentNullException();
            Exception[] exceptions = new Exception[] { ae, ane };
            MultiException me = new MultiException(exceptions);
            Assert.NotEmpty(me.InnerExceptions);
            Assert.Contains(ae, me.InnerExceptions);
            Assert.Contains(ane, me.InnerExceptions);
        }

        [Fact]
        public void InnerExceptions_InstanceConstructedWithIEnumerableReturnsThem_ShouldPass()
        {
            ArgumentException ae = new ArgumentException();
            ArgumentNullException ane = new ArgumentNullException();
            IEnumerable<Exception> exceptions = new Exception[] { ae, ane };
            MultiException me = new MultiException(exceptions);
            Assert.NotEmpty(me.InnerExceptions);
            Assert.Contains(ae, me.InnerExceptions);
            Assert.Contains(ane, me.InnerExceptions);
        }
    
    }
}
