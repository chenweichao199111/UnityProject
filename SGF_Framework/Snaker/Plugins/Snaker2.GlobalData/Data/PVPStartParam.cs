using ProtoBuf;

namespace Snaker.GlobalData.Data
{
    [ProtoContract]
    public class PVPStartParam
    {
        [ProtoMember(1)]
        public int gameId;
        [ProtoMember(2)]
        public string roomName;
    }
}