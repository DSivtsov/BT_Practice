using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTRestartGatherOnForestChanged : MonoBehaviour
    {
        [SerializeField] private BehaviourNodeSequenceRestartable _node;
        
        private IForest _iForest;

        private void Awake()
        {
            _iForest = BTSensorForest.Instance;
        }

        private void OnEnable()
        {
            _iForest.OnForestChanged += RestartNode;
        }

        private void OnDisable()
        {
            _iForest.OnForestChanged -= RestartNode;
        }
        
        [Button]
        private void RestartNode()
        {
            _node.Restart();
        }
    }
}