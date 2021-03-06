﻿/*
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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArgumentValidator
{
    [Serializable]
    internal class ValidationException : AggregateException
    {
        public ValidationException()
            : base() { }

        public ValidationException(IEnumerable<Exception> innerExceptions)
            : base(innerExceptions) { }

        public ValidationException(params Exception[] innerExceptions)
            : base(innerExceptions) { }

        public ValidationException(string message)
            : base(message) { }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException) { }

        public ValidationException(string message, IEnumerable<Exception> innerExceptions)
            : base(message, innerExceptions) { }

        public ValidationException(string message, params Exception[] innerExceptions)
            : base(message, innerExceptions) { }
    }
}
