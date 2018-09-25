// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Example/HeatMap" {
	Properties{
	_HeatTex("Texture", 2D) = "white" {}
	}
		SubShader{
		Tags {"Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha // Alpha blend

		Pass {
		CGPROGRAM
		#pragma vertex vert             
		#pragma fragment frag

		struct vertInput {
		float4 pos : POSITION;
		};

		struct vertOutput {
		float4 pos : POSITION;
		float3 worldPos : TEXCOORD1;
		};

		vertOutput vert(vertInput input) {
		vertOutput o;
		o.pos = UnityObjectToClipPos(input.pos);
		o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
		return o;
		}

		uniform int _Points_Length = 0;
		uniform float4 _Points[20]; // (x, y, z) = position
		uniform float2 _Properties[20]; // x = radius, y = intensity

		sampler2D _HeatTex;

		half4 frag(vertOutput output) : COLOR {
		//	float dist = distance(output.worldPos,
		//	   float3(0.0, 0.0, 0.0));
		//// computes the distance between the fragment position 
		//// and the origin (the 4th coordinate should always be 
		//// 1 for points).

	 //   if (dist < 1.0)
		//	{
		//		return float4(0.0, 1.0, 0.0, 1.0);
		//		// color near origin
		//	}
		//  else
		//	{
		//	 return float4(0.1, 0.1, 0.1, 1.0);
		//	 // color far from origin
		//	}
			// Loops over all the points
			float h = 0;
			for (int i = 0; i < _Points_Length; i++)
			{
				// Calculates the contribution of each point
				float di = distance(output.worldPos, _Points[i].xyz);

				float ri = _Points[i].w;
				if (di < ri)
				{
					float hi = 1.0 - (di / ri);
					h += hi;
				}
				}

			// Converts (0-1) according to the heat texture
			//h = saturate(h);
			h = saturate(h);
			h = clamp(h, 0.01, 0.99);
			
			half4 color = tex2D(_HeatTex, fixed2(1 - h, 0.5));
			if (h < 0.03)
			{
				color.a = 0;
			}
			return color;
			}
			ENDCG
			}
	}
		Fallback "Diffuse"
}