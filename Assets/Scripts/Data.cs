using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public Player DefaultPlayer;

    public Player PlayerSO;
    public Consts ConstsSO;

    public static Player player
    {
        get
        {
            if (instance)
                return instance.PlayerSO;
            else
            {
                instance = FindObjectOfType<Data>();
                return instance.PlayerSO;
            }
        }
        private set => instance.PlayerSO = value;
    }            
    public static Consts consts
    {
        get
        {
            if (instance)
                return instance.ConstsSO;
            else
            {
                instance = FindObjectOfType<Data>();
                return instance.ConstsSO;
            }
        }
        private set => instance.ConstsSO = value;
    }   

    public static Data instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            player = Instantiate(DefaultPlayer);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
