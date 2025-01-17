using UnityEngine;

public class MathHelper
{
    public static int ToIntPercent(float val)
    {
        return Mathf.RoundToInt(val * 100);
    }

    public static float FloatToAudioLog(float val)
    {
        return val == 0f ? -80f : Mathf.Log10(val)*20;
    }
}
