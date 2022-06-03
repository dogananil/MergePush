using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public string characterTag = "Character";
    public string draggingObjectTag = "DraggingCharacter";

    public List<GameObject> outfits;

    public bool isMergeTriggered = false;

    private Vector3 mOffset;

    private Collider otherCharacterCollider;

    private float mZCoord;

    public Character character;
    public TextMeshPro text;

    private void Start()
    {
        character = transform.GetComponent<Character>();
    }
    private void FixedUpdate()
    {
        character.level = int.Parse(text.text);
    }

    private void OnMouseDown()
    {
        if (GameManager.isGameStart == false)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    private void OnMouseUp()
    {
        if (GameManager.isGameStart == false)
        {
            if (!transform.GetComponent<CapsuleCollider>().enabled)
                transform.GetComponent<CapsuleCollider>().enabled = true;

            if (isMergeTriggered == true && otherCharacterCollider != null)
            {
                TextMeshPro textMesh = otherCharacterCollider.transform.GetComponent<DragObject>().text;
                int textValue = int.Parse(textMesh.text);

                if (textValue * 2 == 4 || textValue * 2 == 16)
                {
                    otherCharacterCollider.transform.localScale = new Vector3((float)(transform.localScale.x + 0.2), (float)(transform.localScale.y + 0.2),
                      (float)(transform.localScale.z + 0.2));

                    otherCharacterCollider.transform.localPosition -= Vector3.up * 0.2f;
                    Transform tempParticleDust = otherCharacterCollider.transform.GetComponent<Character>().particleDust.transform;

                    otherCharacterCollider.transform.GetComponent<Character>().particleDust.transform.localScale += new Vector3(tempParticleDust.localScale.x / 5,
                        tempParticleDust.localScale.y / 5, tempParticleDust.localScale.z / 5);
                }

                textMesh.text = (textValue * 2).ToString();

                GameManager.ourPower += (textValue * 2 * 2 - 1) - (textValue * 2 - 1) * 2;

                GameManager.teamLayout[LevelManager.instance.characterPositions.IndexOf(otherCharacterCollider.transform.parent)] = textValue * 2;
                GameManager.teamLayout[LevelManager.instance.characterPositions.IndexOf(this.transform.parent)] = 0;

                var tempCharacter = otherCharacterCollider.transform.GetComponent<Character>();
                tempCharacter.SetLevelupParticle(true);


                
                if (Math.Log(textValue, 2) <= 5)
                {
                    for (int i = 0; i<=Math.Log(textValue, 2); i++)
                    {
                        tempCharacter.outfits[i].SetActive(true);
                    }

                }



                PushMovement.SetSpeed();
                otherCharacterCollider.tag = characterTag;
                transform.tag = characterTag;
                transform.SetParent(null);
                transform.gameObject.SetActive(false);


            }
            else
            {
                transform.localPosition = new Vector3(0, 0, 0);
                transform.tag = characterTag;
            }
        }

    }

    private void OnMouseDrag()
    {
        if (GameManager.isGameStart == false)
        {
            transform.position = GetMouseWorldPos() + mOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, 5.5f);
            if (transform.tag == characterTag)
                transform.tag = draggingObjectTag;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition; //(x,y)
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.tag == draggingObjectTag && other.tag == characterTag)
        {
            Debug.Log("Drag Objenin characterLevel: " + character.level);
            Debug.Log("Other Objenin characterLevel: " + other.GetComponent<DragObject>().character.level);
            
            if (character.level == other.GetComponent<DragObject>().character.level)
            {
                isMergeTriggered = true;
                otherCharacterCollider = other;
            }
            else
            {
                isMergeTriggered = false;
            }
            Debug.Log("isMergeTriggered: "+isMergeTriggered);
        }

        else if (transform.tag == characterTag && other.tag == "LoseTrigger")
        {
            transform.GetComponent<Character>().SetDustParticle(false);
            transform.GetComponent<Character>().SetCrackParticle(false);
        }
    }

}
