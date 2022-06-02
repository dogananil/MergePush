using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (this.tag == "WinTrigger")
        {
            foreach (var character in GameManager.currentTeam)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                character.ChangeAnimation(dance: Random.Range(0, 4));
                character.SetDustParticle(false);
            }
            foreach (var enemy in GameManager.enemyTeam)
            {
                enemy.SetDustParticle(false);
                Random.InitState(System.DateTime.Now.Millisecond);
                enemy.ChangeAnimation(die: Random.Range(0,1));
                enemy.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                enemy.GetComponent<Rigidbody>().useGravity = true;
            }
            UiManager.instance.gameScreenPanel.SetActive(false);
            UiManager.instance.winScreenPanel.SetActive(true);
            UiManager.instance.fx_WinConfetti.SetActive(true);
        }
        if(this.tag=="LoseTrigger")
        {
            foreach (var character in GameManager.currentTeam)
            {
                character.SetDustParticle(false);
                Random.InitState(System.DateTime.Now.Millisecond);
                character.ChangeAnimation(die: Random.Range(0, 1));
                character.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                character.GetComponent<Rigidbody>().useGravity = true;
            }
            foreach (var enemy in GameManager.enemyTeam)
            {
                enemy.SetDustParticle(false);
                Random.InitState(System.DateTime.Now.Millisecond);
                enemy.ChangeAnimation(dance: Random.Range(0, 4));
            }
            UiManager.instance.gameScreenPanel.SetActive(false);
            UiManager.instance.loseScreenPanel.SetActive(true);
        }
        GameManager.isGameEnd = true;

    }
}
