using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MaskedImage : Image
{
    public Sprite Mask;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        if (m_Material)
            if (Mask)
            {
                m_Material.SetTexture("_Mask", Mask.texture);
            }
            else m_Material.SetTexture("_Mask", null);
    }
#endif
}