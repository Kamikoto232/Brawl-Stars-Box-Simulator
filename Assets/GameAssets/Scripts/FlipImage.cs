using UnityEngine.UI;

public class FlipImage : Image
{
    public bool Vertical;

#if UNITY_EDITOR

    protected override void OnValidate()
    {
        base.OnValidate();
        if (m_Material)
            if (Vertical)
            {
                m_Material.EnableKeyword("VERTICAL");
            }
            else m_Material.DisableKeyword("VERTICAL");
    }

#endif
}