using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Event : Referencer
	{
		public sbyte[] identifier;
		public int data;
		public int bone;
		public Vec3F position;
		public EventAnimationBlock enabled;

		public Event()
		{
			identifier = new sbyte[4];
			data = 0;
			bone = 0;
			position = new Vec3F();
			enabled = new EventAnimationBlock();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			for (sbyte i = 0; i < 4; i++)
			{
				identifier[i] = @in.readByte();
			}
			data = @in.readInt();
			bone = @in.readInt();
			position.unmarshal(@in);
			enabled.unmarshal(@in);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tidentifier: ").Append(StringHelperClass.NewString(identifier)).Append("\n\tdata: ").Append(data).Append("\n\tbone: ").Append(bone).Append("\n\tposition: ").Append(position).Append("\n\tenabled: ").Append(enabled).Append("\n}");
			return builder.ToString();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.write(identifier);
			@out.writeInt(data);
			@out.writeInt(bone);
			position.marshal(@out);
			enabled.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
				enabled.writeContent(@out);
		}
	}

}