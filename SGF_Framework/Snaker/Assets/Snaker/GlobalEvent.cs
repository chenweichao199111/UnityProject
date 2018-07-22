using System;
using SGF.Event;

namespace Snaker
{

    /// <summary>
    /// 全局事件
    /// 有些事件不确定应该是由谁发出
    /// 就可以通过全局事件来收和发
    /// </summary>
    public static class GlobalEvent
    {
        public static SGFEvent onUpdate = new SGFEvent();
        public static SGFEvent onFixedUpdate = new SGFEvent();

        /// <summary>
        /// true:登录成功，false：登录失败，或者掉线
        /// </summary>
        public static SGFEvent<bool> onLogin = new SGFEvent<bool>();

        
    }
}