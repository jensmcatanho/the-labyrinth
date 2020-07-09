using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {

    public class NotifyOnDestroyTests {

        private GarbageCollector _garbageCollector = null;

        [SetUp]
        public void SetUp() {
            var eventManager = new GameObject("Event Manager");
            eventManager.AddComponent<Core.EventManager>();

            _garbageCollector = new GarbageCollector();
            _garbageCollector.Enqueue(eventManager);
        }

        [TearDown]
        public void TearDown() {
            _garbageCollector.DestroyAll();
        }

        [UnityTest]
        public IEnumerator Test_NotifyOnDestroy() {
            var gameObject = new GameObject();
            var notifyOnDestroy = gameObject.AddComponent<NotifyOnDestroy>();
            notifyOnDestroy.AssetReference = null;
            _garbageCollector.Enqueue(gameObject);
            var objectDestroyed = false;

            EventManager.Instance.AddListener((Events.ObjectDestroyed e) => {
                objectDestroyed = true;
            });

            GameObject.Destroy(gameObject);
            yield return null;

            Assert.True(objectDestroyed);
        }
    }
}
