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

        private Coroutine coroutine;
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
             this.coroutine = StartCoroutine(GetResource(tree, unit));
             
        }

        protected override void OnDispose()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }
        
        private IEnumerator GetResource(Tree tree, Character unit)
        {
            do
            {
                if (!tree.HasResources())
                {
                    this.Return(false);
                    yield break;
                }
            
                unit.Chop(tree);
                yield return new WaitForSeconds(1f);
                
            } while (!unit.IsResourceBagFull());
            
            this.Return(true);
        }
    }
}