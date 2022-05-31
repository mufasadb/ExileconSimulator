using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButtons : MonoBehaviour
{
    // Start is called before the first frame update    
    public void Fight()
    {
        Debug.Log("fight");
        FightHandler.instance.doFight();
    }

    public void CAncel(){
        FightHandler.instance.CancelFight();
    }
    // Update is called once per frame
    
}
