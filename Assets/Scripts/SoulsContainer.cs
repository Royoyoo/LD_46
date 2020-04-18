using System;
using UnityEngine;

public class SoulsContainer : MonoBehaviour
{
    public int SoulsCount = 0;

    [Range(-1, 10000)]
    public int MaxSoulsCount = 50;

    public int MaxVisibleSoulsCount = 50;

    public bool NeedVisible => SoulsCount < MaxVisibleSoulsCount;

    public event Action<int> OnSoulsCountChange;

    public bool Add()
    {
        if (CanAdd())
        {
            SoulsCount++;
            // Debug.Log(SoulsCount);
            OnSoulsCountChange?.Invoke(SoulsCount);
            return true;
        }

        return false;
    }

    public bool CanAdd()
    {
        return MaxSoulsCount == -1 || SoulsCount < MaxSoulsCount;
    }

    public bool Remove()
    {
        if (CanRemove())
        {
            SoulsCount--;
            OnSoulsCountChange?.Invoke(SoulsCount);
            return true;
        }
        return false;
    }

    public bool CanRemove()
    {
        return SoulsCount > 0;
    }
}
