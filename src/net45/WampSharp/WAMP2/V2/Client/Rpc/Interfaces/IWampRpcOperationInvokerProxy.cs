using System.Collections.Generic;
using WampSharp.V2.Rpc;

namespace WampSharp.V2.Client
{
    public interface IWampRpcOperationInvokerProxy
    {
        void Invoke(IWampRpcOperationCallback caller, IDictionary<string, object> options, string procedure);

        void Invoke(IWampRpcOperationCallback caller, IDictionary<string, object> options, string procedure, object[] arguments);

        void Invoke(IWampRpcOperationCallback caller, IDictionary<string, object> options, string procedure, object[] arguments, IDictionary<string, object> argumentsKeywords);

        void Invoke(IWampRawRpcOperationCallback caller, IDictionary<string, object> options, string procedure);

        void Invoke(IWampRawRpcOperationCallback caller, IDictionary<string, object> options, string procedure, object[] arguments);

        void Invoke(IWampRawRpcOperationCallback caller, IDictionary<string, object> options, string procedure, object[] arguments, IDictionary<string, object> argumentsKeywords);             
    }
}