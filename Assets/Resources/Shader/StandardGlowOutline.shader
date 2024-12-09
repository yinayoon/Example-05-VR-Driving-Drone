Shader "Custom/StandardGlowOutline"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" {}
        _Color("Base Color", Color) = (1, 1, 1, 1)
        _Metallic("Metallic", Range(0.0, 1.0)) = 0.5
        _Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
        _OutlineColor("Outline Color", Color) = (1, 1, 0, 1)
        _OutlineThickness("Outline Thickness", Float) = 0.03
        _GlowIntensity("Glow Intensity", Float) = 1.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            // Pass for outline effect
            Pass
            {
                Name "Outline"
                Tags { "LightMode" = "Always" }
                Cull Front
                ZWrite On

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float3 worldNormal : TEXCOORD0;
                };

                float _OutlineThickness;
                float4 _OutlineColor;
                float _GlowIntensity;

                v2f vert(appdata v)
                {
                    v2f o;
                    float3 worldNormal = normalize(mul(v.normal, (float3x3)unity_ObjectToWorld));
                    float3 offset = worldNormal * _OutlineThickness;
                    o.pos = UnityObjectToClipPos(v.vertex + float4(offset, 0));
                    o.worldNormal = worldNormal;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float glow = saturate(dot(i.worldNormal, float3(0, 0, 1))) * _GlowIntensity;
                    return _OutlineColor * glow;
                }
                ENDCG
            }

            // Pass for standard surface rendering
            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows

            sampler2D _MainTex;
            fixed4 _Color;
            float _Metallic;
            float _Glossiness;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
