using ProtoBuf;

namespace Snaker2.GlobalData.Proto
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