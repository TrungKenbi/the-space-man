﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Pool : Singleton<Pool>
    {
        [Serializable] // Let this appear in the inspector.
        public class ObjectPool
        {
            public int amount;
            public PooledObject objectToPool;
        }

        // List of objects to pool (only used for instantiation)
        public List<ObjectPool> objectPools;

        // Pool of objects, indexed by instance ID.
        // A queue works pretty naturally here.
        private Dictionary<int, Queue<PooledObject>> pool;

        protected override void Awake()
        {
            base.Awake();
            
            // Spawn all objects in provided pools.
            pool = new Dictionary<int, Queue<PooledObject>>();
            foreach (ObjectPool objPool in objectPools)
            {
                int amount = objPool.amount;
                PooledObject obj = objPool.objectToPool;

                // Saved prefabs have an instance id, which we can use to talk about the same prefab from other scripts.
                int id = obj.GetInstanceID();
                Queue<PooledObject> queue = new Queue<PooledObject>(amount);
                for (int i = 0; i < amount; i++)
                {
                    var clone = Instantiate(obj, transform);
                    clone.id = id;
                    clone.Finished += ReQueue;      // When `Finish()` is called, we put our object back in the queue.
                    clone.gameObject.SetActive(false);
                    queue.Enqueue(clone);
                }

                pool.Add(id, queue);
            }
        }

        private PooledObject GetNextObject(PooledObject obj)
        {
            // @NOTE: create a new queue if none exists, for pooling "unplanned" objects?
            var queue = pool[obj.GetInstanceID()];
            PooledObject clone = null;
            // If queue is empty (has been exhausted -- the pool size was too small), extend the queue by instantiating a new object,
            // and add it to the future queue.
            if (queue.Count == 0)
            {
                Debug.LogWarning("Object Pool queue was empty; wasn't able to get a new pooled object, so one will be instatiated.");
                clone = Instantiate(obj, transform);
                clone.id = obj.GetInstanceID();
                clone.Finished += ReQueue;
                clone.gameObject.SetActive(false);
            }
            else
            {
                clone = queue.Dequeue();
            }

            return clone;
        }

        // Gets an object from the pool and returns it after setting position, rotation, and active.
        public PooledObject Spawn(PooledObject obj, Vector3 position, Quaternion rotation)
        {
            var clone = GetNextObject(obj);
            clone.transform.position = position;
            clone.transform.rotation = rotation;
            clone.gameObject.SetActive(true);
            return clone;
        }

        private void ReQueue(PooledObject obj)
        {
            // Hide object and insert back in queue for reuse.
            obj.gameObject.SetActive(false);
            var queue = pool[obj.id];
            queue.Enqueue(obj);
        }
    }
}
