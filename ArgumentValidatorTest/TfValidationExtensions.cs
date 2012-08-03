using System;
using ArgumentValidator;
using Xunit;
using System.Collections;
using System.Collections.Generic;

namespace AgumentValidatorTests
{
    using UtilityLibraryTest;

    public class TfValidationExtensions
    {
        private readonly static string validationException = "ArgumentValidator.ValidationException";
        private readonly static string argumentException = "System.ArgumentException";

        [Fact]
        public void ValidationDecorator_PassOk()
        {
            Validation val = Validate.Begin();
            Validation val2 = val.Check();
            Assert.Null(val);
            Assert.Null(val2);
        }

        [Fact]
        public void ChainingTest_PassOk()
        {
            Validation val = Validate.Begin()
                .ObjectReferenceIsNotNull(new object(), "object")
                .NumericValueIsPositive(2, "Int32")
                .Check();
            Assert.Null(val);
        }

        [Fact]
        public void NullReferenceTest_CheckFails()
        {
            string s = null;
            Validation val = Validate.Begin()
                .ObjectReferenceIsNotNull(s, "myParameter");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Validtion_MultipleValidationErrorsThrowMultiException()
        {
            string s = null;
            Validation val = Validate.Begin()
                .ObjectReferenceIsNotNull(s, "someParam")
                .ObjectReferenceIsNotNull(s, "someOtherParam");

            ValidationException thrownEx = null;

            try
            {
                val.Check();
            }
            catch (ValidationException ex)
            {
                thrownEx = ex;
            }

            Assert.IsType(typeof(MultiException), thrownEx.InnerException);
        }

        [Fact]
        public void StringIsNotNullOrEmpty_StringEmpty_CheckFails()
        {
            string s = string.Empty;
            Validation val = Validate.Begin()
                .StringIsNotNullOrEmpty(s, "myString");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void StringIsNotNullOrEmpty_StringNull_CheckFails()
        {
            string s = null;
            Validation val = Validate.Begin()
                .StringIsNotNullOrEmpty(s, "myString");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        public enum TestEnum
        {
            FirstValue = 1,
            SecondValue = 3
        }

        public enum TestEnum2
        {
            FirstValue,
            SecondValue
        }

        [Fact]
        public void EnumValidation_CorrectEnumValue_PassOk()
        {
            TestEnum t = (TestEnum)1;
            Validation val = Validate.Begin()
                .EnumIsDefined<TestEnum>(t, "myParameter");
            Assert.Null(val);
        }

        [Fact]
        public void EnumValidation_UndefinedEnumValue_CheckFails()
        {
            TestEnum t = (TestEnum)2;
            Validation val = Validate.Begin()
                .EnumIsDefined<TestEnum>(t, "myParameter");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void EnumValidation2_DefinedEnumValue_PassOk()
        {
            TestEnum2 t = 0;
            Validation val = Validate.Begin()
                .EnumIsDefined<TestEnum2>(t, "myParameter");
            Assert.Null(val);
        }

        [Fact]
        public void EnumValidation2_DefinedEnumValue2_PassOk()
        {
            TestEnum2 t = TestEnum2.FirstValue;
            Validation val = Validate.Begin()
                .EnumIsDefined<TestEnum2>(t, "myParameter");
            Assert.Null(val);
        }

        [Fact]
        public void EnumValidation2_UndefinedEnumValue2_CheckFails()
        {
            TestEnum2 t = (TestEnum2)2;
            Validation val = Validate.Begin()
                .EnumIsDefined<TestEnum2>(t, "myParameter");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void EnumValidation2_NotEnumType_ThrowsException()
        {
            int i = 1;
            AssertUtil.ThrowsNamedException(argumentException, () =>
                {
                    Validation val = Validate.Begin()
                        .EnumIsDefined<Int32>(i, "myParameter");
                });
        }

        [Fact]
        public void DateTimeValidation_InstantiateStandardValue_CheckFail()
        {
            DateTime dt = new DateTime();
            Validation val = Validate.Begin()
                .DateTimeIsInitialized(dt, "myParameter");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DateTimeValidation_InstantiateDefinedValue_PassOk()
        {
            DateTime dt = new DateTime(1);
            Validation val = Validate.Begin()
                .DateTimeIsInitialized(dt, "myParameter");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Positive_NumericValueTestPositive_PassOk()
        {
            Int32 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Zero_NumericValueTestPositive_CheckFail()
        {
            Int32 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Negative_NumericValueTestPositive_CheckFail()
        {
            Int32 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Positive_NumericValueTestNegative_CheckFail()
        {
            Int32 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Zero_NumericValueTestNegative_CheckFail()
        {
            Int32 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Negative_NumericValueTestNegative_PassOk()
        {
            Int32 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Positive_NumericValueTestNegativeOrZero_CheckFail()
        {
            Int32 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Zero_NumericValueTestNegativeOrZero_PassOk()
        {
            Int32 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Negative_NumericValueTestNegativeOrZero_PassOk()
        {
            Int32 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Positive_NumericValueTestZero_CheckFail()
        {
            Int32 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Zero_NumericValueTestZero_PassOk()
        {
            Int32 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Negative_NumericValueTestZero_CheckFail()
        {
            Int32 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Positive_NumericValueTestNotZero_PassOk()
        {
            Int32 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Zero_NumericValueTestNotZero_CheckFail()
        {
            Int32 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int32Negative_NumericValueTestNotZero_PassOk()
        {
            Int32 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Positive_NumericValueTestPositiveOrZero_PassOk()
        {
            Int32 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Zero_NumericValueTestPositiveOrZero_PassOk()
        {
            Int32 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int32Negative_NumericValueTestPositiveOrZero_CheckFail()
        {
            Int32 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Positive_NumericValueTestPositiveOrZero_PassOk()
        {
            Int64 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Negative_NumericValueTestPositiveOrZero_CheckFail()
        {
            Int64 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Zero_NumericValueTestPositiveOrZero_PassOk()
        {
            Int64 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Positive_NumericValueTestPositive_PassOk()
        {
            Int64 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Zero_NumericValueTestPositive_CheckFail()
        {
            Int64 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Negative_NumericValueTestPositive_CheckFail()
        {
            Int64 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Positive_NumericValueTestNegative_CheckFail()
        {
            Int64 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Zero_NumericValueTestNegative_CheckFail()
        {
            Int64 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Negative_NumericValueTestNegative_PassOk()
        {
            Int64 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Positive_NumericValueTestNegativeOrZero_CheckFail()
        {
            Int64 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Zero_NumericValueTestNegativeOrZero_PassOk()
        {
            Int64 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Negative_NumericValueTestNegativeOrZero_PassOk()
        {
            Int64 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Positive_NumericValueTestZero_CheckFail()
        {
            Int64 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Zero_NumericValueTestZero_PassOk()
        {
            Int64 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Negative_NumericValueTestZero_CheckFail()
        {
            Int64 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Positive_NumericValueTestNotZero_PassOk()
        {
            Int64 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int64Zero_NumericValueTestNotZero_CheckFail()
        {
            Int64 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int64Negative_NumericValueTestNotZero_PassOk()
        {
            Int64 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalPositive_NumericValueTestPositiveOrZero_PassOk()
        {
            Decimal i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalNegative_NumericValueTestPositiveOrZero_CheckFail()
        {
            Decimal i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalZero_NumericValueTestPositiveOrZero_PassOk()
        {
            Decimal i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalPositive_NumericValueTestPositive_PassOk()
        {
            Decimal i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalZero_NumericValueTestPositive_CheckFail()
        {
            Decimal i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalNegative_NumericValueTestPositive_CheckFail()
        {
            Decimal i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalPositive_NumericValueTestNegative_CheckFail()
        {
            Decimal i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalZero_NumericValueTestNegative_CheckFail()
        {
            Decimal i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalNegative_NumericValueTestNegative_PassOk()
        {
            Decimal i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalPositive_NumericValueTestNegativeOrZero_CheckFail()
        {
            Decimal i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalZero_NumericValueTestNegativeOrZero_PassOk()
        {
            Decimal i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalNegative_NumericValueTestNegativeOrZero_PassOk()
        {
            Decimal i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalPositive_NumericValueTestZero_CheckFail()
        {
            Decimal i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalZero_NumericValueTestZero_PassOk()
        {
            Decimal i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalNegative_NumericValueTestZero_CheckFail()
        {
            Decimal i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalPositive_NumericValueTestNotZero_PassOk()
        {
            Decimal i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DecimalZero_NumericValueTestNotZero_CheckFail()
        {
            Decimal i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DecimalNegative_NumericValueTestNotZero_PassOk()
        {
            Decimal i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoublePositive_NumericValueTestPositiveOrZero_PassOk()
        {
            Double i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoubleNegative_NumericValueTestPositiveOrZero_CheckFail()
        {
            Double i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoubleZero_NumericValueTestPositiveOrZero_PassOk()
        {
            Double i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoublePositive_NumericValueTestPositive_PassOk()
        {
            Double i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoubleZero_NumericValueTestPositive_CheckFail()
        {
            Double i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoubleNegative_NumericValueTestPositive_CheckFail()
        {
            Double i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoublePositive_NumericValueTestNegative_CheckFail()
        {
            Double i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoubleZero_NumericValueTestNegative_CheckFail()
        {
            Double i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoubleNegative_NumericValueTestNegative_PassOk()
        {
            Double i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoublePositive_NumericValueTestNegativeOrZero_CheckFail()
        {
            Double i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoubleZero_NumericValueTestNegativeOrZero_PassOk()
        {
            Double i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoubleNegative_NumericValueTestNegativeOrZero_PassOk()
        {
            Double i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoublePositive_NumericValueTestZero_CheckFail()
        {
            Double i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoubleZero_NumericValueTestZero_PassOk()
        {
            Double i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoubleNegative_NumericValueTestZero_CheckFail()
        {
            Double i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoublePositive_NumericValueTestNotZero_PassOk()
        {
            Double i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void DoubleZero_NumericValueTestNotZero_CheckFail()
        {
            Double i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void DoubleNegative_NumericValueTestNotZero_PassOk()
        {
            Double i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Positive_NumericValueTestPositiveOrZero_PassOk()
        {
            Int16 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Negative_NumericValueTestPositiveOrZero_CheckFail()
        {
            Int16 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Zero_NumericValueTestPositiveOrZero_PassOk()
        {
            Int16 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Positive_NumericValueTestPositive_PassOk()
        {
            Int16 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Zero_NumericValueTestPositive_CheckFail()
        {
            Int16 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Negative_NumericValueTestPositive_CheckFail()
        {
            Int16 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Positive_NumericValueTestNegative_CheckFail()
        {
            Int16 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Zero_NumericValueTestNegative_CheckFail()
        {
            Int16 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Negative_NumericValueTestNegative_PassOk()
        {
            Int16 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Positive_NumericValueTestNegativeOrZero_CheckFail()
        {
            Int16 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Zero_NumericValueTestNegativeOrZero_PassOk()
        {
            Int16 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Negative_NumericValueTestNegativeOrZero_PassOk()
        {
            Int16 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Positive_NumericValueTestZero_CheckFail()
        {
            Int16 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Zero_NumericValueTestZero_PassOk()
        {
            Int16 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Negative_NumericValueTestZero_CheckFail()
        {
            Int16 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Positive_NumericValueTestNotZero_PassOk()
        {
            Int16 i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void Int16Zero_NumericValueTestNotZero_CheckFail()
        {
            Int16 i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void Int16Negative_NumericValueTestNotZero_PassOk()
        {
            Int16 i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SinglePositive_NumericValueTestPositiveOrZero_PassOk()
        {
            Single i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SingleNegative_NumericValueTestPositiveOrZero_CheckFail()
        {
            Single i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SingleZero_NumericValueTestPositiveOrZero_PassOk()
        {
            Single i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SinglePositive_NumericValueTestPositive_PassOk()
        {
            Single i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SingleZero_NumericValueTestPositive_CheckFail()
        {
            Single i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SingleNegative_NumericValueTestPositive_CheckFail()
        {
            Single i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SinglePositive_NumericValueTestNegative_CheckFail()
        {
            Single i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SingleZero_NumericValueTestNegative_CheckFail()
        {
            Single i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SingleNegative_NumericValueTestNegative_PassOk()
        {
            Single i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SinglePositive_NumericValueTestNegativeOrZero_CheckFail()
        {
            Single i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SingleZero_NumericValueTestNegativeOrZero_PassOk()
        {
            Single i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SingleNegative_NumericValueTestNegativeOrZero_PassOk()
        {
            Single i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SinglePositive_NumericValueTestZero_CheckFail()
        {
            Single i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SingleZero_NumericValueTestZero_PassOk()
        {
            Single i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SingleNegative_NumericValueTestZero_CheckFail()
        {
            Single i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SinglePositive_NumericValueTestNotZero_PassOk()
        {
            Single i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void SingleZero_NumericValueTestNotZero_CheckFail()
        {
            Single i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void SingleNegative_NumericValueTestNotZero_PassOk()
        {
            Single i = -1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void BytePositive_NumericValueTestPositiveOrZero_PassOk()
        {
            Byte i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void ByteZero_NumericValueTestPositiveOrZero_PassOk()
        {
            Byte i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositiveOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void BytePositive_NumericValueTestPositive_PassOk()
        {
            Byte i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void ByteZero_NumericValueTestPositive_CheckFail()
        {
            Byte i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsPositive(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void BytePositive_NumericValueTestNegative_CheckFail()
        {
            Byte i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void ByteZero_NumericValueTestNegative_CheckFail()
        {
            Byte i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegative(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void BytePositive_NumericValueTestNegativeOrZero_CheckFail()
        {
            Byte i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void ByteZero_NumericValueTestNegativeOrZero_PassOk()
        {
            Byte i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNegativeOrZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void BytePositive_NumericValueTestZero_CheckFail()
        {
            Byte i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void ByteZero_NumericValueTestZero_PassOk()
        {
            Byte i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void BytePositive_NumericValueTestNotZero_PassOk()
        {
            Byte i = 1;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.Null(val);
        }

        [Fact]
        public void ByteZero_NumericValueTestNotZero_CheckFail()
        {
            Byte i = 0;
            Validation val = Validate.Begin()
                .NumericValueIsNotZero(i, "myParam");
            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void NullReference_GenericCollectionNotNullOrEmpty_CheckFail()
        {
            IEnumerable<int> testCollection = null;

            Validation val = Validate.Begin()
                .CollectionIsNotNullOrEmpty(testCollection, "myParam");

            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void EmptyCollection_GenericCollectionNotNullOrEmpty_CheckFail()
        {
            IEnumerable<int> testCollection = new List<int>();

            Validation val = Validate.Begin()
                .CollectionIsNotNullOrEmpty(testCollection, "myParam");

            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void CollectionWithElements_GenericCollectionNotNullOrEmpty_PassOk()
        {
            IEnumerable<int> testCollection = new List<int>() { 1, 2, 3 };

            Validation val = Validate.Begin()
                .CollectionIsNotNullOrEmpty(testCollection, "myParam");

            Assert.Null(val);
        }

        [Fact]
        public void NullReference_CollectionNotNullOrEmpty_CheckFail()
        {
            IEnumerable testCollection = null;

            Validation val = Validate.Begin()
                .CollectionIsNotNullOrEmpty(testCollection, "myParam");

            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void EmptyCollection_CollectionNotNullOrEmpty_CheckFail()
        {
            IEnumerable testCollection = new ArrayList();

            Validation val = Validate.Begin()
                .CollectionIsNotNullOrEmpty(testCollection, "myParam");

            Assert.NotNull(val);
            AssertUtil.ThrowsNamedException(validationException, () => val.Check());
        }

        [Fact]
        public void CollectionWithElements_CollectionNotNullOrEmpty_PassOk()
        {
            IEnumerable testCollection = new ArrayList() { 1 };

            Validation val = Validate.Begin()
                .CollectionIsNotNullOrEmpty(testCollection, "myParam");

            Assert.Null(val);
        }
    }
}
