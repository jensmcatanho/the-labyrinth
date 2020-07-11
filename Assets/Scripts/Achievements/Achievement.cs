using System;
using UnityEngine;

namespace Achievements {

    [CreateAssetMenu(menuName = "Maze/Achievement")]
    [Serializable]
    public class Achievement : ScriptableObject, IAchievement {

        [SerializeField] private string _name;

        [SerializeField] private string _description;

        [SerializeField] private string _steamID;
        
        public string Name {
            get {
                return _name;
            }
        }

        public string Description {
            get {
                return _description;
            }
        }

        public string SteamID {
            get {
                return _steamID;
            }
        }

    }

}