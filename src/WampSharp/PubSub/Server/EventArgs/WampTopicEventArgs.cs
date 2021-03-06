﻿using System;

namespace WampSharp.PubSub.Server
{
    /// <summary>
    /// Represents <see cref="EventArgs"/> for <see cref="IWampTopic"/>
    /// creation/destruction events.
    /// </summary>
    public class WampTopicEventArgs : EventArgs
    {
        private readonly IWampTopic mTopic;

        /// <summary>
        /// Creates a new instance of <see cref="WampTopicEventArgs"/>.
        /// </summary>
        /// <param name="topic">The relevant topic.</param>
        public WampTopicEventArgs(IWampTopic topic)
        {
            mTopic = topic;
        }

        /// <summary>
        /// Gets the relevant topic.
        /// </summary>
        public IWampTopic Topic
        {
            get
            {
                return mTopic;
            }
        }
    }
}