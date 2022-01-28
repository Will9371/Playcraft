Shader "Playcraft/VR/ScrollTransparent"
{
	Properties
	{
		_MainTexture ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_ScrollX("X scroll speed", Range(-480, 480)) = 0
		_ScrollY("Y scroll speed", Range(-480, 480)) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}
		
		Cull Off
		
		CGPROGRAM
			#pragma surface surf Lambert alpha:fade
			
			sampler2D _MainTexture;
			fixed4 _Color;
			float _ScrollX;
			float _ScrollY;

			struct Input
			{
				float2 uv_MainTexture;
			};		

			void surf (Input IN, inout SurfaceOutput o)
			{
				_ScrollX *= _Time;
				_ScrollY *= _Time;
				float4 color = tex2D(_MainTexture, IN.uv_MainTexture + float2(_ScrollX, _ScrollY));
				o.Emission = color.rgb * _Color;
				o.Alpha = color.a;
			}
		ENDCG
	}

	Fallback "Diffuse"
}