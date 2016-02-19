using System;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wow.lichking
{


	using jm2lib.blizzard.common.types;
	using LookupBuilder = jm2lib.blizzard.wow.classic.LookupBuilder;
	using View = jm2lib.blizzard.wow.lichking.skin.View;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Wrath of the Lich King 3.3.X M2 v264
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public class Model : jm2lib.blizzard.wow.lateburningcrusade.Model, Marshalable
	{
		public ArrayRef<Animation> animations;
		public ArrayRef<Bone> bones;
		public List<View> views;
		public ArrayRef<SubmeshAnimation> submeshAnimations;
		public ArrayRef<Transparency> transparency;
		public ArrayRef<UVAnimation> uvAnimations;
		public ArrayRef<Attachment> attachments;
		public ArrayRef<Event> events;
		public ArrayRef<Light> lights;
		public ArrayRef<Camera> cameras;
		public ArrayRef<Ribbon> ribbonEmitters;
		public new ArrayRef<Particle> particleEmitters;
		public ArrayRef<short?> unknown;
		public Model() : base()
		{
			version = LICH_KING;
			animations = new ArrayRef<Animation>(typeof(Animation));
			bones = new ArrayRef<Bone>(typeof(Bone));
			views = new List<View>();
			submeshAnimations = new ArrayRef<SubmeshAnimation>(typeof(SubmeshAnimation));
			transparency = new ArrayRef<Transparency>(typeof(Transparency));
			uvAnimations = new ArrayRef<UVAnimation>(typeof(UVAnimation));
			attachments = new ArrayRef<Attachment>(typeof(Attachment));
			events = new ArrayRef<Event>(typeof(Event));
			lights = new ArrayRef<Light>(typeof(Light));
			cameras = new ArrayRef<Camera>(typeof(Camera));
			ribbonEmitters = new ArrayRef<Ribbon>(typeof(Ribbon));
			particleEmitters = new ArrayRef<Particle>(typeof(Particle));
			unknown = null;
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
			//TODO Maybe opening each time even when it's an alias a new RaF could be the source of bugs during writing?
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


		protected internal virtual int[] computeAliasLookup()
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
//ORIGINAL LINE: public jm2lib.blizzard.wow.M2Format upConvert() throws Exception
		public override M2Format upConvert()
		{
			jm2lib.blizzard.wow.cataclysm.Model output = new jm2lib.blizzard.wow.cataclysm.Model();
			output.name = name;
			output.globalModelFlags = globalModelFlags;
			output.globalSequences = globalSequences;
			output.animations = animations;
			output.animationLookup = animationLookup;
			output.bones = bones;
			output.keyBoneLookup = keyBoneLookup;
			output.vertices = vertices;
			for (int i = 0; i < views.Count; i++)
			{
				output.views.Add(views[i].upConvert());
			}
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
			//TODO Cameras upconversion
			if (cameras.Count > 0)
			{
				Console.Error.WriteLine("Warning : Cameras conversion not implemented");
			}
			/*
			for (int i = 0; i < cameras.size(); i++){
				output.cameras.add(cameras.get(i).upConvert());
			}
			*/
			output.cameraLookup = cameraLookup;
			output.ribbonEmitters = ribbonEmitters;
			for (int i = 0; i < particleEmitters.Count; i++)
			{
				output.particleEmitters.Add(particleEmitters[i].upConvert());
			}
			output.unknown = unknown;
			return output;
		}

		/// <summary>
		/// Time between each sequence </summary>
		public const int TIME_BETWEEN_SEQUENCES = 3333;
		/// <summary>
		/// When the timeline starts </summary>
		public const int INITIAL_TIME = 3333;
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.M2Format downConvert() throws Exception
		public override M2Format downConvert()
		{
			jm2lib.blizzard.wow.lateburningcrusade.Model output = base.clone();
			output.globalModelFlags &= 0x7;

			for (int i = 0, time = INITIAL_TIME; i < animations.Count; i++)
			{
				output.animations.add(animations[i].downConvert(time));
				time = output.animations.get(i).timeEnd + TIME_BETWEEN_SEQUENCES;
			}

			//Always override default animationLookup. I don't know why the original looks so bugged.
			if (animations.Count > 0)
			{
				output.animationLookup = LookupBuilder.buildAnimLookup(output.animations);
			}
			output.playableAnimationLookup = LookupBuilder.buildPlayAnimLookup(output.animationLookup);

			for (int i = 0; i < bones.Count; i++)
			{
				output.bones.add(bones[i].downConvert(output.animations));
			}
			for (int i = 0; i < views.Count; i++)
			{
				output.views.add(views[i].downConvert());
			}
			for (int i = 0; i < submeshAnimations.Count; i++)
			{
				output.submeshAnimations.add(submeshAnimations[i].downConvert(output.animations));
			}
			for (int i = 0; i < transparency.Count; i++)
			{
				output.transparency.add(transparency[i].downConvert(output.animations));
			}
			for (int i = 0; i < uvAnimations.Count; i++)
			{
				output.uvAnimations.add(uvAnimations[i].downConvert(output.animations));
			}
			for (int i = 0; i < attachments.Count; i++)
			{
				output.attachments.add(attachments[i].downConvert(output.animations));
			}
			if (attachLookup.size() > 37)
			{
				output.attachLookup = new ArrayRef<short?>(short.TYPE);
				output.attachLookup.addAll(attachLookup.subList(0, 36));
			}
			for (int i = 0; i < events.Count; i++)
			{
				output.events.add(events[i].downConvert(output.animations));
			}
			for (int i = 0; i < lights.Count; i++)
			{
				output.lights.add(lights[i].downConvert(output.animations));
			}
			for (int i = 0; i < cameras.Count; i++)
			{
				output.cameras.add(cameras[i].downConvert(output.animations));
			}
			for (int i = 0; i < ribbonEmitters.Count; i++)
			{
				output.ribbonEmitters.add(ribbonEmitters[i].downConvert(output.animations));
			}
			for (int i = 0; i < particleEmitters.Count; i++)
			{
				output.particleEmitters.Add(particleEmitters[i].downConvert(output.animations));
			}
			return output;
		}

		/// <summary>
		/// SHALLOW COPY
		/// </summary>
		public override Model clone()
		{
			Model clone = new Model();
			clone.version = LICH_KING;
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

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\ttextures: ").Append(textures).Append("\n}");
			return builder.ToString();
		}
	}

}