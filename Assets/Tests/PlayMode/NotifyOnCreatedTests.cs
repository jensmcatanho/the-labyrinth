using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Maze;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {

    public class NotifyOnCreatedTests {

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
        public IEnumerator Test_Event_Should_Not_Be_Triggered_While_allowedFrames_Is_Not_Zero() {
            var gameObject = new GameObject();
            var child = new GameObject();
            var notifyOnCreated = gameObject.AddComponent<NotifyOnCreated>();
            child.transform.parent = gameObject.transform;
            var mazeInstancedTriggered = false;

            var serializedObject = new SerializedObject(notifyOnCreated);
            serializedObject.FindProperty("_allowedFrames").intValue = 30;
            serializedObject.ApplyModifiedProperties();

            Core.EventManager.Instance.AddListenerOnce((Events.MazeFinished e) => {
                mazeInstancedTriggered = true;
            });

            yield return null;
            Assert.False(mazeInstancedTriggered);
            _garbageCollector.Enqueue(gameObject, child);
        }

        [UnityTest]
        public IEnumerator Test_Event_Should_Be_Triggered_When_allowedFrames_Reaches_Zero() {
            var gameObject = new GameObject();
            var child = new GameObject();
            var notifyOnCreated = gameObject.AddComponent<NotifyOnCreated>();
            child.transform.parent = gameObject.transform;
            var mazeInstancedTriggered = false;

            var serializedObject = new SerializedObject(notifyOnCreated);
            var allowedFrames = serializedObject.FindProperty("_allowedFrames");
            allowedFrames.intValue = 10;
            serializedObject.ApplyModifiedProperties();

            Core.EventManager.Instance.AddListenerOnce((Events.MazeInstanced e) => {
                mazeInstancedTriggered = true;
            });

            yield return new WaitUntil(() => mazeInstancedTriggered);
            Assert.True(mazeInstancedTriggered);
            _garbageCollector.Enqueue(gameObject, child);
        }
    }
}
