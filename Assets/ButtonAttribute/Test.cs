using System.Collections.Generic;
using UnityEngine;
using ButtonAttribute;

public class Test : MonoBehaviour
{
    [InspectorButton("A")]
    [ContextMenu("A")]
    public void A(int a, string aa)
    {
        Debug.Log(a);
    }
}
