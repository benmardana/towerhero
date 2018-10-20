Shader "WaterPhongShader"
{
	SubShader
	{

		Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		uniform float4 _LightColor;
		uniform float4 _LightPosition;

		struct vertIn
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 color : COLOR;
		};

		struct vertOut
		{
			float4 vertex : SV_POSITION;
			float4 color : COLOR;
			float4 worldVertex : TEXCOORD0;
			float3 worldNormal : TEXCOORD1;
		};

		vertOut vert(vertIn i)
		{
			// Wave displacement.
			float4 displacement = float4(0.0f, sin(i.vertex.x + _Time.y) / 10, 0.0f, 0.0f);
			i.vertex += displacement;
			i.color += displacement;


			vertOut o;
			o.worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), i.normal.xyz));
			o.vertex = UnityObjectToClipPos(i.vertex);
			o.worldVertex = mul(unity_ObjectToWorld, i.vertex);
			o.color = i.color;
			return o;
		}

		float4 frag(vertOut o) : SV_Target
		{
			float3 interpNormal = normalize(o.worldNormal);

			// Calculate ambient RGB intensities
			float Ka = 3;
			float3 amb = o.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

			// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
			// (when calculating the reflected ray in our specular component)
			float fAtt = 0.001;
			float Kd = 1;
			float3 L = normalize(_LightPosition.xyz - o.worldVertex.xyz);
			float LdotN = dot(L, interpNormal);
			float3 dif = fAtt * _LightColor.rgb * Kd * o.color.rgb * saturate(LdotN);

			// Calculate specular reflections
			float Ks = 1;
			float specN = 50; // Values>>1 give tighter highlights
			float3 V = normalize(_WorldSpaceCameraPos.xyz - o.worldVertex.xyz);

			// Using classic reflection calculation
			float3 R = normalize((2.0 * LdotN * interpNormal) - L);
			float3 spe = fAtt * _LightColor.rgb * Ks * pow(saturate(dot(V, R)), specN);

			fixed4 returnColor = fixed4(0.0f, 0.0f, 0.0f, 0.0f);
			returnColor.rgb = amb.rgb + dif.rgb + spe.rgb;
			returnColor.a = 0.5f;

			return returnColor;
		}
			ENDCG
		}
	}
}