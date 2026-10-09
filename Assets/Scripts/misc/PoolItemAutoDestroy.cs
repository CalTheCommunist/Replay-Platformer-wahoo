using UnityEngine;

namespace SimplePool
{
    /// <summary>Moves a pooled item without physics and returns it after a set lifetime.</summary>
    /// <remarks>The historical class name says Destroy, but instances are returned and reused.</remarks>
    public class PoolItemAutoDestroy : PoolItem
    {
        [Tooltip("Movement speed in units per second.")]
        [SerializeField] private float m_speed = 10.0f;
        [Tooltip("Lifetime in seconds after every spawn.")]
        [SerializeField] private float m_lifetime = 3.0f;
        private float m_returnTime;

        /// <summary>Starts a fresh lifetime each time the same object is borrowed.</summary>
        protected override void Activate()
        {
            m_returnTime = Time.time + m_lifetime;
        }

        /// <summary>Moves along local forward and returns the item when its lifetime expires.</summary>
        private void Update()
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * m_speed));
            if (Time.time >= m_returnTime) 
                ReturnToPool();
        }
    }
}
