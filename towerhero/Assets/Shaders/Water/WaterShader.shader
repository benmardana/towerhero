//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/WaterShader"
{
	SubShader
	{
		Pass
		{
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				// Displace the original vertex in model space
				float amplitude = 0.5;
				float speed = 0.5;

				//float4 displacement = float4(0.0f, amplitude * sin(speed * _Time.y + v.vertex.x), 0.0f, 0.0f);
				float4 displacement = float4(0.0f, amplitude * sin(speed * _Time.y + v.vertex.x), 0.0f, 0.0f);
				v.vertex += displacement;

				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				v.color = float4(0.0F, 0.0f, 1.0f, 1.0f);
				o.color = v.color;
				return o;
			}

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				return v.color;
			}
			ENDCG
		}
	}
}
