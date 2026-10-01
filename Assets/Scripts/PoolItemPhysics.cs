using UnityEngine;

namespace SimplePool
{
    /// <summary>Fires a pooled 3D Rigidbody and returns it on collision or timeout.</summary>
    /// <remarks>Uses 3D Rigidbody and OnCollisionEnter; use a separate implementation for Rigidbody2D.</remarks>
    [RequireComponent(typeof(Rigidbody))]
    public class PoolItemPhysics : PoolItem
    {
        [Tooltip("Initial speed in metres per second.")]
        [SerializeField] private float m_speed = 10.0f;
        [Tooltip("Maximum lifetime in seconds per spawn.")]
        [SerializeField] private float m_lifetime = 3.0f;
        private float m_returnTime;
        private Rigidbody m_rigidbody;

        /// <summary>Starts a fresh lifetime and gives the Rigidbody forward velocity.</summary>
        protected override void Activate()
        {
            m_returnTime = Time.time + m_lifetime;
            if (m_rigidbody == null) 
                m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.linearVelocity = transform.forward * m_speed;
        }

        /// <summary>Clears linear and angular velocity before the body is stored inactive.</summary>
        protected override void Deactivate()
        {
            if (m_rigidbody == null) return;
            m_rigidbody.linearVelocity  = Vector3.zero;
            m_rigidbody.angularVelocity = Vector3.zero;
        }

        /// <summary>Returns the item if it has exceeded its current lifetime.</summary>
        private void Update()
        {
            if (Time.time >= m_returnTime) 
                ReturnToPool();
        }

        /// <summary>Returns the item when its 3D collider hits another collider.</summary>
        /// <param name="collision">Collision reported by Unity.</param>
        private void OnCollisionEnter(Collision collision)
        {
            ReturnToPool();
        }
    }
}
