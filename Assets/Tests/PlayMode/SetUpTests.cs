using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class GarbageCollector {

        private readonly Queue<GameObject> _objects = new Queue<GameObject>();

        public void Enqueue(params GameObject[] objects) {
            foreach (var gameObject in objects)
                _objects.Enqueue(gameObject);
        }

        public void DestroyAll() {
            foreach (var gameObject in _objects) {
                Object.DestroyImmediate(gameObject);
            }
        }
    }
}
