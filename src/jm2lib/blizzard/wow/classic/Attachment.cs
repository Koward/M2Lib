using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Attachment : Referencer
	{
		public int id;
		public int bone;
		public Vec3F position;
		public AnimationBlock<sbyte?> animateAttached;

		public Attachment()
		{
			id = 0;
			bone = 0;
			position = new Vec3F();
			animateAttached = new AnimationBlock<sbyte?>(sbyte.TYPE);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			id = @in.readInt();
			bone = @in.readInt();
			position.unmarshal(@in);
			animateAttached.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(id);
			@out.writeInt(bone);
			position.marshal(@out);
			animateAttached.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
			animateAttached.writeContent(@out);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tid: ").Append(id).Append("\n\tbone: ").Append(bone).Append("\n\tposition: ").Append(position).Append("\n\tanimateAttached: ").Append(animateAttached).Append("\n}");
			return builder.ToString();
		}
	}

}