using TMPro;
using UnityEngine;

public class DiceTest : MonoBehaviour
{
    int[] counts = new int[6];

    public TextMeshProUGUI text1;
    TextMeshProUGUI text2;
    TextMeshProUGUI text3;
    TextMeshProUGUI text4;
    TextMeshProUGUI text5;
    TextMeshProUGUI text6;

    public int trais = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        for (int i = 0; i < trais; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1]++;

        }
        
        for (int i = 0; i < counts.Length; i++)
        {
            float percent = (float)counts[i] / trais * 100f;
            Debug.Log($"{i + 1} : {counts[i]}회, ({percent:F2}");
        }

        text1.text = "Hello Hello";
    }

   
}
