using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopupAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _opacityCurve;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private AnimationCurve _heightCurve;

    [SerializeField] private DamagePopup _damagePopup;
    [SerializeField] private TextMeshPro _text;

    private Color _defaultColor = new Color(1, 1, 1, 1);

    private float time = 0f;

    private void OnEnable() =>   
        _damagePopup.AnimPopup += AnimationPopup;    

    private void OnDisable() =>   
        _damagePopup.AnimPopup -= AnimationPopup;    

    public void AnimationPopup(Vector3 position) =>    
        StartCoroutine(AnimPopup(position));   

    public IEnumerator AnimPopup(Vector3 position)
    {
        while (time < 1f)
        {
            time += Time.deltaTime;

            _text.color = new Color(1, 1, 1, _opacityCurve.Evaluate(time));
            transform.localScale = Vector3.one * _scaleCurve.Evaluate(time);
            transform.position = position + new Vector3(0, _heightCurve.Evaluate(time), 0);

            yield return null;
        }

        time = 0f;
        _text.color = _defaultColor;
    }
}