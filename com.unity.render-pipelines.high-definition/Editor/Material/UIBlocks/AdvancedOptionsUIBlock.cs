using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEditor.Rendering.HighDefinition
{
    class AdvancedOptionsUIBlock : MaterialUIBlock
    {
        [Flags]
        public enum Features
        {
            None                = 0,
            Instancing          = 1 << 0,
            SpecularOcclusion   = 1 << 1,
            AdditionalVelocity  = 1 << 2,
            VirtualTexturing    = 1 << 3,
            All                 = ~0,
            Unlit = Instancing | VirtualTexturing,
        }

        public class Styles
        {
            public const string header = "Advanced Options";
            public static GUIContent enableSpecularOcclusionText = new GUIContent("Specular Occlusion From Bent Normal", "Requires cosine weighted bent normal and cosine weighted ambient occlusion. Specular occlusion for Reflection Probe");
            public static GUIContent additionalVelocityChangeText = new GUIContent("Additional Velocity Changes", "Requires additional per vertex velocity info");
            public static GUIContent enableVirtualTexturingText = new GUIContent("Virtual Texturing", "When enabled, use virtual texturing instead of regular textures.");
        }

        protected MaterialProperty enableSpecularOcclusion = null;
        protected MaterialProperty additionalVelocityChange = null;

        protected const string kEnableSpecularOcclusion = "_EnableSpecularOcclusion";
        protected const string kAdditionalVelocityChange = HDMaterialProperties.kAdditionalVelocityChange;

        protected MaterialProperty enableVirtualTexturing = null;
        protected const string kEnableVirtualTexturing = "_VirtualTexturing";

        Expandable  m_ExpandableBit;
        Features    m_Features;

        public AdvancedOptionsUIBlock(Expandable expandableBit, Features features = Features.All)
        {
            m_ExpandableBit = expandableBit;
            m_Features = features;
        }

        public override void LoadMaterialProperties()
        {
            enableSpecularOcclusion = FindProperty(kEnableSpecularOcclusion);
            additionalVelocityChange = FindProperty(kAdditionalVelocityChange);
            enableVirtualTexturing = FindProperty(kEnableVirtualTexturing);
        }

        public override void OnGUI()
        {
            using (var header = new MaterialHeaderScope(Styles.header, (uint)m_ExpandableBit, materialEditor))
            {
                if (header.expanded)
                    DrawAdvancedOptionsGUI();
            }
        }

        void DrawAdvancedOptionsGUI()
        {
            if ((m_Features & Features.Instancing) != 0)
                materialEditor.EnableInstancingField();
            if ((m_Features & Features.SpecularOcclusion) != 0)
                materialEditor.ShaderProperty(enableSpecularOcclusion, Styles.enableSpecularOcclusionText);
            if (((m_Features & Features.VirtualTexturing) != 0) && enableVirtualTexturing != null)
                materialEditor.ShaderProperty(enableVirtualTexturing, Styles.enableVirtualTexturingText);
            if ((m_Features & Features.AdditionalVelocity) != 0)
            {
                if ( additionalVelocityChange != null)
                    materialEditor.ShaderProperty(additionalVelocityChange, Styles.additionalVelocityChangeText);
        	}
    	}
	}
}

