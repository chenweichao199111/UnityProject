using System.Collections.Generic;

namespace Framework.Pattern
{
    public class StateManager : Singleton<StateManager>
    {
        public State CurrentState
        {
            get;
            private set;
        }
        private List<State> mCacheStates = new List<State>();

        public void EnterState<T>() where T : State, new()
        {
            bool tempExistState = false;
            State tempState = null;
            string tempName = typeof(T).FullName;
            for (var i = 0; i < mCacheStates.Count; i++)
            {
                State tempState2 = mCacheStates[i];
                if (tempState2.GetType().FullName == tempName)
                {
                    tempExistState = true;
                    tempState = tempState2;
                    break;
                }
            }

            if (!tempExistState)
            {
                tempState = new T();
                mCacheStates.Add(tempState);
            }
            if (CurrentState != null)
            {
                CurrentState.Leave();
            }

            CurrentState = tempState;
            CurrentState.Enter();
        }

        public void RestartState()
        {
            if (CurrentState != null)
            {
                CurrentState.Restart();
            }
        }
    }
}
