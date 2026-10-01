using UnityEngine;

namespace SimplePool
{
    public class EnemyFire : MonoBehaviour
    {
        [SerializeField] private ObjectPool m_bulletPool;
        private RangedEnemyScript mainscript;
        private Quaternion fireRotation;
        [SerializeField] private Transform FirePoint;
        private bool hasFired;
        private float CD = 1f;
        private float timer;
        private void Awake()
        {
            mainscript = GetComponent<RangedEnemyScript>();
        }

        private void Update()
        {
            fireRotation = Quaternion.LookRotation(mainscript.directionToPlayer);
            if (mainscript.CurrentState == RangedEnemyScript.States.Attack)
            {
                if (hasFired == false)
                {
                    Fire();
                hasFired = true;
                    
                }
            }
            if (hasFired == true)
            {
                timer = timer + Time.deltaTime;
                if (timer >= CD)
                {
                    hasFired = false;
                    timer = 0;

                }

            }
        }

        public void Fire()
        {
            Vector3 firingLocation = FirePoint.position;

          
                m_bulletPool.GetPooledObject(firingLocation, fireRotation);
            
            
        }
    }
}