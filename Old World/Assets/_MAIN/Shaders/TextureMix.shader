Shader "2D/TextureMix"
{
	Properties
	{
		_Texture("Sprite Texture", 2D) = "white" {}
		_Texture2("Sprite Texture", 2D) = "white" {}
	}
		SubShader
	{
		
	Tags
	{
		"RenderType" = "Opaque"
		"Queue" = "Transparent+1"
	}
		
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile DUMMY PIXELSNAP_ON

#include "UnityCG.cginc"

		struct appdata
	{
		float2 uv : TEXCOORD0;
	};

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

	sampler2D _Texture;
	sampler2D _Texture2;
	
	Fragment vert(Vertex v)
	{
		Fragment  o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv_MainTex = v.uv_MainTex;
		o.uv2 = v.uv2;
		return o;
	}

	fixed4 frag(Fragment i) : Color
	{
		// sample the texture
		fixed4 col = 
			lerp(tex2D(_Texture2, i.uv_MainTex),
				 tex2D(_Texture, i.uv_MainTex),
				 (sin(10 * (i.uv_MainTex.x - i.uv_MainTex.y + _Time.y/2))+1)/4+0.5);

		return col;
	}
		ENDCG
	}
	}
}
