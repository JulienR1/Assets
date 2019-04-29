using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FamePoints {

    public static int famePointsAmount;
    public static bool mort;

    public static void Start(){
        famePointsAmount = 0;
        mort = false;
    }

    public static void KillFame(int enemyFame) {
        famePointsAmount += enemyFame;
    }

    public static void EndWaveFame() {

    }

    public static void RecoitDegatFam() {

    }

    public static void MortFame() {
        famePointsAmount = 0;
        mort = true;
        
    }
  
}
