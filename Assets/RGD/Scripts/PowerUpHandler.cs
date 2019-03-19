using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour {

    //powerup types
    private Health playerHP;
    private bool health;
    private bool doubleShot;// etc

    //powerup info/stats
    private int healthGain = 10;

    // if this powerup is active
    public bool powerUpActive;

    private float powerUpLengthCounter;

    // Use this for initialization
    void Start () {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();                 //Assign the players hp variable to the players game object health script
    }
	
	// Update is called once per frame
	void Update () {
		if(powerUpActive)
        {
            powerUpLengthCounter -= Time.deltaTime;

            if(doubleShot)
            {
                // do doubleshot code
            }

            if(health)
            {
                playerHP.currentHealth += healthGain;                
            }

            if(powerUpLengthCounter <= 0)
            {
                powerUpActive = false;
            }
        }
	}

    public bool getHealth()
    {
        return health;
    }

    public bool getDoubleShot()
    {
        return doubleShot;
    }

    public float getPowerUpLength()
    {
        return powerUpLengthCounter;
    }

    public void ActivatePowerup(bool hp, bool doubleS, float time)
    {
        health = hp;
        doubleShot = doubleS;
        powerUpLengthCounter = time;

        powerUpActive = true;
    }
}
