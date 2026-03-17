using UnityEngine;

public class SampleOrbit : MonoBehaviour
{
    public Transform target;         //타겟 지정
    public float speed = 0.1f;           //이동 속도
    public float angle = 45f;             //각도
    public float radian =  15f;            //라디안
    public float distance = 5f;


    public void Update()
    {
        //타겟을 기준으로 돌아야 함
        angle += speed * Time.deltaTime;            //각도는 이동 속도에 따라 늘어나게 하고

        float rad = angle * Mathf.Rad2Deg;           //라디안 공식으로 각도 찾기

        float x = Mathf.Cos(rad) * distance;                 //Cos함수로 x 값 구하가ㅣ     바보야 거리계산을안하면어떡하니 태양안에서돌잖아
        float z = Mathf.Sin(rad) * distance;                 //Sin 함수로 Y값 구하기 (3D라 z에 넣어둠..)

        transform.position = target.position + new Vector3(x, 0, z);           // 오브젝트의 위치는 타겟 위치에서 새 값을 정한 위치
       
    }
}
