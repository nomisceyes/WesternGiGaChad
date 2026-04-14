using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class SoundPanel : Panel
{
    [SerializeField] private CanvasGroup _bodyAlphaGroup;
    [SerializeField] private RectTransform _body;
    [SerializeField] private List<Slider> _sliders;

    private Vector3 _targetBodyPosition;
    private Vector3 _startShift;
    private Sequence _animation;

    private void Awake()
    {
        _targetBodyPosition = _body.anchoredPosition;
        _startShift = new Vector2(_targetBodyPosition.x, -Screen.height / 2);
    }

    public void OnClick()
    {
        if (gameObject.activeInHierarchy)
            HideAnimation();
        else
            ShowAnimation();
    }

    private void ShowAnimation()
    {
        KillCurrentAnimation();

        _animation = DOTween.Sequence();
        gameObject.SetActive(true);

        _animation
            .Append(_body.DOAnchorPos(_targetBodyPosition, 1f).From(_startShift))
            .Join(_bodyAlphaGroup.DOFade(1, 1f).From(0f));

        _sliders.ForEach(slider =>
            _animation.Append(slider.transform
                .DOScale(new Vector3(1.6f, 1, 1), 0.3f).From(0)
                .SetEase(Ease.OutBounce)));
    }

    public override void HideAnimation()
    {
        _animation = DOTween.Sequence();

        _animation
            .Append(_bodyAlphaGroup.DOFade(0, 1f).From(1))
            .Join(_body.DOAnchorPos(_startShift, 1f).From(_targetBodyPosition))
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void KillCurrentAnimation()
    {
        if (_animation is { active: true })
            _animation.Kill();
    }
}