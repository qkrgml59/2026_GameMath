using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 120f;

    private Vector2 moveInput;

    public bool isParryLeft;
    public bool isParryRight;

    void Update()
    {
        InputUpdate();
        Move();
        Rotate();
    }

    void InputUpdate()
    {
        moveInput = Vector2.zero;

        // 앞뒤
        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;

        // 좌우 회전
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;

        // 패링
        isParryLeft = Keyboard.current.qKey.isPressed;
        isParryRight = Keyboard.current.eKey.isPressed;
    }

    void Move()
    {
        transform.Translate(Vector3.forward * moveInput.y * moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * moveInput.x * rotateSpeed * Time.deltaTime);
    }
}