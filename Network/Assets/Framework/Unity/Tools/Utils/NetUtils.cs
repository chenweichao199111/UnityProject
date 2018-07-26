/*
 * 描述：
 * 作者：slicol
*/

using UnityEngine;

namespace Framework.Unity.Tools
{
    public class NetUtils
    {
        public static bool IsWifi()
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }


        public static bool IsAvailable()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }


        public static string SelfIP
        {
            get { return Network.player.ipAddress; }
        }

    }
}
