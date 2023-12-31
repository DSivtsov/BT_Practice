using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BTAborterOnForestChanged : MonoBehaviour
    {
        [SerializeField] private BehaviourNode _nodeRoot;
        
        private IForest _iForest;

        private void Awake()
        {
            _iForest = BTSensorForest.Instance;
        }

        private void OnEnable()
        {
            _iForest.OnForestChanged += RestartNodeRoot;
        }

        private void OnDisable()
        {
            _iForest.OnForestChanged -= RestartNodeRoot;
        }
        
        [Button]
        private void RestartNodeRoot()
        {
            _nodeRoot.Abort();
        }
    }
}