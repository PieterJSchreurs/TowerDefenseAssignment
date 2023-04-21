using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController
{
    private LineRenderer m_lineRenderer;

    public LineRendererController(float lineSize = 0.01f)
    {
        GameObject lineRenderObject = new GameObject("LineRenderObject");
        m_lineRenderer = lineRenderObject.AddComponent<LineRenderer>();
        m_lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));
    }

    public void SetupLine(Transform pFirstPoint, Transform pSecondPoint)
    {
        m_lineRenderer.startWidth = 0.01f;
        m_lineRenderer.endWidth = 0.01f;
        m_lineRenderer.startColor = Color.red;
        m_lineRenderer.endColor = Color.red;
        m_lineRenderer.SetPosition(0, pFirstPoint.position);
        m_lineRenderer.SetPosition(1, pSecondPoint.position);
        UnityEngine.Object.Destroy(m_lineRenderer.gameObject, 0.5f);
    }
}
