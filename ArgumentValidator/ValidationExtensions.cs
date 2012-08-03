/*
 * Copyright 2012 Rune Allan Petersen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ArgumentValidator
{
    /// <summary>
    /// This static type defines the extension methods to the Validation type. When adding new extensions the developer should follow the established
    /// pattern for argument order, which is (1) the object to be validated, (2) the name of the parameter, and (3) any further arguments relevant for
    /// the validation.
    /// Note that the parameter name should always be specified. The library aggressively checks against Null references passed as an argument to the parameterName parameter.
    /// Example of the idiomatic use of this library can be found in the following blog: http://blog.getpaint.net/2008/12/06/a-fluent-approach-to-c-parameter-validation/
    /// More information can be found in the document 'ArgumentValidator overview.doc' which has been included in this solution.
    /// </summary>
    public static class ValidationExtensions
    {
        #region Public methods

        /// <summary>
        /// Validate that the argument is not a null-reference.
        /// </summary>
        /// <typeparam name="T">The type of the argument being checked</typeparam>
        /// <param name="validation">The extended type instance</param>
        /// <param name="theObject">The argument reference to check</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation ObjectReferenceIsNotNull<T>(this Validation validation, T theObject, String parameterName)
            where T : class
        {
            if (parameterName == null)
                throw new ArgumentNullException("parameterName", "The parameter name must be specified.");

            if (theObject == null)
                return AddArgumentNullException(validation, parameterName, "The argument cannot be null.");
            else
                return validation;
        }

        #region IsPositive methods

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">The value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Double value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">The value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Int32 value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Decimal value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Single value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, SByte value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Int16 value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Int64 value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, UInt16 value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, UInt32 value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, UInt64 value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Byte value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositive(this Validation validation, Char value, String parameterName)
        {
            return IsNumberPositive(validation, value, parameterName);
        }

        #endregion

        #region IsNegative methods

        /// <summary>
        /// Validate whether the argument has a negative value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegative(this Validation validation, Double value, String parameterName)
        {
            return IsNumberNegative(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegative(this Validation validation, Int32 value, String parameterName)
        {
            return IsNumberNegative(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegative(this Validation validation, Decimal value, String parameterName)
        {
            return IsNumberNegative(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegative(this Validation validation, Single value, String parameterName)
        {
            return IsNumberNegative(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegative(this Validation validation, SByte value, String parameterName)
        {
            return IsNumberNegative(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegative(this Validation validation, Int16 value, String parameterName)
        {
            return IsNumberNegative(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegative(this Validation validation, Int64 value, String parameterName)
        {
            return IsNumberNegative(validation, value, parameterName);
        }

        #endregion

        #region IsPositiveOrZero methods

        /// <summary>
        /// Validate whether the argument has a positive value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositiveOrZero(this Validation validation, Double value, String parameterName)
        {
            return IsNumberPositiveOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositiveOrZero(this Validation validation, Int32 value, String parameterName)
        {
            return IsNumberPositiveOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositiveOrZero(this Validation validation, Decimal value, String parameterName)
        {
            return IsNumberPositiveOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositiveOrZero(this Validation validation, Single value, String parameterName)
        {
            return IsNumberPositiveOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositiveOrZero(this Validation validation, SByte value, String parameterName)
        {
            return IsNumberPositiveOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositiveOrZero(this Validation validation, Int16 value, String parameterName)
        {
            return IsNumberPositiveOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a positive value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsPositiveOrZero(this Validation validation, Int64 value, String parameterName)
        {
            return IsNumberPositiveOrZero(validation, value, parameterName);
        }

        #endregion

        #region IsNegativeOrZero methods

        /// <summary>
        /// Validate whether the argument has a negative value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegativeOrZero(this Validation validation, SByte value, String parameterName)
        {
            return IsNumberNegativeOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegativeOrZero(this Validation validation, Decimal value, String parameterName)
        {
            return IsNumberNegativeOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegativeOrZero(this Validation validation, Double value, String parameterName)
        {
            return IsNumberNegativeOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegativeOrZero(this Validation validation, Single value, String parameterName)
        {
            return IsNumberNegativeOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegativeOrZero(this Validation validation, Int16 value, String parameterName)
        {
            return IsNumberNegativeOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegativeOrZero(this Validation validation, Int32 value, String parameterName)
        {
            return IsNumberNegativeOrZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument has a negative value or is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNegativeOrZero(this Validation validation, Int64 value, String parameterName)
        {
            return IsNumberNegativeOrZero(validation, value, parameterName);
        }

        #endregion

        #region IsZero methods

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Double value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Int32 value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Decimal value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Single value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, SByte value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Int16 value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Int64 value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, UInt16 value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, UInt32 value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, UInt64 value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Byte value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsZero(this Validation validation, Char value, String parameterName)
        {
            return IsNumberZero(validation, value, parameterName);
        }

        #endregion

        #region IsNotZero methods

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Double value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Int32 value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Decimal value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Single value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, SByte value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Int16 value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Int64 value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, UInt16 value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, UInt32 value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, UInt64 value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Byte value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        /// <summary>
        /// Validate whether the argument is not zero.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">the value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of validation</returns>
        public static Validation NumericValueIsNotZero(this Validation validation, Char value, String parameterName)
        {
            return IsNumberNotZero(validation, value, parameterName);
        }

        #endregion

        /// <summary>
        /// Extension method which will validate that a collection implementing the generic IEnumerable interface contains at least one element.
        /// </summary>
        /// <typeparam name="T">The generic type which is defined in the generic IEnumerable interface</typeparam>
        /// <param name="validation">the extended type instance</param>
        /// <param name="collection">The argument reference to check</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation CollectionIsNotNullOrEmpty<T>(this Validation validation, IEnumerable<T> collection, String parameterName)
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            Validation localVal = Validate.Begin().ObjectReferenceIsNotNull(collection, parameterName);

            if (localVal != null)
                if (validation != null)
                {
                    validation.AddException(localVal.Exceptions.First<Exception>());
                    return validation;
                }
                else
                    return localVal;

            if (collection.Count() > 0)
                return validation;
            else
                return AddArgumentException(validation, parameterName, "The collection must contain at least one element");
        }

        /// <summary>
        /// Extension method which will validate that a collection implementing the IEnumerable interface contains at least one element.
        /// </summary>
        /// <param name="validation">the extended type instance</param>
        /// <param name="collection">The argument reference to check</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation CollectionIsNotNullOrEmpty(this Validation validation, IEnumerable collection, String parameterName)
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            Validation localVal = Validate.Begin().ObjectReferenceIsNotNull(collection, parameterName);

            if (localVal != null)
                if (validation != null)
                {
                    validation.AddException(localVal.Exceptions.First<Exception>());
                    return validation;
                }
                else
                    return localVal;

            if (collection.Count() > 0)
                return validation;
            else
                return AddArgumentException(validation, parameterName, "The collection must contain at least one element");
        }

        /// <summary>
        /// Validate that a string is not a null-reference or empty.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">The value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation StringIsNotNullOrEmpty(this Validation validation, String value, String parameterName)
        {
            Validate.Begin()
                .ObjectReferenceIsNotNull(parameterName, "parameterName")
                .Check();

            if (String.IsNullOrEmpty(value))
                return AddArgumentOutOfRangeException(validation, parameterName, "Empty string or null reference is not accepted as an argument of type System.String");
            else
                return validation;
        }

        /// <summary>
        /// Validate that an instance of DateTime differs from a default-constructed instance of DateTime.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">The value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation DateTimeIsInitialized(this Validation validation, DateTime value, String parameterName)
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            if (value == DateTime.MinValue)
                return AddArgumentOutOfRangeException(validation, parameterName, "must be initialized to a date other than DateTime.Min");
            else
                return validation;
        }

        /// <summary>
        /// Validate that the integer argument is within a desired range
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">The value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="lowerBoundInclusive">The lower bound of the accepted range.</param>
        /// <param name="upperBoundInclusive">The upper bound of the accepted range.</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation NumericValueIsWithinRange(this Validation validation, Int32 value, String parameterName, Int32 lowerBoundInclusive, Int32 upperBoundInclusive)
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            if (!(lowerBoundInclusive <= value && value <= upperBoundInclusive))
                return AddArgumentOutOfRangeException(validation, parameterName, "must be within the range of " + lowerBoundInclusive + " - " + upperBoundInclusive);
            else
                return validation;
        }

        /// <summary>
        /// Validate that an Enumeration value is actually defined correctly.
        /// Note that this method will throw an ArgumentException if T is defined as anything but an Enum. This is necessary to work around the fact that the Enum type is not a valid Generics constraint in C#.
        /// </summary>
        /// <typeparam name="T">The Enum to check against</typeparam>
        /// <param name="validation">The extended type instance</param>
        /// <param name="value">The value of the argument</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns></returns>
        public static Validation EnumIsDefined<T>(this Validation validation, T value, String parameterName) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("This generic method only accepts Enums as valid types of T.", "value");

            CheckParameterNameNotNullOrEmpty(parameterName);

            if (Enum.IsDefined(typeof(T), value))
                return validation;
            else
                return AddArgumentOutOfRangeException(validation, parameterName, "must be a defined Enumeration value, but was " + value.ToString());
        }

        /// <summary>
        /// Check whether the Validations performed have thus far have been successful. If any failed validations have occurred these will be thrown when executing this method.
        /// </summary>
        /// <param name="validation">The extended type instance</param>
        /// <returns>Reference to an instance of Validation</returns>
        public static Validation Check(this Validation validation)
        {
            if (validation == null)
                return validation;
            else
            {
                if (validation.Exceptions.Take(2).Count() == 1)
                    throw new ValidationException("message", validation.Exceptions.First()); // ValidationException is just a standard Exception-derived class with the usual four constructors
                else
                    throw new ValidationException("message", new MultiException(validation.Exceptions));
            }
        }

        #endregion

        #region Private methods

        private static Validation IsNumberZero<T>(Validation validation, T value, String parameterName) where T : IComparable
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            object zero = System.Convert.ChangeType(0, typeof(T), CultureInfo.InvariantCulture);

            if (value.CompareTo(zero) == 0)
                return validation;
            else
                return AddArgumentOutOfRangeException(validation, parameterName, "must be zero, but was " + value.ToString());
        }

        private static Validation AddArgumentOutOfRangeException(Validation validation, String parameterName, String message)
        {
            return CheckValidation(validation).AddException(new ArgumentOutOfRangeException(parameterName, message));
        }

        private static Validation AddArgumentNullException(Validation validation, String parameterName, String message)
        {
            return CheckValidation(validation).AddException(new ArgumentNullException(parameterName, message));
        }

        private static Validation AddArgumentException(Validation validation, String parameterName, String message)
        {
            return CheckValidation(validation).AddException(new ArgumentException(message, parameterName));
        }

        private static Validation CheckValidation(Validation validation)
        {
            return validation ?? new Validation();
        }

        private static Validation IsNumberPositive<T>(Validation validation, T value, String parameterName) where T : IComparable
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            object zero = System.Convert.ChangeType(0, typeof(T), CultureInfo.InvariantCulture);

            if (value.CompareTo(zero) > 0)
                return validation;
            else
                return AddArgumentOutOfRangeException(validation, parameterName, "must be positive, but was " + value.ToString());
        }

        private static Validation IsNumberNegative<T>(Validation validation, T value, String parameterName) where T : IComparable
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            object zero = System.Convert.ChangeType(0, typeof(T), CultureInfo.InvariantCulture);

            if (value.CompareTo(zero) < 0)
                return validation;
            else
                return AddArgumentOutOfRangeException(validation, parameterName, "must be negative, but was " + value.ToString());
        }

        private static Validation IsNumberPositiveOrZero<T>(Validation validation, T value, string parameterName) where T : IComparable
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            object zero = System.Convert.ChangeType(0, typeof(T), CultureInfo.InvariantCulture);

            if (value.CompareTo(zero) >= 0)
                return validation;
            else
                return AddArgumentOutOfRangeException(validation, parameterName, "must be zero or positive, but was " + value.ToString());
        }

        private static Validation IsNumberNegativeOrZero<T>(Validation validation, T value, string parameterName) where T : IComparable
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            object zero = System.Convert.ChangeType(0, typeof(T), CultureInfo.InvariantCulture);

            if (value.CompareTo(zero) <= 0)
                return validation;
            else
                return AddArgumentOutOfRangeException(validation, parameterName, "must be zero or negative, but was " + value.ToString());
        }

        private static Validation IsNumberNotZero<T>(Validation validation, T value, string parameterName) where T : IComparable
        {
            CheckParameterNameNotNullOrEmpty(parameterName);

            object zero = System.Convert.ChangeType(0, typeof(T), CultureInfo.InvariantCulture);

            if (value.CompareTo(zero) != 0)
                return validation;
            else
                return AddArgumentOutOfRangeException(validation, parameterName, "must not be zero");
        }

        private static void CheckParameterNameNotNullOrEmpty(String parameterName)
        {
            Validate.Begin()
                .StringIsNotNullOrEmpty(parameterName, "parameterName")
                .Check();
        }

        #endregion
    }
}
