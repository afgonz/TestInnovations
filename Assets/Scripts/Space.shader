Shader "Andy/Space"
{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_ScrollXSpeed("X Scroll Speed", Range(0 , 10)) = 0
		_ScrollYSpeed("Y Scroll Speed", Range(0 , 10)) = 2
		_Alpha("Alpha", Range(0.0,1.0)) = 1.0
	}
		SubShader{
			Tags {
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard alpha:fade 

			#pragma target 3.0

			sampler2D _MainTex;
			fixed4 _Color;
			fixed _Alpha;
			fixed _ScrollXSpeed;
			fixed _ScrollYSpeed;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed2 scrolledUV = IN.uv_MainTex;
				fixed xScrollValue = _ScrollXSpeed * _Time;
				fixed yScrollValue = _ScrollYSpeed * _Time;

				scrolledUV += fixed2(xScrollValue, yScrollValue);

				half4 c = tex2D(_MainTex, scrolledUV) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a * _Alpha;

			}
			ENDCG
		}
			FallBack "Diffuse"
}