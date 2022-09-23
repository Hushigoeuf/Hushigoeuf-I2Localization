using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if I2
using I2.Loc;
#endif

namespace Hushigoeuf
{
    /// <summary>
    /// Этот компонент можно добавлять для инспектора, чтобы определить текст из I2 Localization.
    /// Источник: https://assetstore.unity.com/packages/tools/localization/i2-localization-14884
    /// </summary>
    [Serializable]
    public struct I2LocalizationText
    {
#if UNITY_EDITOR && ODIN_INSPECTOR
        [ValueDropdown(nameof(EditorGetDropdownValues))]
#endif
        public string Key;

        public string Text
        {
            get
            {
                if (!string.IsNullOrEmpty(Key))
                {
#if I2
                    return LocalizationManager.GetTranslation(Key, true, 0, true, true);
#endif
                }

                return "not_found";
            }
        }

        public void To(Text target)
        {
            target.text = Text;
        }

        public void To(TMP_Text target)
        {
            target.SetText(Text);
        }

#if UNITY_EDITOR && ODIN_INSPECTOR
        private List<string> _editorKeys;

        [OnInspectorInit]
        private void EditorInitKeys()
        {
            if (_editorKeys == null)
                _editorKeys = new List<string>();
            else _editorKeys.Clear();

#if I2
            _editorKeys = LocalizationManager.GetTermsList();
#endif
        }

        private List<string> EditorGetDropdownValues() => _editorKeys;
#endif
    }

    public static class I2LocalizationTextExtensions
    {
        public static void HGSetText(this Text target, I2LocalizationText text)
        {
            text.To(target);
        }

        public static void HGSetText(this TMP_Text target, I2LocalizationText text)
        {
            text.To(target);
        }
    }
}