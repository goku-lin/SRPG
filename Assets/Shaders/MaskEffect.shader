Shader "Common/MaskEffect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Tags{   "Queue" = "Transparent" }

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
                float4 srcPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.srcPos = ComputeScreenPos(o.vertex);
                return o;
            }

            sampler2D _MainTex;
            float2 _Pos;
            float _Size;
            float _EdgeBlurLength;
            // 创建圆
            fixed3 createCircle(float2 pos,float radius,float2 uv) {
                //当前像素到中心点的距离
                float dis = distance(pos,uv);
                //  smoothstep 平滑过渡
                float col = smoothstep(radius + _EdgeBlurLength,radius,dis);
                return fixed3(col,col,col);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // float2 coord = (i.srcPos.xy/i.srcPos.w)*_ScreenParams.xy;
                // float2 uv = (2.0*coord.xy - _ScreenParams.xy)/min(_ScreenParams.x,_ScreenParams.y);
                // 根据屏幕比例缩放
                float2 scale = float2(_ScreenParams.x / _ScreenParams.y, 1);

                fixed4 col = tex2D(_MainTex, i.uv);
                fixed3 mask = createCircle(_Pos * scale,_Size,i.uv * scale);

                return col * fixed4(mask,1.0);
            }
            ENDCG
        }
    }
}