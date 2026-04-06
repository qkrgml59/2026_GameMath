using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SimulateTest : MonoBehaviour
{
    //[Header("UI")]
    //public TextMeshProUGUI text1;

    [Header("Crit UI")]
    public TextMeshProUGUI totalHitText;
    public TextMeshProUGUI critHitText;
    public TextMeshProUGUI targetCritRateText;
    public TextMeshProUGUI realCritRateText;

    [Header("Drop Rate UI")]
    public TextMeshProUGUI normalRateText;
    public TextMeshProUGUI rareRateText;
    public TextMeshProUGUI epicRateText;
    public TextMeshProUGUI legendaryRateText;

    [Header("Drop Count UI")]
    public TextMeshProUGUI normalCountText;
    public TextMeshProUGUI rareCountText;
    public TextMeshProUGUI epicCountText;
    public TextMeshProUGUI legendaryCountText;

    [Header("Drop Result UI")]
    public TextMeshProUGUI currentDropText;

    public CirticalManager critManager;

    int enemyHP = 300;
    int maxHP = 300;
    int attack = 30;

    float normal = 50f;
    float rare = 30f;
    float epic = 15f;
    float legendary = 5f;

    int totalDamage = 0;
    string lastDrop = "없음";

    int normalCount = 0;
    int rareCount = 0;
    int epicCount = 0;
    int legendaryCount = 0;

    public void SimulateGachaSingle()
    {
        Simulate();
        //text1.text = "결과 " + Simulate();
        UpdateUI();
    }

    public void SimulateGachaTenTime()
    {
        //text1.text = "";

        for (int i = 0; i < 10; i++)
        {
            string result = Simulate();
            //text1.text += result + "\n";
        }

        UpdateUI();
    }

    string Simulate()
    {
        bool isCrit = critManager.RollCrit();

        int dmg = attack;
        if (isCrit) dmg *= 2;

        enemyHP -= dmg;
        totalDamage += dmg;

        string result = isCrit ? $"치명타 {dmg}" : $"일반 {dmg}";
        result += $" | HP: {enemyHP}/{maxHP}";

        if (enemyHP <= 0)
        {
            float r = Random.Range(0f, 100f);

            if (r < legendary)
            {
                lastDrop = "전설";
                legendaryCount++;
                ResetRates();
            }
            else if (r < legendary + epic)
            {
                lastDrop = "희귀";
                epicCount++;
                AdjustRates();
            }
            else if (r < legendary + epic + rare)
            {
                lastDrop = "고급";
                rareCount++;
                AdjustRates();
            }
            else
            {
                lastDrop = "일반";
                normalCount++;
                AdjustRates();
            }

            enemyHP = maxHP;
            totalDamage = 0;
        }

        return result;
    }

    void AdjustRates()
    {
        legendary += 1.5f;
        normal -= 0.5f;
        rare -= 0.5f;
        epic -= 0.5f;
    }
    void ResetRates()
    {
        normal = 50f;
        rare = 30f;
        epic = 15f;
        legendary = 5f;
    }


    void UpdateUI()
    {

        totalHitText.text = $"전체 공격: {critManager.totalHits}";
        critHitText.text = $"치명타: {critManager.critHits}";
        targetCritRateText.text = $"설정 확률: {critManager.targetRate * 100f:0.00}%";

        float realRate = critManager.totalHits > 0
            ? (float)critManager.critHits / critManager.totalHits
            : 0f;

        realCritRateText.text = $"실제 확률: {realRate * 100f:0.00}%";

        normalRateText.text = $"일반: {normal:0.0}%";
        rareRateText.text = $"고급: {rare:0.0}%";
        epicRateText.text = $"희귀: {epic:0.0}%";
        legendaryRateText.text = $"전설: {legendary:0.0}%";

        normalCountText.text = $"일반 획득 : {normalCount}"; ;
        rareCountText.text = $"고급 획득 : {rareCount}";
        epicCountText.text = $"희귀 획득 : {epicCount}";
        legendaryCountText.text = $"전설 획득 : {legendaryCount}";

        currentDropText.text = $"현재 드롭: {lastDrop}";
    }

    public void SingleButtonOnClick()
    {
        Debug.Log("버튼 눌림");
        SimulateGachaSingle();
    }

    public void TenButtonOnCklick()
    {
        SimulateGachaTenTime();
    }
}
