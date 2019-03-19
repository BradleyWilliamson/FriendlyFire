using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForDestruction : MonoBehaviour {

    public GameObject DestroyedVersion;
    public AudioClip collectSound;

    void OnCollisionEnter()
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
          
    }
}
