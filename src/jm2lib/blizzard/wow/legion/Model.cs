using System;
using System.Text;

namespace jm2lib.blizzard.wow.legion
{

	using jm2lib.blizzard.common.types;
	using View = jm2lib.blizzard.wow.cataclysm.skin.View;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Legion 7.0.1 Alpha M2 v274 (build 20810)
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public class Model : jm2lib.blizzard.wow.cataclysm.Model, Marshalable
	{
		public ArrayRef<Animation> animations;
		public Model() : base()
		{
			version = LEGION;
			animations = new ArrayRef<Animation>(typeof(Animation));
		}

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tparticleEmitters: ").Append(particleEmitters).Append("\n}");
			return builder.ToString();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public override void unmarshal(UnmarshalingStream @in)
		{
			name.unmarshal(@in);
			globalModelFlags = @in.readInt();
			globalSequences.unmarshal(@in);
			animations.unmarshal(@in);

			int[] aliasLookup = computeAliasLookup();
			// Check if .anim files required and add them to the list
			UnmarshalingStream[] animFiles = new UnmarshalingStream[animations.Count];
			for (int i = 0; i < animations.Count; i++)
			{
				int realIndex = aliasLookup[i];
				if (animations[realIndex].Extern)
				{
					animFiles[i] = new UnmarshalingStream(getAnimFileName(@in.FileName, animations[realIndex].animationID, animations[realIndex].subAnimationID));
				}
			}
			// Give the .anim files list to all structures that may need them
			if (animFiles.Length > 0)
			{
				bones.AnimFiles = animFiles;
				submeshAnimations.AnimFiles = animFiles;
				transparency.AnimFiles = animFiles;
				uvAnimations.AnimFiles = animFiles;
				attachments.AnimFiles = animFiles;
				events.AnimFiles = animFiles;
				lights.AnimFiles = animFiles;
				cameras.AnimFiles = animFiles;
				ribbonEmitters.AnimFiles = animFiles;
				particleEmitters.AnimFiles = animFiles;
				if ((globalModelFlags & 0x0008) != 0)
				{
					unknown.AnimFiles = animFiles;
				}
			}

			animationLookup.unmarshal(@in);
			bones.unmarshal(@in);
			keyBoneLookup.unmarshal(@in);
			vertices.unmarshal(@in);
			int nViews = @in.readInt();
			for (sbyte i = 0; i < nViews; i++)
			{
				UnmarshalingStream skin = new UnmarshalingStream(getSkinName(@in.FileName, i));
				View view = new View();
				view.unmarshal(skin);
				views.add(view);
				skin.close();
			}
			submeshAnimations.unmarshal(@in);
			textures.unmarshal(@in);
			transparency.unmarshal(@in);
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
			if ((globalModelFlags & 0x0008) != 0)
			{
				unknown = new ArrayRef<short?>(short.TYPE);
				unknown.unmarshal(@in);
			}
			closeFiles(false, animFiles);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public override void marshal(MarshalingStream @out)
		{
			// WRITE HEADER
			name.marshal(@out);
			@out.writeInt(globalModelFlags);
			globalSequences.marshal(@out);
			animations.marshal(@out);

			int[] aliasLookup = computeAliasLookup();
			// Check if .anim files required and add them to the list
			MarshalingStream[] animFiles = new MarshalingStream[animations.Count];
			for (int i = 0; i < animations.Count; i++)
			{
				int realIndex = aliasLookup[i];
				if (animations[realIndex].Extern)
				{
					animFiles[i] = new MarshalingStream(getAnimFileName(@out.FileName, animations[realIndex].animationID, animations[realIndex].subAnimationID));
				}
			}
			// Give the .anim files list to all structures that may need them
			if (animFiles.Length > 0)
			{
				bones.AnimFiles = animFiles;
				submeshAnimations.AnimFiles = animFiles;
				transparency.AnimFiles = animFiles;
				uvAnimations.AnimFiles = animFiles;
				attachments.AnimFiles = animFiles;
				events.AnimFiles = animFiles;
				lights.AnimFiles = animFiles;
				cameras.AnimFiles = animFiles;
				ribbonEmitters.AnimFiles = animFiles;
				particleEmitters.AnimFiles = animFiles;
				if ((globalModelFlags & 0x0008) != 0)
				{
					unknown.AnimFiles = animFiles;
				}
			}

			animationLookup.marshal(@out);
			bones.marshal(@out);
			keyBoneLookup.marshal(@out);
			vertices.marshal(@out);
			@out.writeInt(views.size());
			for (sbyte i = 0; i < views.size(); i++)
			{
				MarshalingStream skin = new MarshalingStream(getSkinName(@out.FileName, i));
				views.get(i).marshal(skin);
				align(skin);
				skin.close();
			}
			submeshAnimations.marshal(@out);
			textures.marshal(@out);
			transparency.marshal(@out);
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
			if ((globalModelFlags & 0x0008) != 0)
			{
				unknown.marshal(@out);
			}

			// WRITE BLOCKS
			try
			{
				name.writeContent(@out);
				globalSequences.writeContent(@out);
				animations.writeContent(@out);
				animationLookup.writeContent(@out);
				bones.writeContent(@out);
				keyBoneLookup.writeContent(@out);
				vertices.writeContent(@out);
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
				if ((globalModelFlags & 0x0008) != 0)
				{
					unknown.writeContent(@out);
				}
			}
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java 'multi-catch' syntax:
			catch (InstantiationException | IllegalAccessException e)
			{
				Console.Error.WriteLine("Error in Block writing");
				e.printStackTrace();
			}
			align(@out);
			closeFiles(true, animFiles);
		}

		protected internal override int[] computeAliasLookup()
		{
			int[] aliasLookup = new int[animations.Count];
			for (int i = 0; i < animations.Count; i++)
			{
				aliasLookup[i] = Animation.getRealPos(i, animations);
				if (aliasLookup[i] == -1)
				{
					Console.Error.WriteLine("Error : alias to -1");
				}
			}
			return aliasLookup;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.M2Format downConvert() throws Exception
		public override M2Format downConvert()
		{
			jm2lib.blizzard.wow.cataclysm.Model output = base.clone();
			for (int i = 0; i < animations.Count; i++)
			{
				output.animations.add(animations[i].downConvert());
			}
			return output;
		}

		/// <summary>
		/// SHALLOW COPY
		/// </summary>
		public override Model clone()
		{
			Model clone = new Model();
			clone.version = LEGION;
			clone.name = name;
			clone.globalModelFlags = globalModelFlags;
			clone.globalSequences = globalSequences;
			clone.animations = animations;
			clone.animationLookup = animationLookup;
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
			clone.unknown = unknown;
			return clone;
		}
	}

}