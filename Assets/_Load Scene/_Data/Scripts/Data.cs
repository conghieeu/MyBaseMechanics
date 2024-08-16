using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HieuDev
{
    public class Data : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
