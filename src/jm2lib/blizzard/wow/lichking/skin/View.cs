using System.Text;

namespace jm2lib.blizzard.wow.lichking.skin
{

	using jm2lib.blizzard.common.types;
	using Submesh = jm2lib.blizzard.wow.burningcrusade.Submesh;
	using TextureUnit = jm2lib.blizzard.wow.classic.TextureUnit;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class View : Marshalable
	{
		public sbyte[] magic = new sbyte[] {(sbyte)'S', (sbyte)'K', (sbyte)'I', (sbyte)'N'};
		public ArrayRef<short?> indices;
		public ArrayRef<short?> triangles;
		public ArrayRef<int?> properties;
		public ArrayRef<Submesh> submeshes;
		public ArrayRef<TextureUnit> textureUnits;
		public int bones;

		public View()
		{
			indices = new ArrayRef<short?>(short.TYPE);
			triangles = new ArrayRef<short?>(short.TYPE);
			properties = new ArrayRef<int?>(int.TYPE);
			submeshes = new ArrayRef<Submesh>(typeof(Submesh));
			textureUnits = new ArrayRef<TextureUnit>(typeof(TextureUnit));
			bones = 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			@in.read(magic);
			indices.unmarshal(@in);
			triangles.unmarshal(@in);
			properties.unmarshal(@in);
			submeshes.unmarshal(@in);
			textureUnits.unmarshal(@in);
			bones = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.write(magic);
			indices.marshal(@out);
			triangles.marshal(@out);
			properties.marshal(@out);
			submeshes.marshal(@out);
			textureUnits.marshal(@out);
			@out.writeInt(bones);
			try
			{
				indices.writeContent(@out);
				triangles.writeContent(@out);
				properties.writeContent(@out);
				submeshes.writeContent(@out);
				textureUnits.writeContent(@out);
			}
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java 'multi-catch' syntax:
			catch (InstantiationException | IllegalAccessException e)
			{
				e.printStackTrace();
			}
		}

		public virtual jm2lib.blizzard.wow.burningcrusade.View downConvert()
		{
			jm2lib.blizzard.wow.burningcrusade.View output = new jm2lib.blizzard.wow.burningcrusade.View();
			output.indices = indices;
			output.triangles = triangles;
			output.properties = properties;
			output.submeshes = submeshes;
			output.textureUnits = textureUnits;
			output.bones = bones;
			return output;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.cataclysm.skin.View upConvert() throws Exception
		public virtual jm2lib.blizzard.wow.cataclysm.skin.View upConvert()
		{
			jm2lib.blizzard.wow.cataclysm.skin.View output = new jm2lib.blizzard.wow.cataclysm.skin.View();
			output.indices = indices;
			output.triangles = triangles;
			output.properties = properties;
			for (int i = 0; i < submeshes.Count; i++)
			{
				output.submeshes.Add(submeshes[i].upConvert());
			}
			output.textureUnits = textureUnits;
			output.bones = bones;
			return output;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			string NEW_LINE = System.getProperty("line.separator");
			result.Append("Indices : " + indices + NEW_LINE);
			result.Append("Triangles : " + triangles + NEW_LINE);
			result.Append("Properties : " + properties + NEW_LINE);
			result.Append("Submeshes : " + submeshes + NEW_LINE);
			result.Append("Texture Units : " + textureUnits + NEW_LINE);
			result.Append("Bones : " + bones + NEW_LINE);
			return result.ToString();
		}
	}

}