﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace WampSharp.Rpc.Client.Dynamic
{
    /// <summary>
    /// A dynamic rpc client. 
    /// Allows to call rpc methods without having a given interface.
    /// </summary>
    public class DynamicWampRpcClient : DynamicObject
    {
        private readonly IWampRpcClientHandler mClientHandler;
        private readonly IWampRpcSerializer mSerializer;

        /// <summary>
        /// Creates a new instance of <see cref="DynamicWampRpcClient"/>.
        /// </summary>
        /// <param name="clientHandler">The <see cref="IWampRpcClientHandler"/>
        /// that will deal rpc calls.</param>
        /// <param name="serializer">The <see cref="IWampRpcSerializer"/> that will serialize
        /// RPC calls.</param>
        public DynamicWampRpcClient(IWampRpcClientHandler clientHandler,
                                    IWampRpcSerializer serializer)
        {
            mClientHandler = clientHandler;
            mSerializer = serializer;
        }

        private IWampRpcClientHandler ClientHandler
        {
            get
            {
                return mClientHandler;
            }
        }

        private IWampRpcSerializer Serializer
        {
            get
            {
                return mSerializer;
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            string methodName = binder.Name;

            result = new DynamicWampCall(this, methodName, args);

            return true;
        }

        private class DynamicWampCall : DynamicObject
        {
            private readonly DynamicWampRpcClient mClient;
            private readonly string mMethodName;
            private readonly object[] mArguments;
            private Task<object> mTask;

            public DynamicWampCall(DynamicWampRpcClient client, string methodName, object[] arguments)
            {
                mClient = client;
                mMethodName = methodName;
                mArguments = arguments;
            }

            private object[] Arguments
            {
                get
                {
                    return mArguments;
                }
            }

            private string MethodName
            {
                get
                {
                    return mMethodName;
                }
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                CallRpcMethod(typeof(ExpandoObject));

                ExpandoObject expando = mTask.Result as ExpandoObject;

                if (expando != null)
                {
                    IDictionary<string, object> dictionary = expando;
                    return dictionary.TryGetValue(binder.Name, out result);
                }
                else if (binder.Name == "Result")
                {
                    result = mTask.Result;
                    return true;
                }

                return base.TryGetMember(binder, out result);
            }

            public override bool TryConvert(ConvertBinder binder, out object result)
            {
                Type returnType = binder.Type;

                WampRpcCall rpcCall = CallRpcMethod(returnType);

                if (!typeof (Task).IsAssignableFrom(returnType))
                {
                    result = mTask.Result;
                }
                else
                {
                    result = mTask.Cast(rpcCall.ReturnType);
                }

                return true;
            }

            private WampRpcCall CallRpcMethod(Type returnType)
            {
                WampRpcMethodInfo method = new WampRpcMethodInfo(MethodName, returnType);

                WampRpcCall rpcCall = mClient.Serializer.Serialize(method, Arguments);

                if (mTask == null)
                {
                    mTask = mClient.ClientHandler.HandleAsync(rpcCall);
                }

                return rpcCall;
            }
        }
    }
}