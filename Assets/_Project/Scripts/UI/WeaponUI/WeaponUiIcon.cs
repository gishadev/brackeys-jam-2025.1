using BrackeysJam._Project.Scripts.UI;
using BrackeysJam.Weapons;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BrackeysJam.UI.WeaponUI
{
    public class WeaponUiIcon : UiIcon<WeaponDataSO>
    {
        [SerializeField, Required] private TextMeshProUGUI _label;
        [SerializeField, Required] private Image _icon;

        public override void SetIconParams(WeaponDataSO buttonParams)
        {
            _label.text = buttonParams.Name;
            _icon.sprite = buttonParams.Icon;
        }
    }
}