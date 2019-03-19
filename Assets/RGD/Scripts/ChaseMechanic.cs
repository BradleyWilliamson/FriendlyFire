using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMechanic : MonoBehaviour {

    public float speed;                                     //how fast to move
    private bool forward = true, arrived = false;
    private List<Transform> waypoints = new List<Transform>();
    private CharacterMotor characterMotor;
    private Rigidbody rigid;

    // Use this for initialization
    void Start () {

        speed = 0.3f;

        //get child waypoints, then detach them (so object can move without moving waypoints)
        foreach (Transform child in transform)
            if (child.tag == "Waypoint")
                waypoints.Add(child);

        foreach (Transform waypoint in waypoints)
            waypoint.parent = null;

        if (waypoints.Count == 0)
            Debug.LogError("No waypoints found for 'ChaseMechanic' script. To add waypoints: add child gameObjects with the tag 'Waypoint'", transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //if this is a platform move platforms toward waypoint
    void FixedUpdate()
    {
        if (transform.tag == "Chase")
        {
            if (!arrived && waypoints.Count > 0)
            {
                Vector3 direction = waypoints[1].position - transform.position;
                rigid.MovePosition(transform.position + (direction.normalized * speed * Time.fixedDeltaTime));
            }
        }
    }

    //draw gizmo spheres for waypoints
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (Transform child in transform)
        {
            if (child.tag == "Waypoint")
                Gizmos.DrawSphere(child.position, .7f);
        }
    }
}
