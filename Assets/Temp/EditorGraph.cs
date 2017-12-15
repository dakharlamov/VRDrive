using UnityEngine;
using UnityEditor;
using System.Collections;


public class Channel
{
	public float[] _data = new float[ Graph.MAX_HISTORY ];
	public Color _color = Color.white;
	public bool isActive = false;

	public Channel( Color _C ) {
		_color = _C;
	}

	public void Feed( float val )
	{
		for( int i = Graph.MAX_HISTORY - 1;  i >= 1;  i-- )
			_data[ i ] = _data[ i-1 ];

		_data[ 0 ] = val;
	}
}

public class Graph 
{
	public static float YMin = -1, YMax = +1;

	public const int MAX_HISTORY = 1024;
	public const int MAX_CHANNELS = 3;

	public static Channel [] channel = new Channel[ MAX_CHANNELS ];

	static Graph()
	{
		Graph.channel[ 0 ] = new Channel( Color.red );
		Graph.channel[ 1 ] = new Channel( Color.green );
		Graph.channel[ 2 ] = new Channel( Color.blue );
	} 

}

public class EditorGraph : EditorWindow
{	

	[MenuItem ("Window/Graph")]
	static void ShowGraph()
	{
		EditorWindow.GetWindow< EditorGraph >( );
	}


	Material lineMaterial;

	void OnEnable( ) {
		EditorApplication.update += MyDelegate;
	}

	void OnDisable( ) {
		EditorApplication.update -= MyDelegate;
	}

	void MyDelegate( ) {
		Repaint( );
	}

	void CreateLineMaterial()
	{
		if( ! lineMaterial )
		{
			lineMaterial = new Material(
				"Shader \"Lines/Colored Blended\" {" +
				"SubShader { Pass { " +
				"    Blend Off " + // SrcAlpha OneMinusSrcAlpha " +
				"    ZWrite Off  Cull Off  Fog { Mode Off } " +
				"    BindChannels {" +
				"      Bind \"vertex\", vertex Bind \"color\", color }" +
				"} } }"
			);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	void OnGUI()
	{
		if( Event.current.type != EventType.Repaint )
			return;

		if( Graph.channel[ 0 ] == null )
			return;

		//DrawSquare( );

		int W = (int)this.position.width;
		int H = (int)this.position.height;

		CreateLineMaterial();
		lineMaterial.SetPass( 0 );

		GL.PushMatrix();
		GL.LoadPixelMatrix();

		GL.Begin( GL.LINES );

		float yy = 50;

		for( int chan = 0; chan < Graph.MAX_CHANNELS; chan++ )
		{
			Channel C = Graph.channel[ chan ];

			if( C == null )
				Debug.Log( "FOO:" + chan );

			if( ! C.isActive )
				continue;

			GL.Color( C._color );

			for( int h = 0; h < Graph.MAX_HISTORY; h++ )
			{
				int xPix = (W-1) - h;

				if( xPix >= 0 )
				{
					float y = C._data[ h ];

					float y_01 = Mathf.InverseLerp( Graph.YMin, Graph.YMax, y );

					int yPix = (int)( y_01 * H );

					Plot( xPix, yPix );
				}
			}
		}

		GL.End();

		GL.PopMatrix();
	}	

	// plot an X
	void Plot( float x, float y )
	{
		// first line of X
		GL.Vertex3( x-1, y-1, 0 );
		GL.Vertex3( x+1, y+1, 0 );

		// second
		GL.Vertex3( x-1, y+1, 0 );
		GL.Vertex3( x+1, y-1, 0 );
	}
}