using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopManager : MonoBehaviour {

    
    public Player player;
    public GameObject canvas;
    public GameObject notEnoughMoney;
    

    private bool shopEnabled;

    public GameObject item;
    public int price;
    public float distanceMax;

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
    }

    public void sell() {
        if (player.GetCurrency() >= price) {

            Transform itemSpawned = Instantiate(item.transform, player.transform.position, Quaternion.identity);
            itemSpawned.gameObject.SetActive(false);
            itemSpawned.parent = player.transform;
            player.RemoveCurrency(price);
        }
        else {
            notEnoughMoney.SetActive(true);
        }
    }

   
}
