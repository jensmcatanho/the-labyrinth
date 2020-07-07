using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

namespace Tests {

    public class StartButtonTests {

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
            yield return null;

            Assert.IsTrue(isStartButtonClicked);
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

            yield return null;

            Assert.IsFalse(isStartButtonClicked);
        }


        [UnityTest]
        public IEnumerator Test_StartButton_RemoveListeners_Should_Remove_OnClick_Callback() {
            var gameObject = new GameObject("Start Button");
            var button = gameObject.AddComponent<Button>();
            var startButton = gameObject.AddComponent<Menu.StartButton>();

            startButton.RemoveListeners();
            yield return null;

            Assert.Zero(button.onClick.GetPersistentEventCount());
        }

        public void SetUpEventManager() {
            var eventManager = new GameObject();
            eventManager.AddComponent<Core.EventManager>();
        }
    }

}
