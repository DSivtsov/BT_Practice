using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTUnloadToBarn : BehaviourNode
    {
        [SerializeField]
        private Blackboard _blackboard;

        private Character _unit;
        private BTUnloadBarnDispatcher _dispatcherBarn;
        private void Awake()
        {
            _dispatcherBarn = BTUnloadBarnDispatcher.Instance;
        }

        protected override void Run()
        {
             if (!_blackboard.TryGetVariable(BlackboardKeys.BARN, out Barn barn))
             {
                 Return(false);
                 return;
             }
             
             if (!_blackboard.TryGetVariable(BlackboardKeys.UNIT, out _unit))
             {
                 Return(false);
                 return;
             }

             if (barn.CanAddResources(_unit.ResourceAmount))
             {
                 barn.AddResources(_unit.UnloadResources());
                 Return(true);
                 return;
             }

             //will stay in current node util will be called from BTUnloadBarnDispatcher
             _dispatcherBarn.OnRestartUnload += RestartNode;
             _dispatcherBarn.StartWaitUnload();
        }
        
        private void RestartNode()
        {
            _dispatcherBarn.OnRestartUnload -= RestartNode;
            Run();
        }

        private void OnApplicationQuit()
        {
            _dispatcherBarn.OnRestartUnload -= RestartNode;
        }
    }
}