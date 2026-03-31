using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SimulateTest : MonoBehaviour
{
    public TextMeshProUGUI text1;

    public void SimulateGachaSingle()
    {
        text1.text = "결과 " + Simulate();
    }

    public void SimulateGachaTenTime()
    {
        List<string> results = new List<string>();
        for(int i = 0; i< 9; i++)
        {
            results.Add(Simulate());
        }

        //10번째는 A이상으로 설정
        float r2 = Random.value;
        string result2 = string.Empty;
        if (r2 < 2f / 3f) result2 = "A";
        else result2 = "S";
        results.Add(result2);
        text1.text = "결과 :" + string.Join(",", results);
        // Debug.Log("Gacha Results:" + string.Join(",", results));
    }

    string Simulate()
    {
        float r = Random.value;
        string result = string.Empty;

        if (r < 0.4f) result = "C";
        else if (r < 0.7f) result = "B";
        else if (r < 0.9f) result = "A";
        else result = "S";

        return result;
    }

    public void SingleButtonOnClick()
    {
        SimulateGachaSingle();
       


    }

    public void TenButtonOnCklick()
    {
        SimulateGachaTenTime();
        
    }
}
