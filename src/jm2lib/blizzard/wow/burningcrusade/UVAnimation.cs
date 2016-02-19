using System.Text;

namespace jm2lib.blizzard.wow.burningcrusade
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using QuatS = jm2lib.blizzard.common.types.QuatS;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using jm2lib.blizzard.wow.classic;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class UVAnimation : Referencer
	{
		public AnimationBlock<Vec3F> translation;
		public AnimationBlock<QuatS> rotation;
		public AnimationBlock<Vec3F> scale;

		public UVAnimation()
		{
			translation = new AnimationBlock<Vec3F>(typeof(Vec3F));
			rotation = new AnimationBlock<QuatS>(typeof(QuatS));
			scale = new AnimationBlock<Vec3F>(typeof(Vec3F));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			translation.unmarshal(@in);
			rotation.unmarshal(@in);
			scale.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			translation.marshal(@out);
			rotation.marshal(@out);
			scale.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
				translation.writeContent(@out);
				rotation.writeContent(@out);
				scale.writeContent(@out);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\ttranslation: ").Append(translation).Append("\n\trotation: ").Append(rotation).Append("\n\tscale: ").Append(scale).Append("\n}");
			return builder.ToString();
		}

		public virtual jm2lib.blizzard.wow.classic.UVAnimation downConvert()
		{
			jm2lib.blizzard.wow.classic.UVAnimation output = new jm2lib.blizzard.wow.classic.UVAnimation();
			output.translation = translation;
			output.rotation.interpolationType = rotation.interpolationType;
			output.rotation.globalSequence = rotation.globalSequence;
			output.rotation.ranges = rotation.ranges;
			output.rotation.timestamps = rotation.timestamps;
			for (int i = 0; i < rotation.values.Count; i++)
			{
				output.rotation.values.Add(rotation.values[i].toQuatF());
			}
			output.scale = scale;
			return output;
		}

	}

}