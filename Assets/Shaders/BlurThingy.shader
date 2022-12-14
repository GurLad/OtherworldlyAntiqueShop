Shader "Custom/BlurThingy"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Noise("Noise", 2D) = "white" {}
        _BlurRadius("Blur radius", Float) = 750
        _BlurStrength("Blur strength", Range(0.0, 1.0)) = 1
        _Speed("Speed", Float) = 1
        _ColorModifier("Color modifier", Color) = (.25, .5, .5, 1)
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
            sampler2D _Noise;
            float _BlurRadius;
            float _BlurStrength;
            float _Speed;
            float4 _ColorModifier;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // Apperantly, colors can exceed 1 for no apperant reason                
                if (col.r >= 1)
                {
                    col.r = 1;
                }
                if (col.g >= 1)
                {
                    col.g = 1;
                }
                if (col.b >= 1)
                {
                    col.b = 1;
                }
                if (_BlurStrength > 0)
                {
                    fixed4 tempCol = tex2D(_MainTex, i.uv);
                    // Increase brightness of nearby pixels
                    float size = col.r + col.g + col.b;
                    if (size < 0.25f)
                    {
                        size = 0.25f;
                    }
                    if (size <= 2.75f)
                    {
                        col.rgb /= 9;
                        for (float k = -1; k <= 1; k++)
                        {
                            for (float j = -1; j <= 1; j++)
                            {
                                float2 pos = i.uv;
                                pos.x += (k / (_BlurRadius * size)) * tex2D(_Noise, float2(i.uv.x + _Time[0] * _Speed, i.uv.y + _Time[0] * _Speed)).r;
                                pos.y += (j / (_BlurRadius * size)) * tex2D(_Noise, float2(i.uv.y + 42 + _Time[0] * _Speed, i.uv.x + 76 + _Time[0] * _Speed)).r;
                                fixed4 altCol = tex2D(_MainTex, pos);
                                col.rgb += altCol.rgb / 9;
                            }
                        }
                    }
                    else
                    {
                        col.rgb += col.rgb / 9;
                    }
                    col = tempCol * (1 - _BlurStrength) + col * _BlurStrength;
                }
                col.r *= _ColorModifier.r;
                col.g *= _ColorModifier.g;
                col.b *= _ColorModifier.b;
                return col;
            }
            ENDCG
        }
    }
}
