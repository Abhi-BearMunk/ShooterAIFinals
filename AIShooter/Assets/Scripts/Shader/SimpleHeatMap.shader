Shader "Example/SimpleHeatMap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Tags {"Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha // Alpha blend

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			uniform int _Points_Length;
			uniform float4 _Points[20]; 

			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				float h = 0;
			for (int m = 0; m < _Points_Length; m++)
			{
				// Calculates the contribution of each point
				float di = sqrt((_Points[m].y - i.uv.y) *
					(_Points[m].y - i.uv.y)
					+ (_Points[m].x - i.uv.x) *
					(_Points[m].x - i.uv.x)
				);

				float ri = _Points[m].z;
				if (di < ri)
				{
					float hi = 1 - (di / ri);
					hi *= _Points[m].w;
					h += hi;
				}

				
			}

				// Converts (0-1) according to the heat texture
				//h = saturate(h);
				h = saturate(h);
				h *= 0.8;
				h += 0.1;
				//half4 color = tex2D(_HeatTex, fixed2(0.5, h));
				
				fixed4 col = tex2D(_MainTex, float2(1-h, 0.5));
				
				
				return col;
			}
			ENDCG
		}
	}
}
