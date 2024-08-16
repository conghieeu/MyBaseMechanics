using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HieuDev
{
    public class Player : MonoBehaviour
    {
        [SerializeField] SerializationAndEncryption serializationAndEncryption;
        [SerializeField] PlayerData playerData = new PlayerData();

        void Start()
        {
            playerData.name = "HieuDev";
            playerData.position = transform.position;

            serializationAndEncryption._gameData.player = playerData;

            serializationAndEncryption.SaveGameData(serializationAndEncryption._gameData);
        }

    }
}
