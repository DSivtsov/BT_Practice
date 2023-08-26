using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;
using Tree = Sample.Tree;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTFindTree : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        private Transform[] _trees;

        private IForest _iForest;
        private void Awake()
        {
            _iForest = BTSensorForest.Instance;
        }
        
        protected override void Run()
        {
             if (!blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 Return(false);
                 return;
             }

             (Tree nearestTree, Vector3 treePosition) = _iForest.GetNearestTree(unit.transform.position);
             
             if (nearestTree != null)
             {
                 blackboard.SetVariable(BlackboardKeys.NEAREST_TREE, nearestTree);
                 blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, treePosition);
                 Return(true);
             }
             else
                Return(false);
        }
    }
}