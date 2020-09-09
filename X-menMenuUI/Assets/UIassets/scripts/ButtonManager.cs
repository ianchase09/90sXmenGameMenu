using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GraphicRaycaster gr;
    public Transform currentCharacter;
    public Transform defaultBG;
    public GameObject token;
    public Transform playerSlotsContainer;
    




    void Start()
    {
        gr = GetComponent<GraphicRaycaster>();
        XmenCSS.instance.ShowCharacterInSlot(0, null);
        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(eventData, results);

        print(results[0].gameObject.name);
        if (results.Count > 0)
        {
            Transform raycastCharacter = results[0].gameObject.transform;

            if (raycastCharacter != currentCharacter)
            {

                SetCurrentCharacter(raycastCharacter);
            }
        }
        else
        {
            if (currentCharacter != null)
            {
                SetCurrentCharacter(null);
            }
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            XmenCSS.instance.ConfirmCharacter(0, XmenCSS.instance.characters[currentCharacter.GetSiblingIndex()]);
        }
        else
        {
            currentCharacter = null;
            XmenCSS.instance.confirmedCharacter = null;
        }
    }





    void SetCurrentCharacter(Transform t)
    {
        currentCharacter = t;
        if (t != null)
        {
            int index = t.GetSiblingIndex();
            Character character = XmenCSS.instance.characters[index];
            XmenCSS.instance.ShowCharacterInSlot(0, character);
        }
        else
        {
            XmenCSS.instance.ShowCharacterInSlot(0, null);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentCharacter != null)
            {
                XmenCSS.instance.ConfirmCharacter(0, XmenCSS.instance.characters[currentCharacter.GetSiblingIndex()]);
                token.SetActive(true);
            }
        }

        //CANCEL
        if (Input.GetKeyDown(KeyCode.X))
        {
            XmenCSS.instance.confirmedCharacter = null;
            token.SetActive(false);
            XmenCSS.instance.StartButton.SetActive(false);
            XmenCSS.instance.CancelCharacter(0, XmenCSS.instance.characters[currentCharacter.GetSiblingIndex()]);

        }
        
    }
}
