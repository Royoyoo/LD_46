using System;
using UnityEngine;

public enum ContainerType
{
    Living,
    Aid,
    Resurrection,
    Charon
}

public class SoulsContainer : MonoBehaviour, ISoulsSource
{
    public ContainerType Type;
    public float soulsCount = 0;
    public float collectSpeed = 2;

    [Range(-1, 10000)]
    public int MaxSoulsCount = 50;

    public int MaxVisibleSoulsCount = 50;

    public bool NeedVisible => soulsCount < MaxVisibleSoulsCount;

    public float SoulsCount
    {
        get => soulsCount;
        set
        {
            switch (Type)
            {
                case ContainerType.Living:
                    Data.player.DeadShorePopulation = value;
                    break;
                case ContainerType.Aid:
                    Data.player.HellDoorPopulation = value;
                    break;
                case ContainerType.Resurrection:
                    break;
                case ContainerType.Charon:
                    break;
                default:
                    break;
            }
            soulsCount = value;
        }
    }

    public float CollectSpeed { get => collectSpeed; set => collectSpeed = value; }

    public event Action<float> OnSoulsCountChange;

    void Start()
    {
        switch (Type)
        {
            case ContainerType.Living:
                soulsCount = Data.player.DeadShorePopulation;
                break;
            case ContainerType.Aid:
                soulsCount = Data.player.HellDoorPopulation;
                break;
            case ContainerType.Resurrection:
                break;
            case ContainerType.Charon:
                break;
            default:
                break;
        }
    }

    public bool Add()
    {
        if (CanAdd())
        {
            soulsCount++;
            // Debug.Log(SoulsCount);
            OnSoulsCountChange?.Invoke(soulsCount);
            return true;
        }

        return false;
    }

    public bool CanAdd()
    {
        return MaxSoulsCount == -1 || soulsCount < MaxSoulsCount;
    }

    public bool Remove()
    {
        if (CanRemove())
        {
            soulsCount--;
            OnSoulsCountChange?.Invoke(soulsCount);
            return true;
        }
        return false;
    }

    public void Remove(int count)
    {      
        for (int i = 0; i < count; i++)
        {
            if (CanRemove())
            {
                soulsCount--;
                OnSoulsCountChange?.Invoke(soulsCount);               
            }           
        }       
    }

    public bool CanRemove()
    {
        return soulsCount > 0;
    }
}
