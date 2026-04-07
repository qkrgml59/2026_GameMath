using System.Drawing;
using TMPro;
using UnityEngine;
public class DamageSimulate : MonoBehaviour
{
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI logDisplay;
    public TextMeshProUGUI resultDisplay;
    public TextMeshProUGUI rangeDisplay;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI TotalCritDamage;
    public TextMeshProUGUI MaxDamageText;


    private int level = 1;
    private float totalDamage = 0, baseDamage = 20f;
    private int attackCount = 0;
    private int critCount = 0;
    private int wellCount = 0;
    private int nullCont;
    bool isCrit = false;
    float totalCritDamage;
    float MaxDamage = 0;


    private string weaponname;
    private float stdDevMult, critRate, critMult;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   public void SetWeapon(int id)
    {
        if(id == 0)
        {
            SetStats("단검", 0.1f, 0.4f, 1.5f);
        }
        else if(id ==1)
        {
            SetStats("장검", 0.2f, 0.3f, 2.0f);
        }
        else
        {
            SetStats("도끼", 0.1f, 0.4f, 1.5f);
        }

        logDisplay.text = string.Format("{0} 장착!", weaponname);
        UpdateUI();
    }

    private void SetStats(string _name, float _stdDev, float _critRate, float _critMult)
    {
        weaponname = _name;
        stdDevMult = _stdDev;
        critRate = _critRate;
        critMult = _critMult; 
    }

    public void LevelUp()
    {
        totalDamage = 0;
        attackCount = 0;
        level++;
        baseDamage = level * 20f;
        logDisplay.text = string.Format("레벨업! 현재 레벨 : {0}", level);
        UpdateUI();
    }

    public void OnAttack()
    {
        //정규 분포 데미지 계산
        float sd = baseDamage * stdDevMult;
        float normalDamage = GetNormalStdDevDamage(baseDamage, sd);
     
        //치명타 판정
        bool isCrit = Random.value < critRate;
        float finalDamage = isCrit ? normalDamage * critMult : normalDamage;

        //통계 누적
        attackCount++;
        totalDamage += finalDamage;

        //로그 및 Ui 업데이트
        string cirtMark = isCrit ? "<color=red>[치명타!]</color>" : "";
        logDisplay.text = string.Format("{0}데미지 : {1:F1}", cirtMark, finalDamage);
        UpdateUI();
    }

    public void ManyAttack()
    {
        totalDamage = 0;
        attackCount = 0;

        bool isCrit = Random.value < critRate;
        float finalDamage = 0;

      for (int i = 0; i< 1000; i++)
        {
            float sd = baseDamage * stdDevMult;
            float normalDamage = GetNormalStdDevDamage(baseDamage, sd);

            if (normalDamage > baseDamage + sd * 2)
            {
                normalDamage += baseDamage * 2;
                attackCount++;
                wellCount++;
                totalDamage += finalDamage;
                Debug.Log("약점공격 성공");
            }
            else if(normalDamage > baseDamage - sd * 2)
            {
                normalDamage = 0;
                attackCount++;
                nullCont++;
                totalDamage += finalDamage;
                Debug.Log("명중 실패");
            }
            else 
            {
                OnAttack();
                Debug.Log("일반 공격 성공");
            }

            totalDamage += finalDamage;

            string cirtMark = isCrit ? "<color=red>[치명타!]</color>" : "";
            logDisplay.text = string.Format("{0}데미지 : {1:F1}", cirtMark, finalDamage);
            DamageText.text = string.Format("약점 공격 횟수 : {0} / 명중 실패 횟수 {1}",
               wellCount, nullCont);
            TotalCritDamage.text = string.Format("");
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        statusDisplay.text = string.Format("Level: {0} / 무기 : {1}\n기본데미지:: {2}/ 치명타:{3}% (x{4})",
            level, weaponname, baseDamage, critRate * 100, critMult);

        rangeDisplay.text = string.Format("예상 일반 데미지 범위 :[{0:F1} ~ {1:F1}]",
            baseDamage - (3 * baseDamage * stdDevMult),
            baseDamage + (3 * baseDamage * stdDevMult));

        float dpa = attackCount > 0 ? totalDamage / attackCount : 0;
        resultDisplay.text = string.Format("누적 데미지 :{0:F1}\n 공격 횟수 : {1}\n평균 DPA : {2:F2}",
            totalDamage, attackCount, dpa);



    }

    private float GetNormalStdDevDamage(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        float randStdnormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        return mean + stdDev * randStdnormal;
    }
}
