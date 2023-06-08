using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts
{
    public class WeaponOptions : MonoBehaviour
    {
        public Dropdown WeaponDropdown;
        public List<GunSO> WeaponsList;
        public PlayerScripts.PlayerOptions PlayerOptions;
        private void Start()
        {
            WeaponsList = LevelManager.Instance.WeaponList;
            FillWeaponsDropDown();
        }
        private void FillWeaponsDropDown()
        {
            WeaponDropdown.ClearOptions();
            List<string> WeaponNames = new List<string>();
            foreach ( GunSO weapon in WeaponsList)
            {
                WeaponNames.Add(weapon.name);
            }
            WeaponDropdown.AddOptions(WeaponNames);
        }
        public void OnWeaponDDChange(int index)
        {
            PlayerOptions.CurrentWeapon = WeaponsList[index];
            LevelManager.Instance.CurrentPlayer.GetComponent<PlayerScripts.PlayerShootScript>().SwitchWeapons(WeaponsList[index]);
        }
    }
}
