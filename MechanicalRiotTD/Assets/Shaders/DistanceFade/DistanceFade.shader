/// Thanks to Ronja Böhringer made some of this code

Shader "DistanceFade/DistanceFadeAlpha"
{
    //show values to edit in inspector
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _DitherPattern ("Dithering Pattern", 2D) = "white" {}
        _MinDistance ("Minimum Fade Distance", Float) = 0
        _MaxDistance ("Maximum Fade Distance", Float) = 1
    }

    SubShader {
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType"="Transparent" "Queue"="Transparent"}

        Pass {

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Assets\Cginc\DistanceFade.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;

            float4 _Color;

            //remapping of distance
            float _MinDistance;
            float _MaxDistance;

            sampler2D _DitherPattern;
            float4 _DitherPattern_TexelSize;

            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.screenPos = ComputeScreenPos(o.pos);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                //clip(DistanceFadeClip(i.screenPos, _DitherPattern, _DitherPattern_TexelSize, _MinDistance, _MaxDistance));

                return i.color;

            }
            ENDHLSL
        }
    }
}
