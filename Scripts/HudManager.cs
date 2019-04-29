using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Player player;
    public Text scoreHud;
    public RectTransform healthBar;

    public void Update()
    {
        scoreHud.text = FamePoints.famePointsAmount.ToString();
        float healthPercent = 0;
        if ( player != null)
        {
            healthPercent = player.GetHealth() / player.stats.maxHealth;
        }
        healthBar.localScale = new Vector3(healthPercent, 1, 1);

    }

}
