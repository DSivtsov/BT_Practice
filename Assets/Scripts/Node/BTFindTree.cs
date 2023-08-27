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
        private Blackboard _blackboard;

        private Transform[] _trees;

        private IForest _iForest;
        private void Awake()
        {
            _iForest = BTSensorForest.Instance;
        }
        
        protected override void Run()
        {
             if (!_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 Return(false);
                 return;
             }
             
             if (!_blackboard.TryGetVariable(BlackboardKeys.STOPPING_DISTANCE_TREE, out float stoppingDistance))
             {
                 Return(false);
                 return;
             }
             
             (Tree nearestTree, Vector3 treePosition) = _iForest.GetNearestTree(unit.transform.position);
             
             if (nearestTree != null)
             {
                 _blackboard.SetVariable(BlackboardKeys.NEAREST_TREE, nearestTree);
                 _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, treePosition);
                 _blackboard.SetVariable(BlackboardKeys.STOPPING_DISTANCE, stoppingDistance);
                 Return(true);
             }
             else
                Return(false);
        }
    }
}