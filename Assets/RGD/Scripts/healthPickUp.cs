using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickUp : MonoBehaviour {

    public float lifeSpan;
    public float currentLifeTime;
    private string powerUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= lifeSpan)
        {
            DestroyObject(this.gameObject);
        }

        
	}

    private string OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (this.CompareTag("Medkit"))
                powerUp = "";
            else if (this.CompareTag("PowerUp"))
                powerUp = "PowerUp";

            DestroyObject(this.gameObject);
            return powerUp;
        }

        return null;
    }

    private string OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (this.CompareTag("Medkit"))
                powerUp = "Health";
            else if (this.CompareTag("PowerUp"))
                powerUp = "PowerUp";

            DestroyObject(this.gameObject);
            return powerUp;
        }

        return null;
    }
}
