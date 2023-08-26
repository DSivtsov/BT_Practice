using System;
using UnityEngine;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BT_Aborter_OnForestChanged : MonoBehaviour
    {
        [SerializeField] private BehaviourNode node;
        
        private IForest iForest;

        private void Awake()
        {
            iForest = BT_SensorForest.Instance;
        }

        private void OnEnable()
        {
            iForest.OnForestChanged += RestartGather;
        }

        private void OnDisable()
        {
            iForest.OnForestChanged -= RestartGather;
        }
        
        private void RestartGather()
        {
            this.node.Abort();
        }
    }
}