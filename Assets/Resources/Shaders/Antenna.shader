Shader "Hidden/Antenna"
{
	SubShader
	{
		Pass
		{
			Blend One One
			BlendOp Max
			Cull Off
			ZWrite Off
			ZTest Always

			Stencil
			{
				Ref [_StencilNonBackground]
				ReadMask [_StencilNonBackground]
				CompBack Equal
				CompFront Equal
			}

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 screen : TEXCOORD0;
				float3 ray : TEXCOORD1;
				float3 object : TEXCOORD2;
			};

			sampler2D _CameraDepthTexture;
			float _Strength;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.screen = ComputeScreenPos(o.pos);
				o.ray = UnityObjectToViewPos(v.vertex) * float3(-1, -1, 1);
				o.object = mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
				return o;
			}

			float4 frag (v2f i) : SV_Target
			{
				float2 uv = i.screen.xy / i.screen.w;
				float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
				float3 rayToFarPlane = i.ray * _ProjectionParams.z / i.ray.z;
				float3 viewPos = rayToFarPlane * depth;
				float3 worldPos = mul(unity_CameraToWorld, float4(viewPos, 1)).xyz;
				float3 objectToWorld = worldPos - i.object;
				float t = clamp(length(objectToWorld), 0, _Strength) / _Strength;
				float intensity = lerp(float(1), float(0), t);
				return float4(intensity, intensity, intensity, 1);
			}

			ENDCG
		}
	}
}
