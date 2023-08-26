using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTCheckInventory : BehaviourNode
    {
        [SerializeField]
        private Blackboard _blackboard;

        protected override void Run()
        {
             if (!_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 Return(false);
                 return;
             }

             if (unit.IsResourceBagFull())
             {
                 Return(true);
                 return;
             }
             
             Return(false);
        }
    }
}