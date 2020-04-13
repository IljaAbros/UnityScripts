using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtensions {
    //bool
    public static float Percent(this int v) {
        return v * 0.01f;
    }

    public static float Percent(this float v) {
        return v * 0.01f;
    }

    public static bool Switch(this bool b) {
        return !b;
    }
    public static float OneMinus(this float number) {
        return 1 - number;
    }
    
    public static float Total(this float[] array) {
        float total = 0;
        foreach (float i in array) {
            total += i;
        }
        return total;
    }
    public static int Total(this int[] array) {
        int total = 0;
        foreach (int i in array) {
            total += i;
        }
        return total;
    }

    public static float Squared(this float value) {
        return value * value;
    }

    public static bool IsEven(this int num) {
        return (Mathf.Abs(num % 2) == 0);
    }

    public static int ClampCycle(this int value, int min, int max) {
        int diff = Mathf.Abs(min) + Mathf.Abs(max);

        if(min == max) {
            return min;
        }

        if (min >= max) {
            return min;
        }

        while (value >= max) {
            value -= diff;
        }

        while (value < min) {
            value += diff;
        }

        return value;
    }
    public static float ClampCycle(this float value, float min, float max) {
        float diff = Mathf.Abs(min) + Mathf.Abs(max);

        if (min == max) {
            return min;
        }

        if (min >= max) {
            return min;
        }

        while (value >= max) {
            value -= diff;
        }

        while (value < min) {
            value += diff;
        }

        return value;
    }

    public static int SafeDivide(this int value, int divider) {
        if (divider == 0) { return 0; }
        return value / divider;
    }

    public static float SafeDivide(this float value, float divider) {
        if (divider == 0) { return 0; }
        return value / divider;
    }

    public static float Snap(this float value, float step) {
        if(step <= 0) { return value; }

        return Mathf.Round(value / step) * step;
    }




}
