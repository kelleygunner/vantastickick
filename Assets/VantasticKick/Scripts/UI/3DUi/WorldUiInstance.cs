using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WorldUiInstance : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMesh;

    public void Init(string text, Action<WorldUiInstance> onComplete)
    {
        _textMesh.text = text;
        var color = _textMesh.color;
        color.a = 1f;
        _textMesh.color = color;
        _textMesh.DOFade(0f, 2f).onComplete += () =>
        {
            onComplete?.Invoke(this);
        };
    }

}
