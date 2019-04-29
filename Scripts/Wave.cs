using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave { 
    public WaveSpecs[] specs;

   [System.Serializable]
    public struct WaveSpecs
    {
        public Enemy enemy;
        public int enemyCount;
    }
}
