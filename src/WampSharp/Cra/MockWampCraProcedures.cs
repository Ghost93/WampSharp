﻿using System.Collections.Generic;

namespace WampSharp.Cra
{
    /// <summary>
    /// Mocked IWampCraProcedures for use in registering with the RpcMetadataCatalog.
    /// </summary>
    /// <seealso cref="T:WampSharp.Cra.IWampCraProcedures"/>
    internal class MockWampCraProcedures : IWampCraProcedures
    {
        public string AuthReq(string authKey, IDictionary<string, string> extra)
        {
            throw new System.NotImplementedException();
        }

        public WampCraPermissions Auth(string signature)
        {
            throw new System.NotImplementedException();
        }
    }
}