using ProtoBuf;

namespace Snaker2.GlobalData.Data
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