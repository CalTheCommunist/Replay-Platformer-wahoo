using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    /// <summary>Prepares a fixed initial batch of GameObjects and lends them out on demand.</summary>
    /// <remarks>Assign a prefab with a PoolItem-derived component before entering Play mode. This teaching implementation is not thread-safe.</remarks>
    public class ObjectPool : MonoBehaviour, IObjectPool
    {
        [Tooltip("Prefab with a PoolItem-derived component on its root.")]
        [SerializeField] private GameObject m_pooledObject;
        [Tooltip("Initial number of items created in Awake.")]
        [SerializeField] private int m_poolSize = 5;
        [Tooltip("Create a new batch if there are no inactive items left.")]
        [SerializeField] private bool m_autoExpand = false;
        [Tooltip("Number of items created per expansion.")]
        [SerializeField] private int m_expansionSize = 5;

        /// <summary>Prefab used by this pool; assign before Awake.</summary>
        public GameObject PooledObject { get { return m_pooledObject; } set { m_pooledObject = value; } }
        /// <summary>Initial batch size; assign before Awake.</summary>
        public int PoolSize { get { return m_poolSize; } set { m_poolSize = value; } }
        /// <summary>Enables expansion when all existing items are active.</summary>
        public bool AutoExpand { get { return m_autoExpand; } set { m_autoExpand = value; } }
        /// <summary>Number of new items in one expansion.</summary>
        public int ExpansionSize { get { return m_expansionSize; } set { m_expansionSize = value; } }

        private Stack<PoolItem> m_objectPool;
        private HashSet<PoolItem> m_itemsInPool;

        /// <summary>Validates configuration and creates the initial inactive batch before Start.</summary>
        private void Awake()
        {
            if (m_pooledObject == null || m_pooledObject.GetComponent<PoolItem>() == null)
            {
                Debug.LogError($"{name}: Pooled object is not a GameObject with a PoolItem component!");
                return;
            }
            if (m_poolSize <= 0 || (m_autoExpand && m_expansionSize <= 0))
            {
                Debug.LogError($"{name}: Pool Size and enabled Expansion Size must be positive!");
                return;
            }

            m_objectPool = new Stack<PoolItem>(m_poolSize);
            m_itemsInPool = new HashSet<PoolItem>();
            Expand(m_poolSize);
        }

        /// <summary>Creates one batch, attaches each item to this pool and stores it inactive.</summary>
        /// <param name="amount">Positive number of items to instantiate.</param>
        private void Expand(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject instance = Instantiate(m_pooledObject);
                PoolItem item = instance.GetComponent<PoolItem>();
                item.Pool = this;
                ReturnPooledObject(item);
            }
        }

        /// <summary>Checks the count of inactive items available right now.</summary>
        /// <returns>True if no item is available, including when configuration prevented initialization.</returns>
        public bool PoolIsEmpty()
        {
            return m_objectPool == null || m_objectPool.Count == 0;
        }

        /// <summary>Reuses an inactive item or expands once before activating it.</summary>
        /// <param name="position">World position at spawn.</param>
        /// <param name="rotation">World rotation at spawn.</param>
        /// <param name="parent">Optional active parent; null keeps the item under the pool.</param>
        /// <returns>The active GameObject, or null when uninitialized or empty with Auto Expand off.</returns>
        public GameObject GetPooledObject(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (m_objectPool == null)
            {
                Debug.LogError($"{name}: Pool is not initialized yet.");
                return null;
            }
            if (m_objectPool.Count == 0)
            {
                if (m_autoExpand && m_expansionSize > 0) Expand(m_expansionSize);
                else
                {
                    Debug.LogError($"{name}: Pool is empty");
                    return null;
                }
            }

            PoolItem item = m_objectPool.Pop();
            m_itemsInPool.Remove(item);
            item.Spawn(position, rotation, parent != null ? parent : transform);
            return item.gameObject;
        }

        /// <summary>Returns a borrowed item to the inactive stack, ignoring repeated returns.</summary>
        /// <param name="item">The item to deactivate and store.</param>
        /// <remarks>Gameplay normally calls item.ReturnToPool() so Deactivate runs first.</remarks>
        public void ReturnPooledObject(PoolItem item)
        {
            if (item == null || m_objectPool == null || m_itemsInPool == null || m_itemsInPool.Contains(item)) return;
            item.transform.SetParent(transform);
            item.gameObject.SetActive(false);
            m_objectPool.Push(item);
            m_itemsInPool.Add(item);
        }
    }
}
