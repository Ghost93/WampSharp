using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WampSharp.V2.PubSub;

namespace WampSharp.V2.Client
{
    public interface IWampTopicProxy : IDisposable
    {
        string TopicUri { get; }

        Task<long> Publish(IDictionary<string, object> options);
        Task<long> Publish(IDictionary<string, object> options, object[] arguments);
        Task<long> Publish(IDictionary<string, object> options, object[] arguments, IDictionary<string, object> argumentKeywords);

        Task<IDisposable> Subscribe(IWampTopicSubscriber subscriber, IDictionary<string, object> options);
        Task<IDisposable> Subscribe(IWampRawTopicSubscriber subscriber, IDictionary<string, object> options);
    }
}