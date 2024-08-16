using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HieuDev
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private Text m_loadingCountingTxt;
        [SerializeField] private Image m_loadingFilled;

        private void Start()
        {
            m_loadingFilled.fillAmount = 0.5f;
            LoadingScene.OnLoading += UpdateUI;
        }

        public void UpdateUI(float loadingProgress)
        {
            if (m_loadingCountingTxt)
            {
                m_loadingCountingTxt.text = $"Loading.. {(loadingProgress * 100).ToString("f0")}%"; 
            }

            if (m_loadingFilled)
            {
                m_loadingFilled.fillAmount = loadingProgress;
            }
        }
    }
}
