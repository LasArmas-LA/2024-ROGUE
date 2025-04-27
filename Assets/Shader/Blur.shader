Shader "Custom/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurSize;

            fixed4 frag(v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * 0.36;
                col += tex2D(_MainTex, i.uv + float2(_BlurSize * _MainTex_TexelSize.x, 0)) * 0.16;
                col += tex2D(_MainTex, i.uv - float2(_BlurSize * _MainTex_TexelSize.x, 0)) * 0.16;
                col += tex2D(_MainTex, i.uv + float2(0, _BlurSize * _MainTex_TexelSize.y)) * 0.16;
                col += tex2D(_MainTex, i.uv - float2(0, _BlurSize * _MainTex_TexelSize.y)) * 0.16;
                return col;
            }
            ENDCG
        }
    }
} 
