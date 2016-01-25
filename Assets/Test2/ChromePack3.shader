Shader "Unlit/ChromePack3"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TexCr ("Texture CR", 2D) = "white" {}
		_TexCb ("Texture CB", 2D) = "white" {}
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

			half3 YCbCrtoRGB(half y, half cb, half cr)
			{
				return half3(
					y                 + 1.402    * cr,
					y - 0.344136 * cb - 0.714136 * cr,
					y + 1.772    * cb
					);
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _TexCr;
			sampler2D _TexCb;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.uv; 

				half y  = tex2D(_MainTex, uv ).a;
				half cb = tex2D(_TexCb, uv ).a - 0.5;
				half cr = tex2D(_TexCr, uv ).a - 0.5; 
				//half cr = tex2D(_TexCr, uv ).a;

				//return half4(y,cb,cr,1);
				 return half4(YCbCrtoRGB(y, cb, cr), 1);
			}
			ENDCG
		}
	}
}
