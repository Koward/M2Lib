using System.Text;

namespace jm2lib.blizzard.wow.burningcrusade
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using jm2lib.blizzard.common.types;
	using TextureUnit = jm2lib.blizzard.wow.classic.TextureUnit;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class View : Referencer
	{
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

		public View(View v) : this()
		{
			indices.AddRange(v.indices);
			triangles.AddRange(v.triangles);
			properties.AddRange(v.properties);
			submeshes.AddRange(v.submeshes);
			textureUnits.AddRange(v.textureUnits);
			bones = v.bones;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
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
			indices.marshal(@out);
			triangles.marshal(@out);
			properties.marshal(@out);
			submeshes.marshal(@out);
			textureUnits.marshal(@out);
			@out.writeInt(bones);
		}

		public virtual jm2lib.blizzard.wow.classic.View downConvert()
		{
			jm2lib.blizzard.wow.classic.View output = new jm2lib.blizzard.wow.classic.View();
			output.indices = indices;
			output.triangles = triangles;
			output.properties = properties;
			for (int i = 0; i < submeshes.Count; i++)
			{
				output.submeshes.Add(submeshes[i].downConvert());
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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws java.io.IOException, InstantiationException, IllegalAccessException
		public virtual void writeContent(MarshalingStream @out)
		{
				indices.writeContent(@out);
				triangles.writeContent(@out);
				properties.writeContent(@out);
				submeshes.writeContent(@out);
				textureUnits.writeContent(@out);
		}
	}

}