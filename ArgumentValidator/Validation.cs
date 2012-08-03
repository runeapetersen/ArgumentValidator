using System;
using System.Collections.Generic;

namespace ArgumentValidator
{
    /// <summary>
    /// This class is used to perform parameter validation
    /// </summary>
    public sealed class Validation
    {
        private List<Exception> m_Exceptions;

        /// <summary>
        /// .ctor
        /// </summary>
        public Validation()
        {
            this.m_Exceptions = new List<Exception>(1); // optimize for only having 1 exception
        }

        /// <summary>
        /// Offers a reference to a collection of internally held references of System.Exception
        /// </summary>
        public IEnumerable<Exception> Exceptions
        {
            get
            {
                return this.m_Exceptions.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds an instance of System.Exception to this type
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        internal Validation AddException(Exception ex)
        {
            lock (this.m_Exceptions)
            {
                this.m_Exceptions.Add(ex);
            }

            return this;
        }
    }
}
