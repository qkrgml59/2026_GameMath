using UnityEngine;

public class UnityRandomSeed : MonoBehaviour
{
    //우리가 통제하고 싶은 Random을 사용할때 주로 쓴다
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Random.InitState(1234);
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(Random.Range(1, 7));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
