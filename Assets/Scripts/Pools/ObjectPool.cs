using System.Collections.Generic;
using Fabrics;
using UnityEngine;

namespace Pools
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Queue<T> _objects = new();
        private AbstractFabric<T> _fabric;
        private Transform _parent;
        private int _poolSize;
        private int _counter;

        public T Object { get; private set; }

        public ObjectPool(int size, AbstractFabric<T> fabric, Transform parent)
        {
            _poolSize = size;
            _fabric = fabric;
            _parent = parent;
        }
        
        public void Produce()
        {
            if (_counter > _poolSize)
            {
                ReuseObject();
            }
            else
            {
                _counter++;
                SpawnObject();
            }
        }

        private void SpawnObject()
        {
            var prefab = _fabric.Produce(_parent);
            _objects.Enqueue(prefab);

            Object = prefab;
        }
        
        private void ReuseObject()
        {
            var prefab = _objects.Dequeue();
            _objects.Enqueue(prefab);
            prefab.gameObject.SetActive(true);
            Transform objTransform;
            (objTransform = prefab.transform).SetParent(_parent);
            objTransform.position = Vector3.zero;
            Object = prefab;
        }
    }
}