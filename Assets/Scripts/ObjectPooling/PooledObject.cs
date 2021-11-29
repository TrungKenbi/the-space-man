using System;
using System.Collections;
using UnityEngine;

namespace ObjectPooling
{
    public class PooledObject : MonoBehaviour
    {
        [HideInInspector]
        public int id;
        public Action<PooledObject> Finished;

        // A component reference for fast access -- avoids calls to GetComponent<>().
        public Component behaviour;

        public T As<T>() where T : Component
        {
            return behaviour as T;
        }

        public void Finish()
        {
            if (Finished != null)
            {
                Finished(this);
            }
        }

        public void FinishDelayed(float time)
        {
            if (Finished != null)
            {
                StartCoroutine(FinishDelayedCoroutine(time));
            }
        }

        private IEnumerator FinishDelayedCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            if (Finished != null)
                Finished(this);
        }

        // Convenience method to call finish when particles finish.
        // Needs ParticleSystem stop action to be set to "Callback".
        private void OnParticleSystemStopped()
        {
            Finish();
        }
    }
}
