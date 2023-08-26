using UnityEngine;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTAborterOnForestChanged : MonoBehaviour
    {
        [SerializeField] private BehaviourNode _node;
        
        private IForest _iForest;

        private void Awake()
        {
            _iForest = BTSensorForest.Instance;
        }

        private void OnEnable()
        {
            _iForest.OnForestChanged += RestartGather;
        }

        private void OnDisable()
        {
            _iForest.OnForestChanged -= RestartGather;
        }
        
        private void RestartGather()
        {
            _node.Abort();
        }
    }
}