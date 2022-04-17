using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amirul.AI.StateMachine
{
    public class StateMachine
    {
        // The state in which the AI is currently in.
        public State CurrentState { get; private set; }

        // Initialize the starting state of the AI.
        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        // Change the state of the AI when needed.
        public void ChangeState(State newState)
        {
            if (newState == CurrentState) return;

            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}
