using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
    public enum MenuType
    {
        Hud,
        Pause,
        PlayerOptions,
        WeaponOptions,
        GameOptions
    }
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; set; }

        public List<Menu> Menus = new List<Menu>();

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateMenu(MenuType Type)
        {
            foreach (Menu menu in Menus)
            {
                if (menu.Menutype == Type)
                {
                    menu.gameObject.SetActive(true);
                }
                else
                {
                    menu.gameObject.SetActive(false);
                }
            }
        }
        public void TogglePauseMenu(bool IsPaused)
        {
            if (IsPaused)
            {
                UpdateMenu(MenuType.Pause);
            }
            else
            {
                UpdateMenu(MenuType.Hud);
            }
        }
        public void UnityENUMFIX(string enumstring)
        {
            UpdateMenu((MenuType)System.Enum.Parse(typeof(MenuType), enumstring));
        }
    }
}
