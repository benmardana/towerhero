Shader "CustomOutlineShader"
{
	Properties
	{
		_OutlineColor("Outline Colour", Color) = (0, 0, 0, 1)
		_OutlineSize("Outline Size", Range(1.0, 5.0)) = 1.02
		_MainTex ("Texture", 2D) = "white" {}
	}

	// Declaring important strctures and variables before entering a pass (or subshader)
	// This allows reuse in multiple passes
	// They are effectively "global"
	CGINCLUDE
	#include "UnityCG.cginc"

	struct vertIn
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;		// normal of this vertex
		float2 uv : TEXCOORD0;
	};

	struct vertOut
	{
		//float4 vertex : POSITION;
		float4 vertex : SV_POSITION;
		float3 normal : NORMAL;
		float2 uv : TEXCOORD0;
	};

	float4 _OutlineColor;
	float _OutlineSize;

	ENDCG

	SubShader
	{		
		Tags {"Queue" = "Transparent"}

		// Rendering the Outline
		Pass
		{
			// TODO (clarify) So that we dont write to the depth buffer, and so that other things can be written on top of it
			ZWrite Off

			CGPROGRAM	// program, not an include
			#pragma vertex vert 
			#pragma fragment frag	// method needs to be defined

			// Making the normals bigger, and transforming coordinates into the world frame
			vertOut vert(vertIn v)
			{
				// Expanding this vertex, by the size of the outline
				v.vertex.xyz *= _OutlineSize;
				vertOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);	// transforming this vertex to world space
				return o;
			}
			
			// Setting the colour to be the outline colour
			//fixed4 frag(vertOut v) : COLOR
			half4 frag(vertOut v) : COLOR
			{
				return _OutlineColor;
			}
			
			ENDCG
		}

		// Rendering the Object
		Pass
		{
			// Using a standard shader

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;

			vertOut vert(vertIn v)
			{
				vertOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(vertOut v) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, v.uv);
				return col;
			}

			ENDCG

		}

	}
}
