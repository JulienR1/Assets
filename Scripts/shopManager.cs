using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    
    public Player player;
    public GameObject canvas;
    public GameObject notEnoughMoney;
    public GameObject doublon;

    private bool shopEnabled;
    private float notMoneyCloseTime;
    private float doublonCloseTime;

    public Weapon item;
    
    public float distanceMax;
    public float notMoneyDisplayTime;
    public float doublonDisplayTime;

    public void Start() {
        notEnoughMoney.SetActive(false);
        doublon.SetActive(false);
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
            
            if(player.verifDoublons(item) == false) {
                if (sell() == true) {
                    player.AddWeapon(item);
                }
            }
            else {
                doublon.SetActive(true);
                doublonCloseTime = Time.time + doublonDisplayTime;
            }
           
        }

        shopEnabled = false;

        if(Time.time > notMoneyCloseTime) {
            notEnoughMoney.SetActive(false);
        }

        if (Time.time > doublonCloseTime) {
            doublon.SetActive(false);
        }
    }

    public bool sell() {
        if (player.GetCurrency() >= item.stats.itemCost) {

            Transform itemSpawned = Instantiate(item.transform, player.transform.position, Quaternion.identity);
            itemSpawned.gameObject.SetActive(false);
            itemSpawned.parent = player.transform;
            player.RemoveCurrency(item.stats.itemCost);
            return true;
        }
        else {
            notEnoughMoney.SetActive(true);
            notMoneyCloseTime = Time.time + notMoneyDisplayTime;
            return false;
        }
    }

   
}
