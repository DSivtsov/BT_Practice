﻿using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTUnloadToBarn : BehaviourNode
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
             
             if (!_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 Return(false);
                 return;
             }
             
             if (barn.CanAddResources(unit.ResourceAmount))
             {
                 barn.AddResources(unit.UnloadResources());
                 //False will move Character to next Node - Gather Resource
                 Return(false);
                 return;
             }
             //False will lead to Restart root Node and again Character will try to unload Resource
             Return(true);
        }
    }
}