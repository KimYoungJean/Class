using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeControllerTest : MonoBehaviour
{
    [Color(0, 1, 0, 1),Size(3)]
    public new Renderer renderer;

    [SerializeField, Color(r: 1, b: 0.5f),Size(SizeAttribute.SizeType.ScaleSize,3)]
    //[Space]
    //[Header("æ∆¿Ã∞Ì")]
    private Graphic graphic;

    [Color]
    public float notRendererOrGraphic;

    [Respawn]
    public GameObject respawnObject;
}