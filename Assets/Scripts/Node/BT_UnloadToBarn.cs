using System.Collections;

using Lessons.AI.HierarchicalStateMachine;
using Sample;
using Unity.VisualScripting;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BT_UnloadToBarn : BehaviourNode
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
             
             if (!this.blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 this.Return(false);
                 return;
             }
             
             if (barn.CanAddResources(unit.ResourceAmount))
             {
                 barn.AddResources(unit.UnloadResources());
                 this.Return(true);
                 return;
             }
             
             this.Return(false);
        }
    }
}