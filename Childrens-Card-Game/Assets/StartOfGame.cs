using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfGame : MonoBehaviour
{
    private cardEffectFunctions Functions;
    


    // Start is called before the first frame update
    void Awake()
    {
      Functions = GetComponent<cardEffectFunctions>();
}

    private void Start()
    {
        Functions.updateAll();
    }


}
