using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackScript : MonoBehaviour
{
    public GameObject crackFx;
    public IEnumerator CreateCrackFx()
    {
        while (GameManager.isGameEnd == false && GameManager.isGameStart == true)
        {

            var tempCrackFx = Instantiate(crackFx, this.transform);
            tempCrackFx.transform.localPosition= Vector3.zero;
            tempCrackFx.transform.SetParent(null);
            yield return new WaitForSeconds(1.5f);
        }
    }
    
}
