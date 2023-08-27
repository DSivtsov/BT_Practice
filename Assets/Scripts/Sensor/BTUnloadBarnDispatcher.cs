using System;
using System.Collections;
using Sample;
using UnityEngine;

namespace Lessons.AI.LessonBehaviourTree
{
    public class BTUnloadBarnDispatcher : MonoBehaviour
    {
        [SerializeField] private float _barnCheckPeriod = 2f;
        [SerializeField] private Barn _barn;

        private Coroutine _coroutineWaitUnload;

        public event Action OnRestartUnload;
        
        private static BTUnloadBarnDispatcher _instance;
        public static BTUnloadBarnDispatcher Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<BTUnloadBarnDispatcher>();
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
        }

        public void StartWaitUnload()
        {
            if (_coroutineWaitUnload == null)
                _coroutineWaitUnload = StartCoroutine(WaitUnloadBarn());
        }

        //YAGNI Current realization of Barn give only a possibility to empty Barn fully therefore other variants will not track
        //and all characters can be unloaded per one time
        private IEnumerator WaitUnloadBarn()
        {
            do
                yield return new WaitForSeconds(_barnCheckPeriod);
            while (_barn.IsFull());

            RestartUnloadUnitFromQueue();
        }

        private void RestartUnloadUnitFromQueue()
        {
            StopCoroutine(_coroutineWaitUnload);
            _coroutineWaitUnload = null;
            OnRestartUnload?.Invoke();
        }
    }
}