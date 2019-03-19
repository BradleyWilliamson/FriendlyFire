using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {

    public Slider healthSlider;
    public Slider napalmSlider;
    public Slider playerLocSlider;
    public Slider powerUpDoubleShotSlider;
    private GameObject UICanvas;
    private GameObject powerUpBullet;
    private Health health;
    private MoveToPoints napalm;
    private GameObject player;
    private healthPickUp pickups;
    private PowerUpHandler powerUpManager;
    private float napalmPercent = 0.0f, napalmS = 0.0f, napalmE = 0.0f, mapStart = 0.0f, mapFinish = 0.0f;

    public Text displayText;

    // Use this for initialization
    void Start () {
        //Assign variables

        //Assign all the variables to their objects
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<Health>();                 
        napalm = GameObject.FindGameObjectWithTag("Napalm").GetComponent<MoveToPoints>();
        
        pickups = player.GetComponent<healthPickUp>();
        powerUpManager = FindObjectOfType<PowerUpHandler>();
        powerUpBullet = GameObject.Find("PowerUpBullet");
        mapStart = GameObject.FindGameObjectWithTag("startMap").transform.position.x;
        mapFinish = GameObject.FindGameObjectWithTag("endMap").transform.position.x;

        //Napalm Location Variables
        napalmS = napalm.Start;
        napalmE = napalm.End;
    }
	
	// Update is called once per frame
	void Update () {
        healthSlider.value = health.currentHealth;//Keep the health sliders value equal to the players current health
        displayText.text = (healthSlider.value).ToString()+ "%";

        napalmSlider.value = ((napalm.transform.position.x -napalmS) / (napalmE - napalmS)) * 100;
        playerLocSlider.value = ((player.transform.position.x - mapStart) / (mapFinish - mapStart)) * 100;

        if(powerUpManager.powerUpActive)//if a powerups active do this
        {
            if(powerUpManager.getDoubleShot())// if doubleshot is active
            {
                if (powerUpBullet != null)
                {
                    powerUpBullet.SetActive(true);
                }
                powerUpDoubleShotSlider.enabled = true;
                powerUpDoubleShotSlider.value = ((powerUpManager.getPowerUpLength()) / (5.0f)) * 100;
            }
            else
            {
                if (powerUpBullet != null)
                {
                    powerUpBullet.SetActive(false);                    
                }
                powerUpDoubleShotSlider.enabled = false;
            }
        }
        else
        {
            if (powerUpBullet != null)
                powerUpBullet.SetActive(false);
        }
	}
}
