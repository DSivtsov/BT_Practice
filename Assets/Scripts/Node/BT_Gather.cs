using System.Collections;
using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;
using Tree = Sample.Tree;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BT_Gather : BehaviourNode
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
             
             if (!this.blackboard.TryGetVariable(BlackboardKeys.NEAREST_TREE, out Tree tree))
             {
                 this.Return(false);
                 return;
             }

             if (!tree.HasResources())
             {
                 this.Return(false);
                 return;
             }

             StartCoroutine(GetResource(tree, unit));
             
        }

        private IEnumerator GetResource(Tree tree, Character unit)
        {
            
            unit.Chop(tree);
            yield return new WaitForSeconds(1f);
            
            this.Return(true);
        }
    }
}