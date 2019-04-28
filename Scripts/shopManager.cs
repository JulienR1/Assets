using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopManager : MonoBehaviour {

    
    public Player player;
    public GameObject canvas;
    public GameObject notEnoughMoney;
    

    private bool shopEnabled;
    private float notMoneyCloseTime;

    public Weapon item;
    
    public float distanceMax;
    public float notMoneyDisplayTime;

    public void Start() {
        notEnoughMoney.SetActive(false);
    }

    public void inContact(Vector3 position) {
        if (Vector3.Distance(position, transform.position) <= distanceMax)
        {
            shopEnabled = true;
        }
    }

    public void Update() {

        canvas.SetActive(shopEnabled);

        if (Input.GetKeyDown(KeyCode.E) && shopEnabled == true) {
            sell();
        }

        shopEnabled = false;

        if(Time.time > notMoneyCloseTime) {
            notEnoughMoney.SetActive(false);
        }
    }

    public void sell() {
        if (player.GetCurrency() >= item.stats.itemCost) {

            Transform itemSpawned = Instantiate(item.transform, player.transform.position, Quaternion.identity);
            itemSpawned.gameObject.SetActive(false);
            itemSpawned.parent = player.transform;
            player.RemoveCurrency(item.stats.itemCost);
        }
        else {
            notEnoughMoney.SetActive(true);
            notMoneyCloseTime = Time.time + notMoneyDisplayTime;
        }
    }

   
}
