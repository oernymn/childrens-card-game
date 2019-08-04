using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfGame : MonoBehaviour
{
    private Functions Functions;
    


    // Start is called before the first frame update
    void Awake()
    {
      Functions = GetComponent<Functions>();
}

    private void Start()
    {
        Functions.updateAll();
    }


}
