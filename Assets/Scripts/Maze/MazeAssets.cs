using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class MazeAssets {

    [SerializeField] private AssetReference _wall;

    [SerializeField] private AssetReference _chest;

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

    public MazeAssets(AssetReference wallAssetReference) {
        Wall = wallAssetReference;
    }

}