using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Utility
{
    public class ObjectPool<T> where T : Object
    {
        private readonly List<Action<T>> _actionsOnInstantiate;
        private readonly Queue<T> _available;
        private readonly List<T> _prefabs;
        private readonly List<T> _totalSpawned = new List<T>();

        public ObjectPool(List<T> prefabs, List<Action<T>> actionsOnInstantiate = null)
        {
            _prefabs = prefabs;
            _actionsOnInstantiate = actionsOnInstantiate;

            _available = new Queue<T>(prefabs.Count);
            for (int i = 0; i < prefabs.Count; i++)
                CreateNew();
        }

        public ObjectPool(List<T> prefabs, int initialSize, List<Action<T>> actionsOnInstantiate = null)
        {
            _prefabs = prefabs;
            _actionsOnInstantiate = actionsOnInstantiate;

            _available = new Queue<T>(initialSize);
            for (int i = 0; i < initialSize; i++)
                CreateNew();
        }

        private void CreateNew()
        {
            int index = Random.Range(0, _prefabs.Count);
            var obj = Object.Instantiate(_prefabs[index]);
            _actionsOnInstantiate?.ForEach(x => x.Invoke(obj));

            _available.Enqueue(obj);
            _totalSpawned.Add(obj);
        }

        ~ObjectPool()
        {
            Clear();
        }

        public int Count => _available.Count;

        public T Get()
        {
            if (_available.Count == 0)
                CreateNew();

            return _available.Dequeue();
        }

        public void Return(T value)
        {
            _available.Enqueue(value);
        }

        public void ForEach(Action<T> action)
        {
            foreach (var o in _available)
            {
                action.Invoke(o);
            }
        }

        public void Clear()
        {
            foreach (var item in _totalSpawned)
                if (item != null)
                    Object.Destroy(item);
            _totalSpawned.Clear();
            _available.Clear();
        }
    }
}
