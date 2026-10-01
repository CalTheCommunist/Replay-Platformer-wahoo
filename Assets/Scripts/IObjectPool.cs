using UnityEngine;

namespace SimplePool
{
    /// <summary>Contract for a pool that owns one prefab type and reuses its instances.</summary>
    /// <remarks>Free items are available for a later request; an active item returns through its PoolItem component.</remarks>
    public interface IObjectPool
    {
        /// <summary>Prefab whose root has a PoolItem-derived component. Set before Awake.</summary>
        GameObject PooledObject { get; set; }
        /// <summary>Number of objects created in Awake. Must be greater than zero.</summary>
        int PoolSize { get; set; }
        /// <summary>Whether an empty pool creates an additional batch on request.</summary>
        bool AutoExpand { get; set; }
        /// <summary>Number of objects in each additional batch. Must be positive when AutoExpand is enabled.</summary>
        int ExpansionSize { get; set; }

        /// <summary>Checks whether any inactive items are available for immediate reuse.</summary>
        /// <returns>True when the pool is uninitialized or has no free items; this does not count active items.</returns>
        bool PoolIsEmpty();

        /// <summary>Obtains one item and activates it at the requested pose.</summary>
        /// <param name="position">World position for the item.</param>
        /// <param name="rotation">World rotation for the item.</param>
        /// <param name="parent">Optional parent for the active item; null uses the pool transform.</param>
        /// <returns>An active GameObject, or null if initialization failed or the pool is empty and cannot expand.</returns>
        GameObject GetPooledObject(Vector3 position, Quaternion rotation, Transform parent = null);

        /// <summary>Deactivates and stores an item belonging to this pool.</summary>
        /// <param name="item">The item to return. Usually supplied by PoolItem.ReturnToPool.</param>
        void ReturnPooledObject(PoolItem item);
    }
}
