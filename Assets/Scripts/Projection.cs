using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Text _text;
    [SerializeField] private Transform _visualTransform;

    public void Setup(Material material,string numbertext,float radius)
    {
        _renderer.material = material;
        _text.text = numbertext;
        _visualTransform.localScale = Vector3.one * radius*2f;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
