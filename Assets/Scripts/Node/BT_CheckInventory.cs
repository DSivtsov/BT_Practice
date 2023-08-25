using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BT_CheckInventory : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        protected override void Run()
        {
             if (!this.blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 this.Return(false);
                 return;
             }

             if (unit.IsResourceBagFull())
             {
                 this.Return(true);
                 return;
             }
             
             this.Return(false);
        }
    }
}