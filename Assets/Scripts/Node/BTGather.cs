using System.Collections;
using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;
using Tree = Sample.Tree;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTGather : BehaviourNode
    {
        [SerializeField]
        private Blackboard _blackboard;

        private Coroutine _coroutine;
        protected override void Run()
        {
             if (!_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 Return(false);
                 return;
             }
             
             if (!_blackboard.TryGetVariable(BlackboardKeys.NEAREST_TREE, out Tree tree))
             {
                 Return(false);
                 return;
             }
             _coroutine = StartCoroutine(GetResource(tree, unit));
             
        }

        protected override void OnDispose()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
        
        private IEnumerator GetResource(Tree tree, Character unit)
        {
            do
            {
                if (!tree.HasResources())
                {
                    Return(false);
                    yield break;
                }
            
                unit.Chop(tree);
                yield return new WaitForSeconds(1f);
                
            } while (!unit.IsResourceBagFull());
            
            Return(true);
        }
    }
}