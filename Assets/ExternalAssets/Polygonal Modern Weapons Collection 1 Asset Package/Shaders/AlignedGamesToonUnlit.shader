Shader "AlignedGames/SpecialToon_URP"
{
    Properties
    {
        _TextureMap ("Texture", 2D) = "white" {}
        _TileAmount ("Scale Multiplier", Float) = 1
        _ShadowBrightness ("Shadow Brightness", Range(0, 1)) = 0
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _Smoothness ("Smoothness", Range(0, 1)) = 0
        _Brightness ("Brightness", Range(0, 5)) = 1
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimPower ("Rim Power", Range(1, 15)) = 5
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "Queue" = "Geometry" "RenderType" = "Opaque" }
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            HLSLPROGRAM
            #pragma vertex VSMain
            #pragma fragment PSMain
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_TextureMap);
            SAMPLER(sampler_TextureMap);
            
            CBUFFER_START(UnityPerMaterial)
            float4 _Color;
            float _TileAmount;
            float _ShadowBrightness;
            float _Smoothness;
            float _Brightness;
            float4 _RimColor;
            float _RimPower;
            CBUFFER_END
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
                float4 shadowCoord : TEXCOORD4;
            };

            Varyings VSMain(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv * _TileAmount;
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.worldPos = TransformObjectToWorld(input.positionOS.xyz);
                output.viewDir = GetWorldSpaceViewDir(output.worldPos);
                output.shadowCoord = TransformWorldToShadowCoord(output.worldPos);
                return output;
            }

            half4 PSMain(Varyings input) : SV_TARGET
            {
                // Texture color
                half4 col = SAMPLE_TEXTURE2D(_TextureMap, sampler_TextureMap, input.uv) * _Color;
                col.rgb *= _Brightness;
                
                // Lighting
                Light mainLight = GetMainLight(input.shadowCoord);
                half3 lightColor = mainLight.color;
                half3 lightDir = normalize(mainLight.direction);
                
                // Shadows
                half shadow = mainLight.shadowAttenuation;
                col.rgb = lerp(col.rgb * _ShadowBrightness, col.rgb, shadow);
                
                // Rim Lighting
                float rimFactor = 1.0 - saturate(dot(normalize(input.viewDir), normalize(input.normalWS)));
                float rimIntensity = pow(rimFactor, _RimPower);
                col.rgb += _RimColor.rgb * rimIntensity;
                
                return col;
            }
            ENDHLSL
        }
    }
}
