using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float speed = 3f; 
    public float moveDistance = 5f; 

    private Vector3 startPosition; // wher it starts from 
    private Vector3 targetPosition; // where its heading 
    private bool movingRight = true; // is it going right or not

    void Start()
    {
        // Save wher it strated at 
        startPosition = transform.position;

        // figure out the first place it shold go
        targetPosition = startPosition + Vector3.right * moveDistance;
    }

    void Update()
    {
        // move the thing clsoer to where it shold go
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // see if it reach the spot it shouyld go
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingRight = !movingRight;

            targetPosition = movingRight
                ? startPosition + Vector3.right * moveDistance
                : startPosition - Vector3.right * moveDistance;
        }
    }
}
