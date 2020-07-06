using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Maze {

    [Serializable] public class MazeAssets {

        #region private fields
        [SerializeField] private AssetReference _wall;

        [SerializeField] private AssetReference _chest;
        #endregion

        public AssetReference Wall {
            get {
                return _wall;
            }

            set {
                _wall = value;
            }
        }

        public AssetReference Chest {
            get {
                return _chest;
            }

            set {
                _chest = value;
            }
        }
    }

}