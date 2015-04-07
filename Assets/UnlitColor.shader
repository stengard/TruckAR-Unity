Shader "Custom/UnlitColor" {
     Properties {
         _Color("Color & Transparency", Color) = (20, 220, 0, 0.8)
     }
     SubShader {
         Lighting Off
         ZWrite Off
         Cull Back
         Blend SrcAlpha OneMinusSrcAlpha
         Tags {"Queue" = "Transparent"}
         Color[_Color]
         Pass {
         }
     } 
     FallBack "Unlit/Color"
 }