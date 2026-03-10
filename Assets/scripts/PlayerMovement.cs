using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    Vector3 normalizedVector;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        //nomalized를 이용한 대각선 가속도 방지
        //vector3 direction = new vector3(moveinput.x, moveinput.y, 0).normalized;
        //transform.translate(direction * movespeed * time.deltatime);

        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);

        float sqrMagnitude = direction.x * direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
        float magnitude = Mathf.Sqrt(sqrMagnitude);

        //0으로 나누기 방지
        if (magnitude > 0)
        {
            normalizedVector = direction / magnitude;
        }
        else
        {
            normalizedVector = Vector3.zero; 
        }

        transform.Translate(normalizedVector * moveSpeed * Time.deltaTime);

    }
}
