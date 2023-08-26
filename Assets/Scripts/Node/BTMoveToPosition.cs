using System.Collections;

using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTMoveToPosition : BehaviourNode
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

            _coroutine = StartCoroutine(MoveToPosition(unit));
        }

        protected override void OnDispose()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator MoveToPosition(Character unit)
        {
            var period = new WaitForFixedUpdate();
            float stoppingDistance = _blackboard.GetVariable<float>(BlackboardKeys.STOPPING_DISTANCE);
            
            while (true)
            {
                //var targetPosition = _blackboard.GetVariable<Vector3>(BlackboardKeys.MOVE_POSITION);
                if (!_blackboard.TryGetVariable(BlackboardKeys.MOVE_POSITION, out Vector3 targetPosition))
                {
                    Return(false);
                    yield break;
                }
                
                var distanceVector = targetPosition - unit.transform.position;
                
                if (distanceVector.magnitude <= stoppingDistance)
                {
                    break;
                }
        
                var direction = distanceVector.normalized;
                unit.Move(direction);
        
                yield return period;
            }
        
            yield return period; //Костыль...
            
            _coroutine = null;
            Return(true);
        }
    }
}