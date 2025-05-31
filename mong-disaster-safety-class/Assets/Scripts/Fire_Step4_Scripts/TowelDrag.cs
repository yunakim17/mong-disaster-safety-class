using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowelDrag : MonoBehaviour
{
    public static TowelDrag Instance;
    public GameObject towel;
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

  

    // Start is called before the first frame update
    


}
