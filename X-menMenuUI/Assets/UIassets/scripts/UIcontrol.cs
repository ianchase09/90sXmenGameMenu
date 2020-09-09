using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIcontrol : MonoBehaviour
{
    public RectTransform Logo, CharacterBG, TopMenuBar, LegalText;
    public GameObject pressStartButton;
    public Texture2D xCursor;

    [Range(1.0f, 10.0f), SerializeField]
    private float _moveDuration = 1.0f;

    [SerializeField]
    private Ease _moveEase = Ease.Linear;

    [SerializeField]
    private DoTweenType _doTweenType = DoTweenType.MovementOneWay;



    private enum DoTweenType
    {
        MovementOneWay
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(xCursor, Vector2.zero, CursorMode.ForceSoftware);
        if (_doTweenType == DoTweenType.MovementOneWay)
        {
            DOTween.Sequence()
            .Append(Logo.DOAnchorPos(new Vector2(0f, -225f), _moveDuration).SetEase(EaseFactory.StopMotion(2, _moveEase)))
            .Append(LegalText.DOAnchorPos(new Vector2(0f, -350f), _moveDuration));
        }



        StartCoroutine(ExecuteAfterTime());
        StartCoroutine(HoldEndLegalText());
        StartCoroutine(HoldShowButton());

    }
    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(2f);
        FadeIn(3f);
    }
    IEnumerator HoldEndLegalText()
    {
        yield return new WaitForSeconds(8f);
        EndLegalText();
    }
    IEnumerator HoldShowButton()
    {
        yield return new WaitForSeconds(10f);
        ShowButton();
    }

    public void ShowButton()
    {
        pressStartButton.SetActive(true);
    }
    public void EndLegalText()
    {
        DOTween.Sequence()
            .Append(LegalText.DOAnchorPos(new Vector2(0f, -850f), _moveDuration).SetEase(EaseFactory.StopMotion(10, _moveEase)));
       
        
    }
    public void FadeIn(float duration)
    {
        Fade(0, 1, duration);

    }
    public void Fade(float startOpacity, float endOpacity, float duration)
    {
        var graphics = CharacterBG.GetComponentsInChildren<Graphic>();

        foreach (var graphic in graphics)
        {
            var startColor = graphic.color;
            startColor.a = startOpacity;
            graphic.color = startColor;

            var endColor = graphic.color;
            endColor.a = endOpacity;
            graphic.DOColor(endColor, duration);
        }
        }
  
    }
