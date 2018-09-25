// Modified from: https://forum.unity.com/threads/wireframe-grid-shader.60071/
// and: https://answers.unity.com/questions/803289/how-to-get-vertex-normal-into-surface-shader.html
Shader "Standard/PrefabPrototypeGridShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (0.1294118,0.172549,0.2156863,1)
        _LineColor ("Line Color", Color) = (0.4619972,0.5125082,0.5471698,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.0
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _GridStep ("Grid size", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
       
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
 
        sampler2D _MainTex;
 
        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 vertexNormal;
        };
 
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _LineColor;
        float _GridStep;

        void vert (inout appdata_full v, out Input o) {
           UNITY_INITIALIZE_OUTPUT(Input,o);
           o.vertexNormal = abs(v.normal);
        }
       
        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            
            float2 pos = (IN.worldPos - mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz).xz / _GridStep;
            float3 normal = IN.vertexNormal;

            // grid overlay
            if (normal.y == 1){            
                float2 f  = abs(frac(pos));
                float2 df = fwidth(pos);
                float2 g = smoothstep(-df ,df , f);
                float grid = 1.0 - saturate(g.x * g.y);
                c.rgb = lerp(c.rgb, _LineColor, grid);
            }
            else {
                c.rgb = _LineColor;
            }
            o.Albedo = c.rgb;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
 