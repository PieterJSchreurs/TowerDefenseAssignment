using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ResourceText : MonoBehaviour
{
    [SerializeField]
    private ResourceValue m_resourceValue;
    private Text m_resourceText;

    private void Awake()
    {
        m_resourceText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_resourceText.text = "Resources: " + m_resourceValue.runTimeValue;
    }
}
