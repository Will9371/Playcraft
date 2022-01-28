Shader "Playcraft/VR/InvertedSurface"
{
	// Properties are available for modification in the Inspector
	Properties
	{
		_MainTexture ("Texture", 2D) = "white" {}
	}

	SubShader
	{
		Cull front
		
		CGPROGRAM
			// Categorizes shader type, shader function, and type of lighting used
			#pragma surface surf NoLighting
			
			sampler2D _MainTexture;

			// Describes input data required by shader function
			struct Input
			{
				float2 uv_MainTexture;
			};
			
			fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				fixed4 c;
				c.rgb = s.Albedo;
				c.a = s.Alpha;
				return c;
			}

			// Shader function. This is the part that actually changes what the model looks like
			void surf (Input IN, inout SurfaceOutput o)
			{
				o.Albedo = tex2D(_MainTexture, IN.uv_MainTexture).rgb;
			}
		ENDCG
	}

	// Shader to use if computer can't handle the above
	Fallback "Diffuse"
}