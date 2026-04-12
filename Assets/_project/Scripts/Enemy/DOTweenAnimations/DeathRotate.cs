using DG.Tweening;
using UnityEngine;

public class DeathRotate : MonoBehaviour
{
    public ParticleSystem ps;
    
    public void Trigger(GameObject obj)
    {
        Sequence animation = DOTween.Sequence();

        Vector3 pos = obj.transform.position;
        Quaternion rot = obj.transform.rotation;

        animation
            .Append(obj.transform.DORotate(new Vector3(0, 120 * 3, 0), 0.6f, RotateMode.LocalAxisAdd))
            .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x, 5f, obj.transform.position.z), 0.3f))
            .AppendCallback(() =>
            {
                //GameObject g = new GameObject("---VFX---");
                //Instantiate(g, obj.transform.position, obj.transform.rotation);
                ParticleSystem p = Instantiate(ps, obj.transform.position, obj.transform.rotation);
                //g.transform.localScale = ps.transform.parent.localScale;
                //p.gameObject.transform.SetParent(g.transform);
                p.gameObject.transform.localScale = ps.transform.localScale;

                p.Play();
                obj.SetActive(false);
            });
        // .AppendInterval(1.5f)
        // .Append(obj.transform.DOMove(pos, 0.3f))
        // .Join(obj.transform.DORotate(rot.eulerAngles, 0.3f))
        // .JoinCallback(() => obj.SetActive(true));
    }
}