using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

namespace Tests {

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
            gameObject.AddComponent<Menu.StartButton>();
            var isStartButtonClicked = false;

            Core.EventManager.Instance.AddListenerOnce((Events.StartButtonClicked e) => {
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
            gameObject.AddComponent<Menu.StartButton>();
            var isStartButtonClicked = false;

            Core.EventManager.Instance.AddListenerOnce((Events.StartButtonClicked e) => {
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
            var startButton = gameObject.AddComponent<Menu.StartButton>();

            startButton.RemoveListeners();
            yield return new WaitForSeconds(.5f);

            Assert.Zero(button.onClick.GetPersistentEventCount());
            _garbageCollector.Enqueue(gameObject);
        }
    }

}
