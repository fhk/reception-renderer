Shader "Custom/SolidColour"
{
	Properties
	{
		_Color("Main Color", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags{ "Queue" = "Geometry" "RenderType" = "Opaque"}

		Cull Off 
		ZWrite On 
		ZTest LEqual

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata_t 
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			fixed4 _Color;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				return _Color;
			}

			ENDCG
		}
	}

	FallBack "VertexLit"
}
