Shader "Unlit/ChromaPack1_P"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100

		Pass
		{
			Blend One Zero

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			half3 YCbCrtoRGB(half y, half cb, half cr)
			{
				return half3(
					y                 + 1.402    * cr,
					y - 0.344136 * cb - 0.714136 * cr,
					y + 1.772    * cb
					);
			}

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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);

				 float2 uv = i.uv;

				 half y  = tex2D(_MainTex, uv * float2(2.0 / 3, 1.0)).a;
				 half cb = tex2D(_MainTex, uv * float2(1.0 / 3, 0.5) + float2(2.0 / 3, 0.5)).a - 0.5;
				 half cr = tex2D(_MainTex, uv * float2(1.0 / 3, 0.5) + float2(2.0 / 3, 0.0)).a - 0.5;

				return half4(YCbCrtoRGB(y, cb, cr), 1);
			}
			ENDCG
		}
	}
}
