using System;

public class EaseUtil
{
    public static double EaseInSine(float x)
    {
        return 1 - Math.Cos(x * Math.PI / 2);
    }

    public static double EaseOutSine(float x)
    {
        return Math.Sin(x * Math.PI / 2);
    }

    public static double EaseInOutSine(float x)
    {
        return -(Math.Cos(Math.PI * x) - 1) / 2;
    }

    public static double EaseInQuad(float x)
    {
        return x * x;
    }

    public static double EaseOutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    public static double EaseInOutQuad(float x)
    {
        return x < 0.5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2;
    }

    public static double EaseInQuart(float x)
    {
        return Math.Pow(x, 4);
    }

    public static double EaseOutQuart(float x)
    {
        return 1 - Math.Pow(1 - x, 4);
    }

    public static double EaseInOutQuart(float x)
    {
        return x < 0.5 ? 8 * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 4) / 2;
    }

    public static double EaseInCubic(float x)
    {
        return Math.Pow(x, 3);
    }

    public static double EaseOutCubic(float x)
    {
        return 1 - Math.Pow(1 - x, 3);
    }

    public static double EaseInOutCubic(float x)
    {
        return x < 0.5f ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2;
    }

    public static double EaseInQuint(float x)
    {
        return Math.Pow(x, 5);
    }

    public static double EaseOutQuint(float x)
    {
        return 1 - Math.Pow(1 - x, 5);
    }

    public static double EaseInOutQuint(float x)
    {
        return x < 0.5f ? 16 * Math.Pow(x, 5) : 1 - Math.Pow(-2 * x + 2, 5) / 2;
    }

    public static double EaseInExpo(float x)
    {
        return x == 0 ? 0 : Math.Pow(2, 10 * x - 10);
    }

    public static double EaseOutExpo(float x)
    {
        return x == 1 ? 1 : 1 - Math.Pow(2, -10 * x);
    }

    public static double EaseInOutExpo(float x)
    {
        return x == 0 ? 0 :
                x == 1 ? 1 :
                x < 0.5 ? Math.Pow(2, 20 * x - 10) / 2 :
                (2 - Math.Pow(2, -20 * x + 10)) / 2;
    }

    public static double EaseInCirc(float x)
    {
        return 1 - Math.Sqrt(1 - Math.Pow(x, 2));
    }

    public static double EaseOutCirc(float x)
    {
        return Math.Sqrt(1 - Math.Pow(x - 1, 2));
    }

    public static double EaseInOutCirc(float x)
    {
        return x < 0.5 ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2 : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;
    }

    public static double EaseInBack(float x)
    {
        double c1 = 1.70158;
        double c3 = c1 + 1;

        return c3 * x * x * x - c1 * x * x;
    }

    public static double EaseOutBack(float x)
    {
        double c1 = 1.70158;
        double c3 = c1 + 1;

        return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
    }

    public static double EaseInOutBack(float x)
    {
        double c1 = 1.70158;
        double c2 = c1 * 1.525;

        return x < 0.5 ?
                Math.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2) / 2 :
                (Math.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
    }

    public static double EaseInElastic(float x)
    {
        double c4 = 2 * Math.PI / 3;
        return x == 0 ? 0 : x == 1 ? 1 : -Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * c4);
    }

    public static double EaseOutElastic(float x)
    {
        double c4 = 2 * Math.PI / 3;
        return x == 0 ? 0 : x == 1 ? 1 : Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * c4) + 1;
    }

    public static double EaseInOutElastic(float x)
    {
        double c5 = 2 * Math.PI / 4.5;
        return x == 0 ? 0 :
                x == 1 ? 1 :
                x < 0.5 ? -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 :
                Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * c5) / 2 + 1;
    }

    public static double EaseInBounce(float x)
    {
        return 1 - EaseOutBounce(1 - x);
    }

    public static double EaseOutBounce(float x)
    {
        double n1 = 7.5625;
        double d1 = 2.75;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= (float)(1.5 / d1)) * x + 0.75;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= (float)(2.25 / d1)) * x + 0.9375;
        }
        else
        {
            return n1 * (x -= (float)(2.625 / d1)) * x + 0.984375;
        }
    }

    public static double EaseInOutBounce(float x)
    {
        return x < 0.5 ? (1 - EaseOutBounce(1 - 2 * x)) / 2 : (1 + EaseOutBounce(2 * x - 1)) / 2;
    }
}
