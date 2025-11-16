namespace osu_Bridge.Core.Utils;

public static class ArrayUtils
{
    public static bool IsValidIndex(int arraysCount, int index)
        => index < arraysCount && index >= 0;
}
