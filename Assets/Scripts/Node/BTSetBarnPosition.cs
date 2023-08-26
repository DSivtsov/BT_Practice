using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTSetBarnPosition : BehaviourNode
    {
        [SerializeField]
        private Blackboard _blackboard;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable(BlackboardKeys.BARN, out Barn barn))
            {
                Return(false);
                return;
            }

            Vector3 targetPosition = barn.transform.position;
            _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, targetPosition);
            Return(true);
        }
    }
}