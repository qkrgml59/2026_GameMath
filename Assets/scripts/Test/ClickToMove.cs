using UnityEngine;
using UnityEngine.InputSystem;


public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float RunSpeed = 10f;
    public float WalkSpeed = 5f;
    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isSprinting = false;



    public void OnPoint(InputValue value)
    {
        //마우스 위치 업데이트
        mouseScreenPosition = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;

        if (isSprinting)
        {
            moveSpeed = RunSpeed; // 달리기
        }
        else
        {
            moveSpeed = WalkSpeed; // 멈추면 걷기 속도로 복귀
        }

    }

    public void OnClick(InputValue value)
    {
        if(value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);  //마우스 위치에서 레이저 발사
            RaycastHit[] hits = Physics.RaycastAll(ray);  //레이저 경로에 있는 모든 물체 탐색

            foreach(RaycastHit hit in hits)        //모든 물체에 한해 반복
            {
                if (hit.collider.gameObject != gameObject) ;   //부딪힌 물체가 나 자신이 아닐 때만
                {
                    targetPosition = hit.point;    //타겟 지점
                    targetPosition.y = transform.position.y;
                    isMoving = true;

                    break;
                }
            }
        }
    }

    

    void Update()
    {


        if (isMoving)
        {
            Vector3 direction = targetPosition - transform.position;   //목적지까지의 방향 계산

            float sqrmagnitude = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
            float magnitude = Mathf.Sqrt(sqrmagnitude);

            if(magnitude > 0.1f)
            {
                transform.Translate(direction / magnitude * moveSpeed * Time.deltaTime);   //목적지까지의 방향으로 이동
            }
            else
            {
                isMoving = false;
            }

        }

  

    }
}
