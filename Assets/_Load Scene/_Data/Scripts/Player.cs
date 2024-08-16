using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HieuDev
{
    public class Player : MonoBehaviour
    {
        [SerializeField] PlayerData playerData = new PlayerData();

        void Start()
        {
            playerData.name = "HieuDev"; 
            playerData.position = transform.position;

            SerializationAndEncryption.Instance.GameData.player = playerData;
        }

        void LoadPlayerData()
        {

        }

        void SavePlayerData()
        {

        }
    }
}
