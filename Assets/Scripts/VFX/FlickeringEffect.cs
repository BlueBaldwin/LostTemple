using UnityEngine;
using UnityEngine.VFX;

public class FlickeringEffect : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;
    [SerializeField] float flickerRate = 0.1f;
    [SerializeField] [ColorUsage(true, true)] private Color _color1;
    [SerializeField] [ColorUsage(true, true)] private Color _color2;

    public static bool portalIsFlickering;

    private void Update()
    {
        Flicker();
    }
    
    private void Flicker()
    {
        if (!portalIsFlickering)
        {
            vfx.SetBool("IsFlickering", false);
            vfx.SetVector4("PortalColour", _color1);
        }
        else
        {
            vfx.SetBool("IsFlickering", true);
            vfx.SetVector4("PortalColour", _color2);
        }
    }
}