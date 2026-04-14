using UnityEngine;

public class Click : MonoBehaviour
{
    private void OnMouseDown()
    {
        Global.AudioManager.PlaySound(Res.Audio.MouseClickSound);
    }
}