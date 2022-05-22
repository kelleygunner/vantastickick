using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldUiInstance : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMesh;

    public void Init(string text, float lifetime)
    {
        _textMesh.text = text;
        StartCoroutine(Kill(lifetime));
    }

    private IEnumerator Kill(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    
}
