using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Bound
{
    [SerializeField]
    private Vector2 _lt;
    [SerializeField]
    private Vector2 _rb;

    public Bound()
    {}


    public Bound(Vector2 lt, Vector2 rb)
    {
        _lt = lt;
        _rb = rb;
    }
    
    public Vector2 Center => new Vector2(_lt.x + (_rb.x - _lt.x) * 0.5f, _lt.y + (_rb.y - _lt.y) * 0.5f);
    public Vector2 Size => new Vector2(_rb.x - _lt.x, _rb.y - _lt.y);

    public Vector2 GetRandomPos()
    {
        float x = Random.Range(_lt.x, _rb.x);
        float y = Random.Range(_lt.y, _rb.y);
        return new Vector2(x, y);
    }
}
public static class Define
{
    
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }

    }

    private static Camera _mainCam;


    public static Vector2 MousePos => MainCam.ScreenToWorldPoint(Input.mousePosition);

    public static T GetRandomEnum<T>(int startPos = 0, int? length = null) where T : System.Enum
    {
        Array values = Enum.GetValues(typeof(T));
        if (length == null)
        {
            return (T)values.GetValue(Random.Range(startPos, values.Length));
        }
        else
            return (T)values.GetValue(Random.Range(startPos, length.Value));
    }
    public static T StringToEnum<T>(this string enumString) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumString);
    }
    
    struct Shell<T> where T : Enum
    {
        public int Int;
        public T Enum;
    }

    public static int EnumToInt<T>(T e) where T : Enum
    {
        Shell<T> s;
        s.Enum = e;
        unsafe
        {
            int* pi = &s.Int;
            pi += 1;

            return *pi;
        }
    }

    public static T IntToEnum<T>(int value) where T : Enum
    {
        var s = new Shell<T>();
        unsafe
        {
            int* pi = &s.Int;
            pi += 1;
            *pi = value;

        }
        return s.Enum;
    }
    

    public static T GetEnumValue<T>(this Enum e) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        return (T)(object)e;
    }

    public static Bound GetRandomBound(params Bound[] bounds)
    {

        float random = Random.Range(0f, 1f);
        float sizeAmount = 0f;

        float currentSizeRandom = 0f;

        foreach (Bound bound in bounds)
        {
            sizeAmount += bound.Size.magnitude;
        }
        foreach (Bound bound in bounds)
        {
            if (random <= bound.Size.magnitude / sizeAmount + currentSizeRandom)
            {
                return bound;
            }
            else
            {
                currentSizeRandom += bound.Size.magnitude / sizeAmount;
            }
        }

        int rand = Random.Range(0, bounds.Length);
        return bounds[rand];
    }

}



namespace System.Runtime.CompilerServices
{
    public static class IsExternalInit
    {}
}
