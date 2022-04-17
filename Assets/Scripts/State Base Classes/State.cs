using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amirul.AI.StateMachine
{
    public abstract class State
    {
        public virtual void Enter()
        {

        }

        /*public virtual void HandleInput()
        {

        }*/

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
