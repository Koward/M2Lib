using System;
using System.Text;

namespace jm2lib.blizzard.wow.lateburningcrusade
{

	using jm2lib.blizzard.common.types;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// The Burning Crusade 2.4.3 v263
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public class Model : jm2lib.blizzard.wow.burningcrusade.Model, Marshalable
	{
		public ArrayRef<Particle> particleEmitters;

		public Model() : base()
		{
			version = BURNING_CRUSADE + 3;
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
//ORIGINAL LINE: @Override public jm2lib.blizzard.wow.M2Format downConvert() throws Exception
		public override M2Format downConvert()
		{
			jm2lib.blizzard.wow.burningcrusade.Model output = base.clone();
			for (int i = 0; i < particleEmitters.Count; i++)
			{
				output.particleEmitters.add(particleEmitters[i].downConvert());
			}
			return output;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public jm2lib.blizzard.wow.M2Format upConvert() throws Exception
		public override M2Format upConvert()
		{
			//TODO Implement the Burning Crusade to Lich King converting.
			return null;
		}

		/// <summary>
		/// SHALLOW COPY
		/// </summary>
		public override Model clone()
		{
			Model clone = new Model();
			clone.version = BURNING_CRUSADE + 3;
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

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tviews: ").Append(views).Append("\n}");
			return builder.ToString();
		}
	}

}