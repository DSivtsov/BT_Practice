using Sample;
using UnityEngine;
using static Lessons.AI.HierarchicalStateMachine.BlackboardKeys;

namespace Lessons.AI.HierarchicalStateMachine
{
    [RequireComponent(typeof(Blackboard))]
    public sealed class BlackboardInstaller : MonoBehaviour
    {
        [SerializeField]
        private Character unit;
        
        [SerializeField]
        private Barn barn;

        private void Awake()
        {
            var blackboard = this.GetComponent<Blackboard>();
            blackboard.SetVariable(UNIT, this.unit);
            blackboard.SetVariable(BARN, this.barn);
            blackboard.SetVariable(STOPPING_DISTANCE, 0.25f);
        }
    }
}