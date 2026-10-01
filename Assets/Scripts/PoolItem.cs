using UnityEngine;

namespace SimplePool
{
    /// <summary>Base behaviour for a prefab that can be borrowed from an ObjectPool.</summary>
    /// <remarks>Override Activate and Deactivate to reset state on every use; Start only runs once.</remarks>
    public class PoolItem : MonoBehaviour
    {
        private IObjectPool m_myPool;

        /// <summary>Owning pool, assigned when the instance is created by ObjectPool.</summary>
        public IObjectPool Pool { set { m_myPool = value; } }

        /// <summary>Called after SetActive(true) on every borrow; override to reset timers and other state.</summary>
        protected virtual void Activate() { }
        /// <summary>Called before return to the pool; override to clear velocity or other transient state.</summary>
        protected virtual void Deactivate() { }

        /// <summary>Places and activates a previously prepared instance.</summary>
        /// <param name="position">World position at spawn.</param>
        /// <param name="rotation">World rotation at spawn.</param>
        /// <param name="parent">Parent while active.</param>
        public void Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            transform.SetPositionAndRotation(position, rotation);
            transform.SetParent(parent, true);
            gameObject.SetActive(true);
            Activate();
        }

        /// <summary>Resets subclass state and returns this instance to its owner.</summary>
        /// <remarks>Only call on instances obtained from a pool; a manually instantiated prefab has no owner.</remarks>
        public void ReturnToPool()
        {
            Deactivate();
            m_myPool.ReturnPooledObject(this);
        }

        /// <summary>Returns a pooled item or destroys a regular GameObject.</summary>
        /// <param name="gameObject">Object to dispose of.</param>
        /// <param name="poolingEnabled">Whether to try its PoolItem component first.</param>
        /// <remarks>Use this only for gameplay that deliberately supports both lifecycles.</remarks>
        public static void ReturnToPoolOrDestroy(GameObject gameObject, bool poolingEnabled = true)
        {
            if (poolingEnabled && gameObject.TryGetComponent(out PoolItem item))
            {
                item.ReturnToPool();
                return;
            }
            Destroy(gameObject);
        }
    }
}
