using UnityEngine;

namespace Fabrics
{
    public class AbstractFabric<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;

        public T Produce(Transform parent)
        {
            var obj = Instantiate(prefab, parent);
            return obj;
        }
    }
}