using UnityEngine;


// Handles setting a color to a given renderer and material slot. Used to simplify coloring our Unit.
// (This can be added on the visual prefab and the Unit code can just query oif that component exists to set color)
public class ColorHandler : MonoBehaviour
{
    public Renderer TintRenderer;
    public int TintMaterialSlot;
    
    public void SetColor(Color c)
    {
        var prop = new MaterialPropertyBlock();
        prop.SetColor("_BaseColor", c);
        TintRenderer.SetPropertyBlock(prop, TintMaterialSlot);
    }
}
