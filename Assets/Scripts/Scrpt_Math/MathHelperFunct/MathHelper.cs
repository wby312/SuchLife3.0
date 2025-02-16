using UnityEngine;


//https://stackoverflow.com/questions/13462001/ease-in-and-ease-out-animation-formula
//Shout out to them for the Bezier in out blend
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

    public static float BezierBlend(float t)
    {
        return t * t * (3.0f - 2.0f * t);
    }

    //Input a [0,1] at 0.5 will give you a value of 2 at the rest will smooth out to a value of 2
    public static float BezierBuff(float t)
    {
        return t <= 0.5 ? 32f * t * (1f - t) + 1f : 9;
    }
}
