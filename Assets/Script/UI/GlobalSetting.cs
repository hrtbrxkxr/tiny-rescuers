using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetting : MonoBehaviour
{
    [SerializeField] Color highlightedColor;
    [SerializeField] Color textColor;
    [SerializeField] Color defaultColor;

    public Color HighlightedColor => highlightedColor;
    public Color TextColor => textColor;
    public Color DefaultColor => defaultColor;

    public static GlobalSetting i { get; private set; }

    private void Awake()
    {
        i = this;
    }
}
