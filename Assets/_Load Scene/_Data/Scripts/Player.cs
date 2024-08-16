using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HieuDev
{
    public class Player : MonoBehaviour
    {
        public GameObject player; // Đối tượng mà bạn muốn lưu và load vị trí
        private SerializationAndEncryption saveLoadPosition;

        void Start()
        {
            saveLoadPosition = new SerializationAndEncryption();

            // Tải vị trí khi khởi động
            Vector3 loadedPosition = saveLoadPosition.LoadPosition();
            if (loadedPosition != Vector3.zero)
            {
                player.transform.position = loadedPosition;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                // Lưu vị trí hiện tại của đối tượng khi nhấn phím S
                saveLoadPosition.SavePosition(player.transform.position);
            }
        }
    }
}
