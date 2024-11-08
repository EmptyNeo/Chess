using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[ExecuteInEditMode]
[AddComponentMenu("UI/Mask Graphic Inverter", 11)]
[RequireComponent(typeof(MaskableGraphic))]
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class MaskGraphicInverter : MonoBehaviour, IMaterialModifier
{
    [NonSerialized]
    private Graphic m_Graphic;

    [NonSerialized]
    private Material m_OriginalMaterial;

    [NonSerialized]
    private Material m_CustomMaterial;

    public Graphic graphic
    {
        get { return m_Graphic ?? (m_Graphic = GetComponent<Graphic>()); }
    }

    public Material GetModifiedMaterial(Material baseMaterial)
    {
        if (!isActiveAndEnabled)
            return baseMaterial;

        if (m_OriginalMaterial == baseMaterial && m_CustomMaterial != null)
        {
            return m_CustomMaterial;
        }

        m_OriginalMaterial = baseMaterial;
        m_CustomMaterial = new Material(baseMaterial);
        m_CustomMaterial.SetInt("_StencilComp", (int)CompareFunction.NotEqual);

        return m_CustomMaterial;
    }

    protected void OnEnable()
    {
        if (graphic != null)
        {
            graphic.canvasRenderer.hasPopInstruction = true;
            graphic.SetMaterialDirty();
        }

        MaskUtilities.NotifyStencilStateChanged(this);
    }

    protected void OnDisable()
    {
        if (graphic != null)
        {
            graphic.SetMaterialDirty();
            graphic.canvasRenderer.hasPopInstruction = false;
            graphic.canvasRenderer.popMaterialCount = 0;
        }

        m_OriginalMaterial = null;
        m_CustomMaterial = null;
        MaskUtilities.NotifyStencilStateChanged(this);
    }

    protected void OnValidate()
    {
        if (!isActiveAndEnabled)
            return;
        if (graphic != null)
            graphic.SetMaterialDirty();
        MaskUtilities.NotifyStencilStateChanged((Component)this);
    }
}