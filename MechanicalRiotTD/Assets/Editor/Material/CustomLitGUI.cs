using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;

public class CustomLitGUI : LightingShaderGraphGUI
{
    // For lit GUI we don't display the heightmap nor layering options
    const LitSurfaceInputsUIBlock.Features litSurfaceFeatures = LitSurfaceInputsUIBlock.Features.All ^ LitSurfaceInputsUIBlock.Features.HeightMap ^ LitSurfaceInputsUIBlock.Features.LayerOptions;

    MaterialUIBlockList uiBlocks = new MaterialUIBlockList
        {
            new SurfaceOptionUIBlock(MaterialUIBlock.ExpandableBit.Base, features: SurfaceOptionUIBlock.Features.Lit),
            new TessellationOptionsUIBlock(MaterialUIBlock.ExpandableBit.Tessellation),
            new LitSurfaceInputsUIBlock(MaterialUIBlock.ExpandableBit.Input, features: litSurfaceFeatures),
            new DetailInputsUIBlock(MaterialUIBlock.ExpandableBit.Detail),
            // We don't want distortion in Lit
            new TransparencyUIBlock(MaterialUIBlock.ExpandableBit.Transparency, features: TransparencyUIBlock.Features.All & ~TransparencyUIBlock.Features.Distortion),
            new EmissionUIBlock(MaterialUIBlock.ExpandableBit.Emissive),
            new AdvancedOptionsUIBlock(MaterialUIBlock.ExpandableBit.Advance, AdvancedOptionsUIBlock.Features.StandardLit),
        };

    protected override void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        uiBlocks.OnGUI(materialEditor, props);
    }

}
