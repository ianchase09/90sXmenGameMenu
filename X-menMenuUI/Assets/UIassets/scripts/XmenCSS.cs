using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;




public class XmenCSS : MonoBehaviour
{
    public static XmenCSS instance;

    public List<Character> characters = new List<Character>();

    public GameObject charCellPrefab;
    public GameObject defaultBG;
    public Transform playerSlotsContainer;
    public Texture2D xCursor;
    public Character confirmedCharacter;
    public GameObject StartButton;
 






    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.SetCursor(xCursor, Vector2.zero, CursorMode.ForceSoftware);
        foreach (Character character in characters)
        {
            SpawnCharacterCell(character);


        }
    }

    private void SpawnCharacterCell(Character character)
    {
        GameObject charCell = Instantiate(charCellPrefab, transform);
        charCell.name = character.characterName;

        Image artwork = charCell.transform.Find("artwork").GetComponent<Image>();
        TextMeshProUGUI name = charCell.transform.Find("nameplate").GetComponentInChildren<TextMeshProUGUI>();

        artwork.sprite = character.characterSprite;
        name.text = character.characterName;
    }

    public void ShowCharacterInSlot(int player, Character character)
    {
        bool nullChar = (character == null);


        Color alpha = nullChar ? Color.clear : Color.white;
        Sprite artwork = nullChar ? null : character.characterSprite;
        string name = nullChar ? string.Empty : character.characterName;


        Transform slot = playerSlotsContainer.GetChild(player);

        Transform slotArtwork = slot.Find("artwork");
        

        Sequence s = DOTween.Sequence();

        s.Append(slotArtwork.DOLocalMoveX(-300, .1f));
        s.AppendCallback(() => slotArtwork.GetComponent<Image>().color = alpha);
        s.AppendCallback(() => slotArtwork.GetComponent<Image>().sprite = artwork);
        s.Append(slotArtwork.DOLocalMoveX(300, 0));
        s.Append(slotArtwork.DOLocalMoveX(0, .1f));

        slotArtwork.GetComponent<Image>().color = alpha;

        slot.Find("name").GetComponent<TextMeshProUGUI>().text = name;
        slot.Find("artwork").GetComponent<Image>().sprite = artwork;

        if (confirmedCharacter == true)
        {
            slot.Find("artwork").GetComponent<Image>().sprite = artwork;

        }
    }
    public void ConfirmCharacter(int player, Character character)
    {
        if (confirmedCharacter == null)
        {
            confirmedCharacter = character;
            playerSlotsContainer.GetChild(player).DOPunchPosition(Vector3.down * 10, .3f, 10, 1);
            StartButton.SetActive(true);
           
        }
    }
    public void CancelCharacter(int player, Character character)
    {
        if (confirmedCharacter == null)
        {
            confirmedCharacter = character;
            playerSlotsContainer.GetChild(player).DOPunchPosition(Vector3.left * 10, .3f, 10, 1);

        }
    }
}