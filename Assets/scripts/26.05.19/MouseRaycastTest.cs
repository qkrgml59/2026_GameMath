using UnityEngine;
using UnityEngine.InputSystem;


public class MouseRaycastTest : MonoBehaviour
{
    public Rigidbody player1Ball;
    public Rigidbody player2Ball;

    public bool isPlayerturn = true;
    public bool canShoot = true;

    public float rayDistance = 100f;
    public float hitPower = 2f;
    float moveInput;
    public CameraOrbit cam;
    public void OnMouse(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
        cam.moveInput = moveInput;
    }

    void Start()
    {
        cam.target = player1Ball.transform;
    }
    void Update()
    {
        if(!canShoot)
        {
            if(player1Ball.linearVelocity.magnitude <0.5f &&
                player2Ball.linearVelocity.magnitude < 0.5f)
            {
                canShoot = true;

                isPlayerturn = !isPlayerturn;

                if (isPlayerturn)
                {
                    cam.target = player1Ball.transform;
                }
                else
                {
                    cam.target = player2Ball.transform;
                }
            }
        }
    }

    public void OnClick(InputValue value)
    {
        if (!value.isPressed || !canShoot)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();


        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;

            if (!isPlayerturn && rb != player1Ball)
                return;
            if (!isPlayerturn && rb != player2Ball)
                return;

            if(rb != null)
            {
                Vector3 hitPoint = hit.point;
                Vector3 center = rb.gameObject.transform.position;
                Vector3 forceDirection = center - hitPoint;
                forceDirection.y = 0f;
                forceDirection.Normalize();

                rb.AddForce(forceDirection * hitPower, ForceMode.Impulse);

                canShoot = false;
            }
        }
    }
    
}
