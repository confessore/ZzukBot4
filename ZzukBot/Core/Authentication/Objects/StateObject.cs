using System.Net.Security;
using System.Text;

namespace ZzukBot.Core.Authentication.Objects
{
    internal class StateObject
    { 
        internal SslStream workStream = null;
        internal const int BufferSize = 256;
        internal byte[] buffer = new byte[BufferSize];
        internal StringBuilder sb = new StringBuilder();
    }
}
