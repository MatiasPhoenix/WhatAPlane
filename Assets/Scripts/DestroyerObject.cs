using NUnit.Framework.Constraints;
using UnityEngine;

public class DestroyerObject : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject gameObjectPlane;
    public GameObject gameObjectDeadPlane; //FX dead plane
    public string thisPlayerPlane;
    
    
    [Header("Map borders for dead planes")] //Coordinates for map borders, ONLY FOR DEAD PLANE
    public int limitBoundarySX; 
    public int limitBoundaryDX;
    public int limitBoundaryZ;


    //Private variables
    Quaternion       playerRotation; //Quaternion player
    Collider         playerCollider; //Collider player
    GameObject       playerParentObject; //Pareng of game object
    PlayerShooting   playerShooting; //Script player
    PlayerController playerController; //Script player

    float deadCounter; //Counter for animation dead in game manager
    string nameParent;
    bool isTriggering = false, planeHitted = false; //Bools for game manager

    void Awake()
    {
        playerParentObject = gameObject.transform.parent.gameObject; //Declare parent
        nameParent = playerParentObject.name; //Get parent name

        SoundsManager.instance.PlayFlightSound(thisPlayerPlane);

    }

    void Start()
    {
        deadCounter        = Random.Range(1f, 5f); //Number random for DestroyAndReborn() metod

        gameObjectPlane    = transform.gameObject; //Declare position

        playerCollider     = playerParentObject.GetComponent<Collider>(); //Get the object collider 
        playerShooting     = playerParentObject.GetComponent<PlayerShooting>(); //Get the shooting script
        playerController   = playerParentObject.GetComponent<PlayerController>(); //Get the controller script
    }
    void FixedUpdate()
    {
        if (planeHitted)
        {
            playerParentObject.transform.rotation *= Quaternion.Euler(3f, 0, 0);

            float rangeX = Mathf.Clamp(transform.position.x, limitBoundarySX, limitBoundaryDX);
            float rangeZ = Mathf.Clamp(transform.position.z, -limitBoundaryZ, limitBoundaryZ);
            transform.position = new Vector3(rangeX, transform.position.y, transform.position.z);

            deadCounter -= Time.deltaTime * 1;
            if (deadCounter <= 0) DestroyAndReborn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Bullet")
        {
            SoundsManager.instance.PlayPlaneHitted(thisPlayerPlane);
            ActiveDeadPlane();
            planeHitted = true;
        }
        else if (other.tag == "ObstacleInmortal")
        {
            DestroyAndReborn();
        }
    }

    private void DestroyAndReborn()
    {
        SoundsManager.instance.PlayExplosion(thisPlayerPlane);

        string namePlane = base.gameObject.name;

        if (namePlane == "Player1Plane" && isTriggering == false)
        {
            isTriggering = true;
            GameManager.Instance.StartCoroutineManager("Player1");
            GameManager.Instance.PointPlayer("Player1");
        }
        else if (namePlane == "Player2Plane" && isTriggering == false)
        {
            isTriggering = true;
            GameManager.Instance.StartCoroutineManager("Player2");
            GameManager.Instance.PointPlayer("Player2");
        }
        else if (namePlane == "Player3Plane" && isTriggering == false)
        {
            isTriggering = true;
            GameManager.Instance.StartCoroutineManager("Player3");
            GameManager.Instance.PointPlayer("Player3");
        }
        else if (namePlane == "Player4Plane" && isTriggering == false)
        {
            isTriggering = true;
            GameManager.Instance.StartCoroutineManager("Player4");
            GameManager.Instance.PointPlayer("Player4");
        }
        
        // SoundsManager.instance.StopSound(nameParent);
        GameManager.Instance.CreateExplosion(transform.position);
        Destroy(playerParentObject);
    }

    private void ActiveDeadPlane()
    {
        playerShooting.enabled = false;
        gameObjectDeadPlane.SetActive(true);

        playerController.MoveSpeed = 30f;
        playerController.PitchSpeed = 20f;

        playerCollider.enabled = false;
        playerParentObject.GetComponent<Rigidbody>().useGravity = true;

    }

}
