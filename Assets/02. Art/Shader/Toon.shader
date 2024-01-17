Shader "Anping/Toon"
{
    Properties
    { 
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
	}
    SubShader
    {

        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 positionOS : POSITION;
				float3 normal : NORMAL;	   
				float4 uv : TEXCOORD0;               
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
            };        

			sampler2D _MainTex;   
			float4 _MainTex_ST;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				OUT.worldNormal = TransformObjectToWorldNormal(IN.normal);
				OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);

                return OUT;
            }

			float4 _Color;
			float4 _AmbientColor;


            // The fragment shader definition.            
            half4 frag(Varyings IN) : SV_Target
            {
				float3 normal = normalize(IN.worldNormal);
				float NdotL = dot(_MainLightPosition, normal);
				float lightIntensity = smoothstep(0, 0.01, NdotL);
				float4 light = lightIntensity * _MainLightColor;
				float4 sample = tex2D(_MainTex, IN.uv);

                return _Color * sample * (_AmbientColor + light);
            }
            ENDHLSL
        }
    }
}