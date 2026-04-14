using UnityEngine;

public class Click : MonoBehaviour
{
    public void pu()
    {
        Global.AudioManager.PlaySound(Res.Audio.MouseClickSound);
    }
}