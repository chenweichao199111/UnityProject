namespace Framework.Pattern
{
    public abstract class State
    {
        public enum StateCode
        {
            None = 0,
            Begin,
            End
        }
        public StateCode pStateCode
        {
            get;
            set;
        }

        public abstract void Restart();  // 重新开始
        public abstract void Enter();  // 进入
        public abstract void Leave();  // 离开
    }
}