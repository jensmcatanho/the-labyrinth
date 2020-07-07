using System.Collections;
using System.Collections.Generic;
using Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {

    public class FinishTriggerTests {
        
        [Test]
        public void Test_Spawn_When_Position_X_Is_Greater_Than_Position_Y() {
            var finishCell = new Maze.Cell(11, 10, 2);
            var expectedPosition = new Vector3(48.0f, 1.0f, 42.0f);
            var expectedLocalScale = new Vector3(1.0f, 4.0f, 3.0f);

            var finishTrigger = FinishTrigger.Spawn(finishCell);

            Assert.AreEqual(expectedPosition, finishTrigger.transform.position);
            Assert.AreEqual(expectedLocalScale, finishTrigger.transform.localScale);
        }
        
        [Test]
        public void Test_Spawn_When_Position_X_Is_Lower_Than_Position_Y() {
            var finishCell = new Maze.Cell(9, 10, 2);
            var expectedPosition = new Vector3(88.0f, 1.0f, 40.0f);
            var expectedLocalScale = new Vector3(3.0f, 4.0f, 1.0f);

            var finishTrigger = FinishTrigger.Spawn(finishCell);

            Assert.AreEqual(expectedPosition, finishTrigger.transform.position);
            Assert.AreEqual(expectedLocalScale, finishTrigger.transform.localScale);
        }
        
        [Test]
        public void Test_Spawn_When_Position_X_And_Position_Y_Are_Equal() {
            var finishCell = new Maze.Cell(10, 10, 2);
            var expectedPosition = new Vector3(96.0f, 1.0f, 40.0f);
            var expectedLocalScale = new Vector3(3.0f, 4.0f, 1.0f);

            var finishTrigger = FinishTrigger.Spawn(finishCell);

            Assert.AreEqual(expectedPosition, finishTrigger.transform.position);
            Assert.AreEqual(expectedLocalScale, finishTrigger.transform.localScale);
        }

        [UnityTest]
        public IEnumerator Test_OnTriggerEnter_When_A_Collider_Enters() {
            var finishCell = new Maze.Cell(10, 10, 2);
            var finishTrigger = FinishTrigger.Spawn(finishCell);
            var isMazeFinished = false;

            SetUpEventManager();
            EventManager.Instance.AddListenerOnce((Events.MazeFinished e) => {
                isMazeFinished = true;
            });

            SetUpColliderObject(finishTrigger.transform.position);
            yield return null;

            Assert.IsTrue(isMazeFinished);
        }


        [UnityTest]
        public IEnumerator Test_OnTriggerEnter_When_No_Collision_Is_Happening() {
            var finishCell = new Maze.Cell(10, 10, 2);
            var finishTrigger = FinishTrigger.Spawn(finishCell);
            var isMazeFinished = false;

            SetUpEventManager();
            EventManager.Instance.AddListenerOnce((Events.MazeFinished e) => {
                isMazeFinished = true;
            });

            SetUpColliderObject(new Vector3(100, 100, 100));
            yield return null;

            Assert.IsFalse(isMazeFinished);
        }

        public void SetUpColliderObject(Vector3 position) {
            var collider = new GameObject("Collider");
            collider.AddComponent<BoxCollider>();
            collider.AddComponent<Rigidbody>();
            collider.transform.position = position;
        }

        public void SetUpEventManager() {
            var eventManager = new GameObject();
            eventManager.AddComponent<EventManager>();
        }
    }
}
