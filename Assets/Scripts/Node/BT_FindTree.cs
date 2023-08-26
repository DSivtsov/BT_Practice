using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;
using Tree = Sample.Tree;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BT_FindTree : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private Transform parentForest;
        private Transform[] trees;

        protected override void Run()
        {
             if (!this.blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character unit))
             {
                 this.Return(false);
                 return;
             }

             //GetNearestTree(unit.transform.position);
             this.Return(BT_SensorForest.Instance.SetNearestTree(unit.transform.position, blackboard));
             return;
        }

        // private void GetNearestTree(Vector3 unitPosition)
        // {
        //     float minDistance = float.MaxValue;
        //     Transform nearestTransform = null;
        //     foreach (Transform tree in parentForest)
        //     {
        //         if (!tree.gameObject.activeSelf) continue;
        //         
        //         Vector3 direction = tree.position - unitPosition;
        //         float distance = direction.sqrMagnitude;
        //         if (distance < minDistance)
        //         {
        //             minDistance = distance;
        //             nearestTransform = tree;
        //         }
        //     }
        //
        //     if (nearestTransform == null)
        //     {
        //         this.Return(false);
        //         return;
        //     }
        //
        //     Tree nearestTree = nearestTransform.GetComponent<Tree>();
        //     
        //     this.blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, nearestTransform.position);
        //     this.blackboard.SetVariable(BlackboardKeys.NEAREST_TREE, nearestTree);
        //     
        //     this.Return(true);
        // }
    }
}