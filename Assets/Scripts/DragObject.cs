using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public string characterTag = "Character";
    public string draggingObjectTag = "DraggingCharacter";

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
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();


    }

    private void OnMouseUp()
    {
        if (!transform.GetComponent<CapsuleCollider>().enabled)
            transform.GetComponent<CapsuleCollider>().enabled = true;

        if (isMergeTriggered == true && otherCharacterCollider != null)
        {
            TextMeshPro textMesh = otherCharacterCollider.transform.GetComponent<DragObject>().text;
            int textValue = int.Parse(textMesh.text);

            if (textValue * 2 == 2 || textValue * 2 == 4)
            {
                otherCharacterCollider.transform.localScale = new Vector3((float)(transform.localScale.x + 0.2), (float)(transform.localScale.y + 0.2),
                  (float)(transform.localScale.z + 0.2));
                otherCharacterCollider.transform.localPosition -= Vector3.up * 0.2f;
            }


            textMesh.text = (textValue * 2).ToString();

            transform.SetParent(null);
            transform.gameObject.SetActive(false);
            otherCharacterCollider.tag = characterTag;

            transform.tag = characterTag;
            otherCharacterCollider = null;

            GameManager.ourPower+= (textValue * 2 * 2 - 1) - (textValue * 2 - 1) * 2;
            Debug.Log("ourPower: " + GameManager.ourPower);
            PushMovement.SetSpeed();
            /*GameManager.speed += (float)(textValue * 2 * 2 - 1) / 10 - (float)(textValue * 2 - 1) * 2 / 10;
            GameManager.speed = Mathf.Clamp(GameManager.speed, -1, 1);*/

        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.tag = characterTag;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;

        if (transform.tag == characterTag)
            transform.tag = draggingObjectTag;

        /*if (transform.GetComponent<CapsuleCollider>().enabled)
            transform.GetComponent<CapsuleCollider>().enabled = false;*/
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
            if (character.level == other.GetComponent<DragObject>().character.level)
            {
                isMergeTriggered = true;
                otherCharacterCollider = other;
            }
            else
            {
                isMergeTriggered = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (transform.tag == draggingObjectTag && other.tag == characterTag)
        {
            isMergeTriggered = false;
        }
    }


}
