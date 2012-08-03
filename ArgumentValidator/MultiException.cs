﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArgumentValidator
{
    /// <summary>
    /// This type can contain a number of Exception-derived references.
    /// </summary>
    [Serializable]
    public sealed class MultiException
        : Exception
    {
        private readonly IList<Exception> m_InnerExceptions = new List<Exception>();

        /// <summary>
        /// Initializes a new instance of the MultiException class.
        /// </summary>
        public MultiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiException"/> class. 
        /// .ctor
        /// </summary>
        /// <param name="message">
        /// </param>
        public MultiException(string message): base(message)
        {
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MultiException(string message, Exception innerException)
            : base(message, innerException)
        {
            m_InnerExceptions.Add(innerException);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="innerExceptions"></param>
        public MultiException(IEnumerable<Exception> innerExceptions)
            : this(null, innerExceptions)
        {
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="innerExceptions"></param>
        public MultiException(Exception[] innerExceptions)
            : this(null, (IEnumerable<Exception>)innerExceptions)
        {
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerExceptions"></param>
        public MultiException(string message, Exception[] innerExceptions)
            : this(message, (IEnumerable<Exception>)innerExceptions)
        {
        }

        /// <summary>
        /// .ctor 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerExceptions"></param>
        public MultiException(string message, IEnumerable<Exception> innerExceptions)
            : base(message, innerExceptions.FirstOrDefault())
        {
            if (innerExceptions.AnyNull())
            {
                throw new ArgumentNullException("innerExceptions", "Cannot pass collection that contains null references");
            }

            m_InnerExceptions = innerExceptions.ToList();
        }

        private MultiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets a reference to a collection of internally held references of System.Exception
        /// </summary>
        public IEnumerable<Exception> InnerExceptions
        {
            get
            {
                if (this.m_InnerExceptions == null)
                {
                }
                else
                {
                    foreach (Exception t in this.m_InnerExceptions)
                    {
                        yield return t;
                    }
                }
            }
        }

        /// <summary>
        /// Creates and returns a string representation of the current exception.
        /// </summary>
        /// <returns>A string representation of the current exception.</returns>
        public override string ToString()
        {
            string baseString = base.ToString();

            StringBuilder sbuilder = new StringBuilder(baseString);

            if (m_InnerExceptions.Any())
            {
                int exceptionCounter = 0;

                sbuilder.AppendLine();
                sbuilder.AppendLine("List of InnerExceptions:");

                foreach (Exception ex in m_InnerExceptions)
                {
                    exceptionCounter++;
                    sbuilder.AppendLine(string.Format("Exception {0}:", exceptionCounter));
                    sbuilder.Append(" ---> ");
                    sbuilder.AppendLine(ex.InnerException.ToString());
                }

                sbuilder.Append("End of InnerExceptions list");
            }

            return sbuilder.ToString();
        }
    }
}
