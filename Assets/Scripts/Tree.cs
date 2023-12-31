using Lessons.AI.LessonBehaviourTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Tree : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private int remainingResources = 10;

        private BTSensorForest forestSensor;
        private void Awake()
        {
            forestSensor = BTSensorForest.Instance;
        }

        public bool HasResources()
        {
            return this.remainingResources > 0;
        }

        [Button]
        public bool TakeResource()
        {
            if (this.remainingResources <= 0)
            {
                return false;
            }

            this.remainingResources--;

            if (this.remainingResources <= 0)
            {
                DeactivateTree();
            }
            else
            {
                this.animator.Play("Chop", -1, 0);
            }

            return true;
        }
        
        [Button]
        private void DeactivateTree()
        {
            this.forestSensor.TreeDeactivated(this);
            this.gameObject.SetActive(false);
        }
    }
}
