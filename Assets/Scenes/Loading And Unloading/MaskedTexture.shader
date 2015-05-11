Shader "Custom/MaskedTexture" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "Queue" = "Transparent+1" "RenderType"="Transparent" }
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha
		
		Stencil {
			Ref 1
			Comp Equal
		}
		Pass{
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4 vert(float4 vertexPos : POSITION) : SV_POSITION
			{
				return mul(UNITY_MATRIX_MVP, vertexPos);
			}

			fixed4 _Color;

			float4 frag(void) : COLOR
			{
				return _Color;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
