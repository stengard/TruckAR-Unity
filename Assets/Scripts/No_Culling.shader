Shader "No_Culling" {
Properties
     {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Alpha Color Key", Color) = (0,0,0,1)
     }
 Category 
 {
     SubShader 
     { 
		Tags
		 { 
			 "Queue"="Transparent" 
			 "IgnoreProjector"="True" 
			 "RenderType"="Transparent" 
			 "PreviewType"="Plane"
			 "CanUseSpriteAtlas"="True"
		 }
 
		 Cull Off
		 
		 Pass
         {
             ZWrite Off
             ZTest Greater
             Blend SrcAlpha OneMinusSrcAlpha 
  
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
  
             sampler2D _MainTex;
             float4 _Color;
 
             struct Vertex
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
     
             struct Fragment
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
  
             Fragment vert(Vertex v)
             {
                 Fragment o;
     
                 o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                 o.uv_MainTex = v.uv_MainTex;
                 o.uv2 = v.uv2;
                 return o;
             }
                                                     
             float4 frag(Fragment IN) : COLOR
             {
                 float4 c = tex2D (_MainTex, IN.uv_MainTex); 
                 return c;
             }
 
             ENDCG
         }
         Pass 
         {

             ZTest Less          
             Blend SrcAlpha OneMinusSrcAlpha 
  
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
  
             sampler2D _MainTex;
             float4 _Color;
 
             struct Vertex
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
     
             struct Fragment
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
  
             Fragment vert(Vertex v)
             {
                 Fragment o;
     
                 o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                 o.uv_MainTex = v.uv_MainTex;
                 o.uv2 = v.uv2;
     
                 return o;
             }
                                                     
             float4 frag(Fragment IN) : COLOR
             {
                 float4 c = tex2D (_MainTex, IN.uv_MainTex);
                 return c;
             }
 
             ENDCG
         }
     }
 }
 
 FallBack "Specular", 1
}