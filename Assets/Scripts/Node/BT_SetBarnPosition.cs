using System.Collections;

using Lessons.AI.HierarchicalStateMachine;
using Sample;
using Unity.VisualScripting;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BT_SetBarnPosition : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;
        
        protected override void Run()
        {
            if (!this.blackboard.TryGetVariable(BlackboardKeys.BARN, out Barn barn))
            {
                this.Return(false);
                return;
            }

            Vector3 targetPosition = barn.transform.position;
            this.blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, targetPosition);
            this.Return(true);
        }
    }
}