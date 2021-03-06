//-----------------------------------------------------------------------
// <copyright file="UpdateStatusCommand.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The command to update the user's status. (a.k.a. post a new tweet)</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer.Core;

    /// <summary>
    /// The command to update the user's status. (a.k.a. post a new tweet)
    /// </summary>
    [AuthorizedCommandAttribute]
#if !SILVERLIGHT
    [Serializable]
#endif
    internal sealed class UpdateStatusCommand : TwitterCommand<TwitterStatus>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStatusCommand"/> class.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        /// <param name="text">The status text.</param>
        /// <param name="optionalProperties">The optional properties.</param>
        public UpdateStatusCommand(OAuthTokens tokens, string text, StatusUpdateOptions optionalProperties)
            : base(HTTPVerb.POST, "statuses/update.json", tokens, optionalProperties)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException("tokens");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            this.Text = text;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <value>The status text.</value>
        public string Text { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            this.RequestParameters.Add("status", this.Text);

            StatusUpdateOptions options = this.OptionalProperties as StatusUpdateOptions;
            if (options != null)
            {
                NumberFormatInfo nfi = CultureInfo.InvariantCulture.NumberFormat;

                if (options.InReplyToStatusId > 0)
                    this.RequestParameters.Add("in_reply_to_status_id", options.InReplyToStatusId.ToString("#"));

                if (options.Latitude != 0)
                    this.RequestParameters.Add("lat", options.Latitude.ToString(nfi));

                if (options.Longitude != 0)
                    this.RequestParameters.Add("long", options.Longitude.ToString(nfi));

                if (!string.IsNullOrEmpty(options.PlaceId))
                    this.RequestParameters.Add("place_id", options.PlaceId);

                if (options.PlacePin)
                    this.RequestParameters.Add("display_coordinates", "true");

                if (options.WrapLinks)
                    this.RequestParameters.Add("wrap_links", "true");
            }
        }
    }
}
