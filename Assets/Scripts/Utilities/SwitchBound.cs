using Cinemachine;
using UnityEngine;

public class SwitchBound : MonoBehaviour
{
    //TODO:ʵ���л�����ʱ��Ҫ���ĵ���
    private void Start()
    {
        SwitchConfinerShape();
    }
    private void SwitchConfinerShape()
    {
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsShape").GetComponent<PolygonCollider2D>();

        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();

        cinemachineConfiner.m_BoundingShape2D = confinerShape;

        //Call this if the bounding shape's points change at runtime
        cinemachineConfiner.InvalidatePathCache();
    }
}
