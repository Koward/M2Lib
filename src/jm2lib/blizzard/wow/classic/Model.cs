using System;
using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using jm2lib.blizzard.common.types;
	using Vec2C = jm2lib.blizzard.common.types.Vec2C;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Classic 1.12.1 v256
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public class Model : M2Format, Marshalable
	{
		public ArrayRef<sbyte?> name;
		public int globalModelFlags;

		public ArrayRef<int?> globalSequences;
		public ArrayRef<Animation> animations;
		public ArrayRef<short?> animationLookup;
		public ArrayRef<PlayableRecord> playableAnimationLookup;
		public ArrayRef<Bone> bones;
		public ArrayRef<short?> keyBoneLookup;
		public ArrayRef<Vertex> vertices;
		public ArrayRef<View> views;
		public ArrayRef<SubmeshAnimation> submeshAnimations;
		public ArrayRef<Texture> textures;
		public ArrayRef<Transparency> transparency;
		public ArrayRef<short?> unknownRef;
		public ArrayRef<UVAnimation> uvAnimations;
		public ArrayRef<short?> texReplace;
		public ArrayRef<Vec2C> renderFlags;
		public ArrayRef<short?> boneLookupTable;
		public ArrayRef<short?> texLookup;
		public ArrayRef<short?> texUnits;
		public ArrayRef<short?> transLookup;
		public ArrayRef<short?> uvAnimLookup;

		public Vec3F[] boundingBox;
		public float boundingSphereRadius;
		public Vec3F[] collisionBox;
		public float collisionSphereRadius;

		public ArrayRef<short?> boundingTriangles;
		public ArrayRef<Vec3F> boundingVertices;
		public ArrayRef<Vec3F> boundingNormals;
		public ArrayRef<Attachment> attachments;
		public ArrayRef<short?> attachLookup;
		public ArrayRef<Event> events;
		public ArrayRef<Light> lights;
		public ArrayRef<Camera> cameras;
		public ArrayRef<short?> cameraLookup;
		public ArrayRef<Ribbon> ribbonEmitters;
		public ArrayRef<Particle> particleEmitters;

		public Model()
		{
			version = CLASSIC;
			name = new ArrayRef<sbyte?>("");
			globalModelFlags = 0;
			globalSequences = new ArrayRef<int?>(int.TYPE);
			animations = new ArrayRef<Animation>(typeof(Animation));
			animationLookup = new ArrayRef<short?>(short.TYPE);
			playableAnimationLookup = new ArrayRef<PlayableRecord>(typeof(PlayableRecord));
			bones = new ArrayRef<Bone>(typeof(Bone));
			keyBoneLookup = new ArrayRef<short?>(short.TYPE);
			vertices = new ArrayRef<Vertex>(typeof(Vertex));
			views = new ArrayRef<View>(typeof(View));
			submeshAnimations = new ArrayRef<SubmeshAnimation>(typeof(SubmeshAnimation));
			textures = new ArrayRef<Texture>(typeof(Texture));
			transparency = new ArrayRef<Transparency>(typeof(Transparency));
			unknownRef = new ArrayRef<short?>(short.TYPE);
			uvAnimations = new ArrayRef<UVAnimation>(typeof(UVAnimation));
			texReplace = new ArrayRef<short?>(short.TYPE);
			renderFlags = new ArrayRef<Vec2C>(typeof(Vec2C));
			boneLookupTable = new ArrayRef<short?>(short.TYPE);
			texLookup = new ArrayRef<short?>(short.TYPE);
			texUnits = new ArrayRef<short?>(short.TYPE);
			transLookup = new ArrayRef<short?>(short.TYPE);
			uvAnimLookup = new ArrayRef<short?>(short.TYPE);

			boundingBox = new Vec3F[] {new Vec3F(), new Vec3F()};
			boundingSphereRadius = 0.0F;
			collisionBox = new Vec3F[] {new Vec3F(), new Vec3F()};
			collisionSphereRadius = 0.0F;

			boundingTriangles = new ArrayRef<short?>(short.TYPE);
			boundingVertices = new ArrayRef<Vec3F>(typeof(Vec3F));
			boundingNormals = new ArrayRef<Vec3F>(typeof(Vec3F));
			attachments = new ArrayRef<Attachment>(typeof(Attachment));
			attachLookup = new ArrayRef<short?>(short.TYPE);
			events = new ArrayRef<Event>(typeof(Event));
			lights = new ArrayRef<Light>(typeof(Light));
			cameras = new ArrayRef<Camera>(typeof(Camera));
			cameraLookup = new ArrayRef<short?>(short.TYPE);
			ribbonEmitters = new ArrayRef<Ribbon>(typeof(Ribbon));
			particleEmitters = new ArrayRef<Particle>(typeof(Particle));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public override void unmarshal(UnmarshalingStream @in)
		{
			name.unmarshal(@in);
			globalModelFlags = @in.readInt();
			globalSequences.unmarshal(@in);
			animations.unmarshal(@in);
			animationLookup.unmarshal(@in);
			playableAnimationLookup.unmarshal(@in);
			bones.unmarshal(@in);
			keyBoneLookup.unmarshal(@in);
			vertices.unmarshal(@in);
			views.unmarshal(@in);
			submeshAnimations.unmarshal(@in);
			textures.unmarshal(@in);
			transparency.unmarshal(@in);
			unknownRef.unmarshal(@in);
			uvAnimations.unmarshal(@in);
			texReplace.unmarshal(@in);
			renderFlags.unmarshal(@in);
			boneLookupTable.unmarshal(@in);
			texLookup.unmarshal(@in);
			texUnits.unmarshal(@in);
			transLookup.unmarshal(@in);
			uvAnimLookup.unmarshal(@in);

			boundingBox[0].unmarshal(@in);
			boundingBox[1].unmarshal(@in);
			boundingSphereRadius = @in.readFloat();
			collisionBox[0].unmarshal(@in);
			collisionBox[1].unmarshal(@in);
			collisionSphereRadius = @in.readFloat();

			boundingTriangles.unmarshal(@in);
			boundingVertices.unmarshal(@in);
			boundingNormals.unmarshal(@in);
			attachments.unmarshal(@in);
			attachLookup.unmarshal(@in);
			events.unmarshal(@in);
			lights.unmarshal(@in);
			cameras.unmarshal(@in);
			cameraLookup.unmarshal(@in);
			ribbonEmitters.unmarshal(@in);
			particleEmitters.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public override void marshal(MarshalingStream @out)
		{
			//WRITE HEADER
			name.marshal(@out);
			@out.writeInt(globalModelFlags);
			globalSequences.marshal(@out);
			animations.marshal(@out);
			animationLookup.marshal(@out);
			playableAnimationLookup.marshal(@out);
			bones.marshal(@out);
			keyBoneLookup.marshal(@out);
			vertices.marshal(@out);
			views.marshal(@out);
			submeshAnimations.marshal(@out);
			textures.marshal(@out);
			transparency.marshal(@out);
			unknownRef.marshal(@out);
			uvAnimations.marshal(@out);
			texReplace.marshal(@out);
			renderFlags.marshal(@out);
			boneLookupTable.marshal(@out);
			texLookup.marshal(@out);
			texUnits.marshal(@out);
			transLookup.marshal(@out);
			uvAnimLookup.marshal(@out);

			boundingBox[0].marshal(@out);
			boundingBox[1].marshal(@out);
			@out.writeFloat(boundingSphereRadius);
			collisionBox[0].marshal(@out);
			collisionBox[1].marshal(@out);
			@out.writeFloat(collisionSphereRadius);

			boundingTriangles.marshal(@out);
			boundingVertices.marshal(@out);
			boundingNormals.marshal(@out);
			attachments.marshal(@out);
			attachLookup.marshal(@out);
			events.marshal(@out);
			lights.marshal(@out);
			cameras.marshal(@out);
			cameraLookup.marshal(@out);
			ribbonEmitters.marshal(@out);
			particleEmitters.marshal(@out);

			// WRITE BLOCKS
			try
			{
				name.writeContent(@out);
				globalSequences.writeContent(@out);
				animations.writeContent(@out);
				animationLookup.writeContent(@out);
				playableAnimationLookup.writeContent(@out);
				bones.writeContent(@out);
				keyBoneLookup.writeContent(@out);
				vertices.writeContent(@out);
				views.writeContent(@out);
				submeshAnimations.writeContent(@out);
				textures.writeContent(@out);
				transparency.writeContent(@out);
				uvAnimations.writeContent(@out);
				texReplace.writeContent(@out);
				renderFlags.writeContent(@out);
				boneLookupTable.writeContent(@out);
				texLookup.writeContent(@out);
				texUnits.writeContent(@out);
				transLookup.writeContent(@out);
				uvAnimLookup.writeContent(@out);

				boundingTriangles.writeContent(@out);
				boundingVertices.writeContent(@out);
				boundingNormals.writeContent(@out);
				attachments.writeContent(@out);
				attachLookup.writeContent(@out);
				events.writeContent(@out);
				lights.writeContent(@out);
				cameras.writeContent(@out);
				cameraLookup.writeContent(@out);
				ribbonEmitters.writeContent(@out);
				particleEmitters.writeContent(@out);
			}
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java 'multi-catch' syntax:
			catch (InstantiationException | IllegalAccessException e)
			{
				Console.Error.WriteLine("Error in Block writing");
				e.printStackTrace();
				Environment.Exit(1);
			}
			align(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public jm2lib.blizzard.wow.M2Format upConvert() throws Exception
		public override M2Format upConvert()
		{
			jm2lib.blizzard.wow.burningcrusade.Model output = new jm2lib.blizzard.wow.burningcrusade.Model();
			output.name = name;
			output.globalModelFlags = globalModelFlags;
			output.globalSequences = globalSequences;
			output.animations = animations;
			output.animationLookup = animationLookup;
			output.playableAnimationLookup = playableAnimationLookup;
			for (int i = 0; i < bones.Count; i++)
			{
				output.bones.Add(bones[i].upConvert());
			}
			output.keyBoneLookup = keyBoneLookup;
			output.vertices = vertices;
			for (int i = 0; i < views.Count; i++)
			{
				output.views.Add(views[i].upConvert());
			}
			output.submeshAnimations = submeshAnimations;
			output.textures = textures;
			output.transparency = transparency;
			for (int i = 0; i < uvAnimations.Count; i++)
			{
				output.uvAnimations.Add(uvAnimations[i].upConvert());
			}

			output.texReplace = texReplace;
			output.renderFlags = renderFlags;
			output.boneLookupTable = boneLookupTable;
			output.texLookup = texLookup;
			output.texUnits = texUnits;
			output.transLookup = transLookup;
			output.uvAnimLookup = uvAnimLookup;
			output.boundingBox = boundingBox;
			output.boundingSphereRadius = boundingSphereRadius;
			output.collisionBox = collisionBox;
			output.collisionSphereRadius = collisionSphereRadius;
			output.boundingTriangles = boundingTriangles;
			output.boundingVertices = boundingVertices;
			output.boundingNormals = boundingNormals;
			output.attachments = attachments;
			output.attachLookup = attachLookup;
			output.events = events;
			output.lights = lights;
			output.cameras = cameras;
			output.cameraLookup = cameraLookup;
			output.ribbonEmitters = ribbonEmitters;
			output.particleEmitters = particleEmitters;
			return output;
		}

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tname: ").Append(name.toNameString());
			builder.Append(" {\n\tattachments: ").Append(attachments);
			return builder.ToString();
		}

		/// <summary>
		/// SHALLOW COPY
		/// </summary>
		public override Model clone()
		{
			Model clone = new Model();
			clone.version = CLASSIC;
			clone.name = name;
			clone.globalModelFlags = globalModelFlags;
			clone.globalSequences = globalSequences;
			clone.animations = animations;
			clone.animationLookup = animationLookup;
			clone.playableAnimationLookup = playableAnimationLookup;
			clone.bones = bones;
			clone.keyBoneLookup = keyBoneLookup;
			clone.vertices = vertices;
			clone.views = views;
			clone.submeshAnimations = submeshAnimations;
			clone.textures = textures;
			clone.transparency = transparency;
			clone.uvAnimations = uvAnimations;
			clone.texReplace = texReplace;
			clone.renderFlags = renderFlags;
			clone.boneLookupTable = boneLookupTable;
			clone.texLookup = texLookup;
			clone.texUnits = texUnits;
			clone.transLookup = transLookup;
			clone.uvAnimLookup = uvAnimLookup;
			clone.boundingBox = boundingBox;
			clone.boundingSphereRadius = boundingSphereRadius;
			clone.collisionBox = collisionBox;
			clone.collisionSphereRadius = collisionSphereRadius;
			clone.boundingTriangles = boundingTriangles;
			clone.boundingVertices = boundingVertices;
			clone.boundingNormals = boundingNormals;
			clone.attachments = attachments;
			clone.attachLookup = attachLookup;
			clone.events = events;
			clone.lights = lights;
			clone.cameras = cameras;
			clone.cameraLookup = cameraLookup;
			clone.ribbonEmitters = ribbonEmitters;
			clone.particleEmitters = particleEmitters;
			return clone;
		}
	}

}