using ProtoBuf;

namespace Snaker.GlobalData.Proto
{
    [ProtoContract]
    public class LoginReq
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public string name;
    }

    [ProtoContract]
    public class LoginRsp
    {
        [ProtoMember(1)]
        public int ret;
        [ProtoMember(2)]
        public string msg;
    }


}