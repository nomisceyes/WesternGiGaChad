using DG.Tweening;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private TextMeshPro _text;
    private RectTransform _body;

    private Sequence _currentAnimation;
    
    private Vector3 _targetBodyPosition;
    private Vector3 _startPosition;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        _body = GetComponent<RectTransform>();

        _targetBodyPosition = _body.anchoredPosition;
        _startPosition = _targetBodyPosition;

        _startPosition.y = -0.5f;

        _text.alpha = 0f;
    }

    public void Show(int value)
    {
        _text.text = value.ToString();
        
        _currentAnimation?.Kill();
        
        _currentAnimation = DOTween.Sequence();
        _body.anchoredPosition = _startPosition;

        _currentAnimation.Append(_body.DOAnchorPos(_targetBodyPosition + new Vector3(0, 0.5f, 0), 0.25f))
            .Join(_text.DOFade(1f, 0.2f))
            .Join(_text.transform.DOScale(1.5f, 0.25f))
            .Append(_body.DOAnchorPos(_targetBodyPosition, 0.25f).SetEase(Ease.OutCirc))
            .Join(_text.transform.DOScale(1f, 0.35f))
            .Append(_text.DOFade(0f, 0.4f));
    }

    public void Hide()
    {
        _currentAnimation?.Kill();
        gameObject.SetActive(false);
    }
}