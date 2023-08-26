using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tree = Sample.Tree;

namespace Lessons.AI.LessonBehaviourTree
{
    public interface IForest
    {
        (Tree obj, Vector3 position) GetNearestTree(Vector3 unitPosition);

        event Action OnForestChanged;

    }
    public class BTSensorForest : MonoBehaviour, IForest
    {
        [SerializeField] private Transform parentForest;

        private Dictionary<Tree, Vector3> _forest;
        private HashSet<Tree> _activeTrees;
        private static BTSensorForest _instance;
        private Tree _nearestTree;
        private Vector3 _treePosition;

        public static BTSensorForest Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<BTSensorForest>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                if (_instance != this)
                    throw new NotImplementedException("[BT_SensorForest]: Singleton init error");

            GetForestAndActiveTrees();
        }

        private void GetForestAndActiveTrees()
        {
            _forest = parentForest
                .Cast<Transform>()
                .ToDictionary((tree) => tree.GetComponent<Tree>(),(tree) => tree.position);

            _activeTrees = parentForest.Cast<Transform>().Select((transformTree) => transformTree.GetComponent<Tree>()).ToHashSet();
        }

        public bool GetNearestTree(Vector3 unitPosition)
        {
            float minDistance = float.MaxValue;
            _nearestTree = null;
            
            foreach (Tree tree in _activeTrees)
            {
                Vector3 treePosition = _forest[tree];
                Vector3 direction = treePosition - unitPosition;
                float distance = direction.sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    _nearestTree = tree;
                }
            }

            if (_nearestTree == null) return false;
            
            _treePosition = _forest[_nearestTree];
            return true;
        }

        public void TreeDeactivated(Tree tree)
        {
            _activeTrees.Remove(tree);
            
            OnForestChanged?.Invoke();
        }

        private event Action OnForestChanged;

        event Action IForest.OnForestChanged
        {
            add => OnForestChanged += value;
            remove => OnForestChanged -= value;
        }

        (Tree obj, Vector3 position) IForest.GetNearestTree(Vector3 unitPosition)
        {
            return GetNearestTree(unitPosition) ? (_nearestTree, _treePosition) : (null, Vector3.zero);
        }
    }
}