using System.Collections.Generic;
using System.Threading.Tasks;
using WampSharp.V2.Rpc;

namespace WampSharp.V2.Client
{
    public interface IWampRpcOperationRegistrationProxy
    {
        Task Register(IWampRpcOperation operation, IDictionary<string, object> options);

        Task Unregister(IWampRpcOperation operation);         
    }
}