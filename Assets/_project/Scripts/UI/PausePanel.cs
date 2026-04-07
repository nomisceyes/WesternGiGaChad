using UnityEngine;

public class PausePanel : Panel
{
    public void Pause(bool @is)
    {
        Time.timeScale = @is ? 0 : 1;
    }
}