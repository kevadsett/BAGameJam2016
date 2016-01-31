Shader "BossAlien/StageDemon"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_YCutOff ("CutOff", Float) = 250.0
		_Alpha ("Alpha", Float) = 1.0
	}
	SubShader
	{
		Tags {"Queue" = "opaque" }
		LOD 100

		Pass
		{
			

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 vertex2 : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _YCutOff;
			float _Alpha;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);

				o.vertex2 = o.vertex;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				//if(i.vertex2.y < _YCutOff)
				//	discard;

				//if( _Alpha == 0.0 )
				//	discard;
				
				return col;
			}
			ENDCG
		}
	}
}
