using UnityEngine;

public class DoorController : MonoBehaviour
{
    // this is the door object we're controlling
    public GameObject door;
    // how fast the door opens or closes
    public float moveSpeed = 2f;
    // how much the door moves when opening
    public Vector3 openPositionOffset = new Vector3(0, 5, 0);

    // the door's starting position (closed)
    private Vector3 closedPosition;
    private Vector3 openPosition;
    // is the door currently opening?
    private bool isOpening = false;
    // is the door currently closing?
    private bool isClosing = false;
    // this is the door's collider
    private Collider doorCollider;

    void Start()
    {
        closedPosition = door.transform.position;
        // calculate the open position based on the offset
        openPosition = closedPosition + openPositionOffset;

        doorCollider = door.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        // if the player steps into the trigger zon open the door
        if (other.CompareTag("Player"))
        {
            isOpening = true;
            isClosing = false; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        // if the player leaves the trigger zone close the door
        if (other.CompareTag("Player"))
        {
            isClosing = true;
            isOpening = false; 
        }
    }

    void Update()
    {
        if (isOpening)
        {
            // turn off the collider while moving the door
            if (doorCollider != null) doorCollider.enabled = false;

            // slowly move the door to the open position
            door.transform.position = Vector3.Lerp(door.transform.position, openPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(door.transform.position, openPosition) < 0.01f)
            {
                isOpening = false;
                if (doorCollider != null) doorCollider.enabled = true;
            }
        }
        else if (isClosing)
        {
            // turn off the collider while moving the door
            if (doorCollider != null) doorCollider.enabled = false;

            door.transform.position = Vector3.Lerp(door.transform.position, closedPosition, Time.deltaTime * moveSpeed);

            // if the door is fully  stop moving and re-enable the collider
            if (Vector3.Distance(door.transform.position, closedPosition) < 0.01f)
            {
                isClosing = false;
                if (doorCollider != null) doorCollider.enabled = true;
            }
        }
    }
}
