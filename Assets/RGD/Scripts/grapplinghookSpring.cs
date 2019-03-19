using UnityEngine;

public class grapplinghookSpring : MonoBehaviour
{


    //grappling hook variables
    public LineRenderer line;
    DistanceJoint2D joint;
    Vector3 target;
    Vector2 targetPos;
    Vector2 transformConvert;
    //RaycastHit2D hit;
    public float distance = 10f;
    public LayerMask mask;
    public float speed = 10.0f;
    private float step = 0.0f;
    private bool grappled = false;

    //3d grapplehook variables
    HingeJoint hJoint;
    RaycastHit hit;
    float hingeLength = 0f;
    Rigidbody body;
    public float transitionLength = 1f;
    public float transitionStartTime = 0f;

    //gun additions
    public GameObject bullets;
    public float bulletSpeed;
    public float reloadTime;
    public bool reloading;

    private float reload;
    public float pullSpeed;
    public float limitsMin, limitsMax, restingPosition, swingSlowdown;
    public float shotType;

    private MenuPause menuPause;



    // Use this for initialization
    void Start()
    {
        //line.enabled = false;
        menuPause = FindObjectOfType<MenuPause>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuPause.gamePaused == false)
        {
            //if (Input.GetMouseButtonDown(0))
            if (Input.GetMouseButtonDown(1))
            {
                Grapple();
            }

            if (grappled) // if grappled check if user wants to move up rope
            {
                if (Input.GetKey(KeyCode.E))
                {
                    MoveTo();
                }
            }

            //gun stuff
            if (Input.GetMouseButtonDown(0))
            {

                if (!reloading)
                {
                    FireGun();
                    reloading = true;
                }
            }

            if (reloading)
            {
                reload += Time.deltaTime;

                if (reload >= reloadTime)
                {
                    reload = 0.0f;
                    reloading = false;
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            //line.enabled = false;
            Destroy(hJoint);
            grappled = false;
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<CharacterMotor>().sidescroller = true;
            gameObject.GetComponent<DoubleJumpEnabler>().enabled = true;
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void FixedUpdate()
    {
    }

    void Grapple()
    {
        // create ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 3d raycast and send result to hit
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Terrain") // if hit is terrain then create joint and set information
            {
                target = hit.transform.position;
                grappled = true;
                gameObject.GetComponent<PlayerMove>().enabled = false;
                gameObject.GetComponent<CharacterMotor>().sidescroller = false;
                gameObject.GetComponent<DoubleJumpEnabler>().enabled = false;
                gameObject.GetComponent<Rigidbody>().freezeRotation = false;

                hJoint = gameObject.AddComponent<HingeJoint>();
                hJoint.connectedBody = hit.transform.GetComponent<Rigidbody>();
                hJoint.anchor = transform.InverseTransformPoint(hit.collider.transform.position);
                hJoint.connectedBody = gameObject.GetComponent<Rigidbody>();
                hJoint.useLimits = true;

                JointLimits limits = hJoint.limits;
                limits.min = limitsMin;
                limits.max = limitsMax;
                hJoint.limits = limits;

                JointSpring spring = hJoint.spring;
                hJoint.useSpring = true;
                spring.targetPosition = restingPosition;
                spring.damper = swingSlowdown;
                hJoint.spring = spring;
            }
        }
    }

    void MoveTo()
    {
        hJoint.autoConfigureConnectedAnchor = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Vector3 direction;
        direction = gameObject.transform.position - hit.collider.transform.position;

        hJoint.connectedAnchor = hJoint.connectedAnchor - (direction * pullSpeed);

        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
    
    private void OnGUI()
    {

        GUI.Label(new Rect(20, 40, 300, 20), "x: " + target.x + "Y: " + target.y);
        //GUI.Label(new Rect(40, 40, 300, 20), hit.transform.position.ToString());
    }

    void FireGun()
    {
        if (shotType == 0)
        {
            GameObject newShot = Instantiate(bullets);

            //mouse position to screen position
            Vector3 mouse = Input.mousePosition;


            Vector3 direction = mouse - transform.position;
            Vector3 startPoint = gameObject.transform.position + new Vector3(0, 1, 0);
            newShot.transform.position = startPoint + (direction / 800);

            newShot.SetActive(true);
            newShot.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed);
        }
        else if (shotType == 1)
        {
            GameObject newShot = Instantiate(bullets);
            GameObject newShot2 = Instantiate(bullets);
            GameObject newShot3 = Instantiate(bullets);

            //mouse position to screen position
            Vector3 mouse = Input.mousePosition;
            Vector3 shotSpread = new Vector3(0, 50, 0);


            Vector3 direction = mouse - transform.position;
            Vector3 direction2 = (mouse - transform.position) + shotSpread;
            Vector3 direction3 = (mouse - transform.position) - shotSpread;
            Vector3 startPoint = gameObject.transform.position + new Vector3(0, 1, 0);
            newShot.transform.position = startPoint + (direction / 800);
            newShot2.transform.position = startPoint + (direction / 800);
            newShot3.transform.position = startPoint + (direction / 800);

            newShot.SetActive(true);
            newShot.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed);
            newShot2.SetActive(true);
            newShot2.GetComponent<Rigidbody>().AddForce((direction2) * bulletSpeed);
            newShot3.SetActive(true);
            newShot3.GetComponent<Rigidbody>().AddForce((direction3) * bulletSpeed);
        }
    }
}
