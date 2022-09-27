Shader "Hidden/TimeStop"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Range("Range", Range(0,1)) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            sampler2D _MainTex;
			float _Range;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				
				float d = distance(i.uv, float2(0.5,0.5));
				d = step(d, _Range);
				float3 gray = (col.r + col.g + col.b) / 3;
				col.rgb = lerp( col.rgb, gray, d );

				return col;
            }
            ENDCG
        }
    }
}