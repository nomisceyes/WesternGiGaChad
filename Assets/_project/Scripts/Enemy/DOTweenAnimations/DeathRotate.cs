using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class DeathRotate : MonoBehaviour
{
    public async UniTask TriggerAsync(GameObject obj, CancellationToken token = default)
    {
        Sequence animation = DOTween.Sequence();
        ParticleSystem ps = Global.VFX.GhostDeathVFX;

        Vector3 targetPosition = new(obj.transform.position.x, 5f, obj.transform.position.z);
        var tcs = new UniTaskCompletionSource();

        animation
            .Append(obj.transform.DORotate(new Vector3(0, 120 * 3, 0), 0.6f, RotateMode.LocalAxisAdd))
            .Join(obj.transform.DOMove(targetPosition, 0.3f))
            .AppendCallback(() =>
            {
                // Global.VFX.GhostDeathVFX.transform.SetPositionAndRotation(targetPosition, obj.transform.rotation);
                // Global.VFX.GhostDeathVFX.Play();

                ps.transform.SetPositionAndRotation(targetPosition, obj.transform.rotation);
                ps.Play();
                //obj.SetActive(false);
                tcs.TrySetResult();
            });
        
        await tcs.Task;
    }
}