using UnityEngine;

public class Testtrigonometry : MonoBehaviour
{
    public void Start()
    {
        float degreses = 45f;

        float radians = degreses * Mathf.Deg2Rad;
        Debug.Log("45도 -> 라디안 : " + radians);

        float radianvalue = Mathf.PI / 3;
        float degreevalue = radianvalue * Mathf.Rad2Deg;
        Debug.Log("파이/3 라디안 -> 도 변환" + degreevalue);
    }

    void Update()
    {
        float speed = 5f;
        float angle = 30f;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 direction = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        transform.position += direction * speed * Time.deltaTime;
    }
}
