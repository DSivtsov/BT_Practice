using System.Collections;

using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BT_MoveToPosition : BehaviourNode
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
            
            if (!this.blackboard.TryGetVariable(BlackboardKeys.MOVE_POSITION, out Vector3 targetPoint))
            {
                this.Return(false);
                return;
            }

            this.coroutine = this.StartCoroutine(this.MoveToPosition(unit));
        }

        protected override void OnDispose()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        private IEnumerator MoveToPosition(Character unit)
        {
            var period = new WaitForFixedUpdate();
        
            while (true)
            {
                var stoppingDistance = this.blackboard.GetVariable<float>(BlackboardKeys.STOPPING_DISTANCE);
                var targetPosition = this.blackboard.GetVariable<Vector3>(BlackboardKeys.MOVE_POSITION);
                
                var distanceVector = targetPosition - unit.transform.position;
                var distance = distanceVector.magnitude;
                if (distance <= stoppingDistance)
                {
                    break;
                }
        
                var direction = distanceVector.normalized;
                unit.Move(direction);
        
                yield return period;
            }
        
            yield return period; //Костыль...
            
            this.coroutine = null;
            this.Return(true);
        }
    }
}