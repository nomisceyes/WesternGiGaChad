using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class DeathRotate : MonoBehaviour
{
    private Sequence _animation;
    private ParticleSystem _ps;

    private void Start() =>
        _ps = Global.VFX.GhostDeathVFX;
    
    public async UniTask TriggerAsync(GameObject obj, CancellationToken token = default)
    {
        _animation = DOTween.Sequence();

        Vector3 targetPosition = new(obj.transform.position.x, 5f, obj.transform.position.z);
        var tcs = new UniTaskCompletionSource();

        _animation
            .Append(obj.transform.DORotate(new Vector3(0, 120 * 3, 0), 0.6f, RotateMode.LocalAxisAdd))
            .Join(obj.transform.DOMove(targetPosition, 0.3f))
            .AppendCallback(() =>
            {
                _ps.transform.SetPositionAndRotation(targetPosition, obj.transform.rotation);
                _ps.Play();
                tcs.TrySetResult();
            });
        
        await tcs.Task;
    }
}