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


namespace ASC.Mail.Net.Dns.Client
{
    #region usings

    using System;

    #endregion

    /// <summary>
    /// PTR record class.
    /// </summary>
    [Serializable]
    public class DNS_rr_PTR : DNS_rr_base
    {
        #region Members

        private readonly string m_DomainName = "";

        #endregion

        #region Properties

        /// <summary>
        /// Gets domain name.
        /// </summary>
        public string DomainName
        {
            get { return m_DomainName; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="domainName">DomainName.</param>
        /// <param name="ttl">TTL value.</param>
        public DNS_rr_PTR(string domainName, int ttl) : base(QTYPE.PTR, ttl)
        {
            m_DomainName = domainName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses resource record from reply data.
        /// </summary>
        /// <param name="reply">DNS server reply data.</param>
        /// <param name="offset">Current offset in reply data.</param>
        /// <param name="rdLength">Resource record data length.</param>
        /// <param name="ttl">Time to live in seconds.</param>
        public static DNS_rr_PTR Parse(byte[] reply, ref int offset, int rdLength, int ttl)
        {
            string name = "";
            if (Dns_Client.GetQName(reply, ref offset, ref name))
            {
                return new DNS_rr_PTR(name, ttl);
            }
            else
            {
                throw new ArgumentException("Invalid PTR resource record data !");
            }
        }

        #endregion
    }
}