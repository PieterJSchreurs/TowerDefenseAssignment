using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ResourceText : MonoBehaviour
{
    [SerializeField]
    private ResourceValue m_resourceValue;
    private TMP_Text m_text;

    private void Awake()
    {
        m_text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_text.text = "Resources: " + m_resourceValue.RuntimeValue;
    }
}
