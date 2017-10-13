// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
//Daniel Kharlamov

Shader "Unlit/PosGraph"
{
	Properties
	{
		_GridTX ("Grid Scaling X", Int) = 10
		_GridTY ("Grid Scaling Y", Int) = 10
		_GridTZ ("Grid Scaling Z", Int) = 10
		_GridFact ("Grid Scaling Factor", Float) = 10 
		_SpaceCol ("Spacing Color", Color) = (0,0,0,0)
		_GridCol ("From Grid Color", Color) = (0,0.707,0.707,1)
		_GridColTo ("To Grid Color", Color) = (0.707,0,0.707,1)
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
			};

			struct v2f
			{
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 wPos : POSITION1;
			};

			int _GridTX;
			int _GridTY;
			int _GridTZ;
			float _GridFact;
			fixed4 _SpaceCol;
			fixed4 _GridCol;
			fixed4 _GridColTo;

			v2f vert (appdata v)
			{
				v2f o;
				o.wPos = mul(unity_ObjectToWorld, v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				float4 rc = _SpaceCol;

				float4 gridCol = lerp(_GridCol, _GridColTo, i.wPos.y * 0.1f);

				if(((int)(i.wPos.x * _GridFact)) % _GridTX == 0)
					rc = gridCol;
				if(((int)(i.wPos.z * _GridFact)) % _GridTZ == 0)
					rc = gridCol;
				if(((int)(i.wPos.y * _GridFact)) % _GridTY == 0)
					rc = gridCol;

				UNITY_APPLY_FOG(i.fogCoord, rc);

				return rc;
			}
			ENDCG
		}
	}
}
