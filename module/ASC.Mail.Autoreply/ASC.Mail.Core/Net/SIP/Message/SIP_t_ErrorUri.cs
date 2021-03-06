/*
 *
 * (c) Copyright Ascensio System Limited 2010-2021
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/


namespace ASC.Mail.Net.SIP.Message
{
    #region usings

    using System;
    using System.Text;

    #endregion

    /// <summary>
    /// Implements SIP "error-uri" value. Defined in RFC 3261.
    /// </summary>
    /// <remarks>
    /// <code>
    /// RFC 3261 Syntax:
    ///     error-uri = LAQUOT absoluteURI RAQUOT *( SEMI generic-param )
    /// </code>
    /// </remarks>
    public class SIP_t_ErrorUri : SIP_t_ValueWithParams
    {
        #region Members

        private string m_Uri = "";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets uri value.
        /// </summary>
        public string Uri
        {
            get { return m_Uri; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Property Uri value can't be null or empty !");
                }

                m_Uri = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses "error-uri" from specified value.
        /// </summary>
        /// <param name="value">SIP "error-uri" value.</param>
        /// <exception cref="ArgumentNullException">Raised when <b>value</b> is null.</exception>
        /// <exception cref="SIP_ParseException">Raised when invalid SIP message.</exception>
        public void Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Parse(new StringReader(value));
        }

        /// <summary>
        /// Parses "error-uri" from specified reader.
        /// </summary>
        /// <param name="reader">Reader from where to parse.</param>
        /// <exception cref="ArgumentNullException">Raised when <b>reader</b> is null.</exception>
        /// <exception cref="SIP_ParseException">Raised when invalid SIP message.</exception>
        public override void Parse(StringReader reader)
        {
            /* 
                Error-Info = "Error-Info" HCOLON error-uri *(COMMA error-uri)
                error-uri  = LAQUOT absoluteURI RAQUOT *( SEMI generic-param )
            */

            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            // Parse uri
            // Read to LAQUOT
            reader.QuotedReadToDelimiter('<');
            if (!reader.StartsWith("<"))
            {
                throw new SIP_ParseException("Invalid 'error-uri' value, Uri not between <> !");
            }

            // Parse parameters
            ParseParameters(reader);
        }

        /// <summary>
        /// Converts this to valid "error-uri" value.
        /// </summary>
        /// <returns>Returns "error-uri" value.</returns>
        public override string ToStringValue()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("<" + m_Uri + ">");
            retVal.Append(ParametersToString());

            return retVal.ToString();
        }

        #endregion
    }
}