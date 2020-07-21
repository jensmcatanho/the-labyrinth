using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Labyrinth.Tests;

namespace Labyrinth.Menu.UIElements.Tests {

    public class StartButtonTests {

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
        public IEnumerator Test_StartButton_Should_Queue_A_StartButtonClicked_Event_When_Clicked() {
            var gameObject = new GameObject("Start Button");
            var button = gameObject.AddComponent<Button>();
            gameObject.AddComponent<StartButton>();
            var isStartButtonClicked = false;

            Core.EventManager.Instance.AddListenerOnce((Events.Menu.StartButtonClicked e) => {
                isStartButtonClicked = true;
            });

            button.onClick.Invoke();
            yield return new WaitForSeconds(.5f);

            Assert.IsTrue(isStartButtonClicked);
            _garbageCollector.Enqueue(gameObject);
        }

        [UnityTest]
        public IEnumerator Test_StartButton_Should_Not_Queue_A_StartButtonClicked_Event_When_Its_Not_Clicked() {
            var gameObject = new GameObject("Start Button");
            var button = gameObject.AddComponent<Button>();
            gameObject.AddComponent<StartButton>();
            var isStartButtonClicked = false;

            Core.EventManager.Instance.AddListenerOnce((Events.Menu.StartButtonClicked e) => {
                isStartButtonClicked = true;
            });

            yield return new WaitForSeconds(.5f);

            Assert.IsFalse(isStartButtonClicked);
            _garbageCollector.Enqueue(gameObject);
        }


        [UnityTest]
        public IEnumerator Test_StartButton_RemoveListeners_Should_Remove_OnClick_Callback() {
            var gameObject = new GameObject("Start Button");
            var button = gameObject.AddComponent<Button>();
            var startButton = gameObject.AddComponent<StartButton>();

            startButton.RemoveListeners();
            yield return new WaitForSeconds(.5f);

            Assert.Zero(button.onClick.GetPersistentEventCount());
            _garbageCollector.Enqueue(gameObject);
        }
    }

}
