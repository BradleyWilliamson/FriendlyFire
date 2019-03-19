using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public bool health;
    public bool doubleShot;// etc
    public AudioClip collectSound;

    public float powerUpLength;

    private PowerUpHandler powerUpManager;

    // Use this for initialization
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            powerUpManager.ActivatePowerup(health, doubleShot, powerUpLength);
            gameObject.SetActive(false);
        }
        
    }
}
