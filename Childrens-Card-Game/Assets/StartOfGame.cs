using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;
using static GetSet;

public class StartOfGame : MonoBehaviour
{

    


    // Start is called before the first frame update
    void Awake()
    {
        runFinalEffects += FinalEffect;

    }

    private void Start()
    {
        updateAll();
    }

    public void FinalEffect(object sender, EffectEventArgs e)
    {
        Debug.Log("FinalEffect run.");


        for(int i = 0; i < e.AfterList.Count; i++)
        {
            if (
                e.BeforeList[i].stats != null && e.AfterList[i].stats != null &&
                e.BeforeList[i].stats.currentHealth > 0  && e.AfterList[i].stats.currentHealth <= 0)
            {

                RunEffects(new List<Card> { e.AfterList[i] }, Die);


            }
        }
    }

    public void Die(List<Card> AfterList)
    {
        AfterList[0].transform.parent = GetContainer(AfterList[0], false, graveyardIndex);
    }

}
