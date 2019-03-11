// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/LightEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_ObjectPos("ObjectPos", Vector) = (-1, -1, 0, 0)
		_Dist("Dist", Float) = 1
		_MaxDist("MaxDist", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _ObjectPos;
			fixed4 _Color;
			float _Dist;
			float _MaxDist;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

				float2 worldXY = mul(unity_ObjectToWorld, v.vertex).xy;

				float2 objpos = _ObjectPos.xy;

				float xdist = worldXY.x - objpos.x;
				float ydist = worldXY.y - objpos.y;

				//_Dist = sqrt((xdist * xdist) + (ydist * ydist));

                o.uv = TRANSFORM_TEX(worldXY, _MainTex);
				o.uv.x = sqrt((xdist * xdist) + (ydist * ydist));
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 closest = float4(0.0, 0.0, 1.0, 1.0);
				float4 next = float4(0.0, 1.0, 1.0, 1.0);
				float4 mid = float4(0.0, 1.0, 1.0, 1.0);
				float4 farther = float4(0.0, 1.0, 0.0, 1.0);
				float4 wahh = float4(1.0, 1.0, 0.0, 1.0);
				float4 farthest = float4(1.0, 0.0, 0.0, 1.0);

				fixed4 col;

				if(i.uv.x >= _MaxDist) {
					col = farthest * _Color;
				}
				else if(i.uv.x >= _MaxDist*4/5) {
					col = wahh * _Color;
				}
				else if(i.uv.x >= _MaxDist*3/5) {
					col = farther * _Color;
				}
				else if(i.uv.x >= _MaxDist*2/5) {
					col = mid * _Color;
				}
				else if(i.uv.x >= _MaxDist*1/5) {
					col = next * _Color;
				}
				else {
					col = closest * _Color;
				}
				col.w = 1;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
