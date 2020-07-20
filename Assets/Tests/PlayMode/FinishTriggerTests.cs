using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Labyrinth.Tests {

    public class FinishTriggerTests {
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

        [Test]
        public void Test_Spawn_When_Position_X_Is_Greater_Than_Position_Y() {
            var finishCell = new Maze.Cell(11, 10, 2);
            var expectedPosition = new Vector3(48.0f, 1.0f, 42.0f);
            var expectedLocalScale = new Vector3(1.0f, 4.0f, 3.0f);

            var finishTrigger = FinishTrigger.Spawn(finishCell);

            Assert.AreEqual(expectedPosition, finishTrigger.transform.position);
            Assert.AreEqual(expectedLocalScale, finishTrigger.transform.localScale);
            _garbageCollector.Enqueue(finishTrigger);
        }

        [Test]
        public void Test_Spawn_When_Position_X_Is_Lower_Than_Position_Y() {
            var finishCell = new Maze.Cell(9, 10, 2);
            var expectedPosition = new Vector3(88.0f, 1.0f, 40.0f);
            var expectedLocalScale = new Vector3(3.0f, 4.0f, 1.0f);

            var finishTrigger = FinishTrigger.Spawn(finishCell);

            Assert.AreEqual(expectedPosition, finishTrigger.transform.position);
            Assert.AreEqual(expectedLocalScale, finishTrigger.transform.localScale);
            _garbageCollector.Enqueue(finishTrigger);
        }

        [Test]
        public void Test_Spawn_When_Position_X_And_Position_Y_Are_Equal() {
            var finishCell = new Maze.Cell(10, 10, 2);
            var expectedPosition = new Vector3(96.0f, 1.0f, 40.0f);
            var expectedLocalScale = new Vector3(3.0f, 4.0f, 1.0f);

            var finishTrigger = FinishTrigger.Spawn(finishCell);

            Assert.AreEqual(expectedPosition, finishTrigger.transform.position);
            Assert.AreEqual(expectedLocalScale, finishTrigger.transform.localScale);
            _garbageCollector.Enqueue(finishTrigger);
        }

        [UnityTest]
        public IEnumerator Test_OnTriggerEnter_When_A_Collider_Enters() {
            var finishCell = new Maze.Cell(10, 10, 2);
            var finishTrigger = FinishTrigger.Spawn(finishCell);
            var isMazeFinished = false;

            Core.EventManager.Instance.AddListenerOnce((Events.MazeFinished e) => {
                isMazeFinished = true;
            });

            var collider = SetUpColliderObject(finishTrigger.transform.position);
            yield return new WaitForSeconds(.5f);

            Assert.IsTrue(isMazeFinished);
            _garbageCollector.Enqueue(finishTrigger, collider);
        }


        [UnityTest]
        public IEnumerator Test_OnTriggerEnter_When_No_Collision_Is_Happening() {
            var finishCell = new Maze.Cell(10, 10, 2);
            var finishTrigger = FinishTrigger.Spawn(finishCell);
            var isMazeFinished = false;

            Core.EventManager.Instance.AddListenerOnce((Events.MazeFinished e) => {
                isMazeFinished = true;
            });

            var collider = SetUpColliderObject(new Vector3(100, 100, 100));
            yield return new WaitForSeconds(.5f);

            Assert.IsFalse(isMazeFinished);
            _garbageCollector.Enqueue(finishTrigger, collider);
        }

        public GameObject SetUpColliderObject(Vector3 position) {
            var collider = new GameObject("Collider");
            collider.AddComponent<BoxCollider>();
            collider.AddComponent<Rigidbody>();
            collider.transform.position = position;

            return collider;
        }


    }
}
