using System;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wow.cataclysm
{



	using jm2lib.blizzard.common.types;
	using View = jm2lib.blizzard.wow.cataclysm.skin.View;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Cataclysm 4.3.X M2 v272
	/// Also used in later WoW versions. So far up until Legion 20810 build
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public class Model : jm2lib.blizzard.wow.lichking.Model, Marshalable
	{
		public new List<View> views;
		public new ArrayRef<Camera> cameras;
		public new ArrayRef<Particle> particleEmitters;

		public Model() : base()
		{
			version = CATACLYSM;
			views = new List<View>();
			cameras = new ArrayRef<Camera>(typeof(Camera));
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

			int[] aliasLookup = computeAliasLookup();
			// Check if .anim files required and add them to the list
			UnmarshalingStream[] animFiles = new UnmarshalingStream[animations.size()];
			for (int i = 0; i < animations.size(); i++)
			{
				int realIndex = aliasLookup[i];
				if (animations.get(realIndex).Extern)
				{
					animFiles[i] = new UnmarshalingStream(getAnimFileName(@in.FileName, animations.get(realIndex).animationID, animations.get(realIndex).subAnimationID));
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
				views.Add(view);
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
			MarshalingStream[] animFiles = new MarshalingStream[animations.size()];
			for (int i = 0; i < animations.size(); i++)
			{
				int realIndex = aliasLookup[i];
				if (animations.get(realIndex).Extern)
				{
					animFiles[i] = new MarshalingStream(getAnimFileName(@out.FileName, animations.get(realIndex).animationID, animations.get(realIndex).subAnimationID));
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
			}

			animationLookup.marshal(@out);
			bones.marshal(@out);
			keyBoneLookup.marshal(@out);
			vertices.marshal(@out);
			@out.writeInt(views.Count);
			for (sbyte i = 0; i < views.Count; i++)
			{
				MarshalingStream skin = new MarshalingStream(getSkinName(@out.FileName, i));
				views[i].marshal(skin);
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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.M2Format upConvert() throws Exception
		public override M2Format upConvert()
		{
			jm2lib.blizzard.wow.legion.Model output = new jm2lib.blizzard.wow.legion.Model();
			output.name = name;
			output.globalModelFlags = globalModelFlags;
			output.globalSequences = globalSequences;
			for (int i = 0; i < animations.size(); i++)
			{
				output.animations.Add(animations.get(i).upConvert());

			}
			output.animationLookup = animationLookup;
			output.bones = bones;
			output.keyBoneLookup = keyBoneLookup;
			output.vertices = vertices;
			output.views = views;
			output.submeshAnimations = submeshAnimations;
			output.textures = textures;
			output.transparency = transparency;
			output.uvAnimations = uvAnimations;
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
			output.unknown = unknown;
			return output;
		}

		internal const char DEEPRUN_BLENDING = (char)6;
		internal const char MOD2X_BLENDING = (char)4;
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.M2Format downConvert() throws Exception
		public override M2Format downConvert()
		{
			jm2lib.blizzard.wow.lichking.Model output = base.clone();
			/// <summary>
			/// @author Zim4ik </summary>
			output.texUnits.add((short) 0);

			for (int i = 0; i < views.Count; i++)
			{
				output.views.Add(views[i].downConvert());
			}
			for (int i = 0; i < cameras.Count; i++)
			{
				output.cameras.Add(cameras[i].downConvert());
			}
			for (int i = 0; i < particleEmitters.Count; i++)
			{
				output.particleEmitters.Add(particleEmitters[i].downConvert());
			}
			/// <summary>
			/// @author PhilipTNG </summary>
			for (int i = 0; i < output.renderFlags.size(); i++)
			{
				// Flags fix
				output.renderFlags.get(i).X = (char)(output.renderFlags.get(i).X & 0x1F);

				// Blending mode fix
				if (output.renderFlags.get(i).Y > DEEPRUN_BLENDING)
				{
					output.renderFlags.get(i).Y = MOD2X_BLENDING;
				}
			}
			return output;
		}

		/// <summary>
		/// SHALLOW COPY
		/// </summary>
		public override Model clone()
		{
			Model clone = new Model();
			clone.version = CATACLYSM;
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

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tparticleEmitters: ").Append(particleEmitters).Append("\n}");
			return builder.ToString();
		}
	}

}