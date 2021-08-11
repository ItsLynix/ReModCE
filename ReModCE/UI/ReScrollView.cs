﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReModCE.UI
{
    internal class ReScrollView : UIElement
    {
        private static GameObject _reportWorldButton;
        private static GameObject ReportWorldButton
        {
            get
            {
                if (_reportWorldButton == null)
                {
                    _reportWorldButton = GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/ReportWorldButton");
                }

                return _reportWorldButton;
            }
        }

        private readonly Text _logText;

        public ReScrollView(string name, Vector2 pos, Transform parent) : base(ReportWorldButton, parent, pos, $"ScrollView_{name}")
        {
            Object.DestroyImmediate(GameObject.GetComponentsInChildren<Image>(true).First(a => a.transform != GameObject.transform));
            Object.DestroyImmediate(GameObject.GetComponent<UiTooltip>());
            Object.DestroyImmediate(GameObject.GetComponent<ButtonReaction>());

            GameObject.AddComponent<RectMask2D>();

            var button = GameObject.GetComponent<Button>();
            var originalColors = button.colors;
            button.colors = new ColorBlock
            {
                colorMultiplier = originalColors.colorMultiplier,
                disabledColor = originalColors.normalColor,
                fadeDuration = originalColors.fadeDuration,
                highlightedColor = originalColors.normalColor,
                pressedColor = originalColors.normalColor,
                selectedColor = originalColors.normalColor
            };
            button.interactable = false;

            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1680f);
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1260F);
            RectTransform.ForceUpdateRectTransforms();

            _logText = GameObject.GetComponentInChildren<Text>();

            var content = new GameObject("Content", new UnhollowerBaseLib.Il2CppReferenceArray<Il2CppSystem.Type>(new Il2CppSystem.Type[1] { Il2CppType.Of<RectTransform>() }));
            var contentRect = content.GetComponent<RectTransform>();
            contentRect.SetParent(RectTransform);

            var scrollRect = GameObject.AddComponent<ScrollRect>();
            scrollRect.content = _logText.GetComponent<RectTransform>();
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
            scrollRect.horizontal = false;
            scrollRect.decelerationRate = 0.03f;
            scrollRect.scrollSensitivity = 3;

            //_logText.GetComponent<RectTransform>().SetParent(contentRect);

            _logText.fontSize = (int)(_logText.fontSize * 0.75f);
            _logText.transform.localPosition += new Vector3(0f, -140f);
            _logText.alignment = TextAnchor.LowerLeft;
            _logText.verticalOverflow = VerticalWrapMode.Overflow;
            _logText.text = "";

        }

        public void AddText(string message)
        {
            _logText.text += message;
        }
    }
}
