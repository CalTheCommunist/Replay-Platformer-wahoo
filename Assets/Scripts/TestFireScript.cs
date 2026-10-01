using UnityEngine;

namespace SimplePool
{
    /// <summary>Small input driver for the SimplePool test scene.</summary>
    public class TestFireScript : MonoBehaviour
    {
        [Tooltip("Pool used by the test scene.")]
        [SerializeField] private ObjectPool m_bulletPool;

        /// <summary>Requests one object on Space; ObjectPool handles exhaustion and expansion.</summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
                m_bulletPool.GetPooledObject(transform.position, transform.rotation);
        }
    }
}
