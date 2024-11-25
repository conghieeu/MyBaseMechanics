using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CuaHang.UI
{
    [RequireComponent(typeof(Button))]
    public class BtnSwitch : MonoBehaviour
    {
        [Header("Active Object")]
        [SerializeField] bool isOpen = false;
        [SerializeField] List<GameObject> ListPanelTrigger;

        Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SwitchPanel();
        }

        private void SwitchPanel()
        {
            isOpen = !isOpen;

            foreach (GameObject panel in ListPanelTrigger)
            {
                if (panel != null)
                {
                    panel.SetActive(isOpen); // Bật đối tượng
                }
            }
        }

    }

}
