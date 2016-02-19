using System;
using System.Text;

namespace jm2lib.blizzard.wc3
{

	using jm2lib.blizzard.common.types;
	using jm2lib.blizzard.common.types;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using BlizzardFile = jm2lib.blizzard.io.BlizzardFile;
	using Bone = jm2lib.blizzard.wc3.mdx.Bone;
	using Geoset = jm2lib.blizzard.wc3.mdx.Geoset;
	using GeosetAnimation = jm2lib.blizzard.wc3.mdx.GeosetAnimation;
	using Light = jm2lib.blizzard.wc3.mdx.Light;
	using Material = jm2lib.blizzard.wc3.mdx.Material;
	using Model = jm2lib.blizzard.wc3.mdx.Model;
	using Sequence = jm2lib.blizzard.wc3.mdx.Sequence;
	using SoundTrack = jm2lib.blizzard.wc3.mdx.SoundTrack;
	using Texture = jm2lib.blizzard.wc3.mdx.Texture;
	using TextureAnimation = jm2lib.blizzard.wc3.mdx.TextureAnimation;
	using Animation = jm2lib.blizzard.wow.classic.Animation;
	using LookupBuilder = jm2lib.blizzard.wow.classic.LookupBuilder;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Warcraft 3 : The Frozen Throne Model
	/// 
	/// </summary>
	public class MDX : BlizzardFile
	{
		public Chunk<int?> version;
		public Chunk<Model> model;
		public Chunk<Sequence> sequences;
		public Chunk<int?> globalSequences;
		public Chunk<Texture> textures;
		public Chunk<SoundTrack> sounds;
		public Chunk<Material> materials;
		public Chunk<TextureAnimation> textureAnimations;
		public Chunk<Geoset> geosets;
		public Chunk<GeosetAnimation> geosetAnimations;
		public Chunk<Bone> bones;
		public Chunk<Light> lights;
		public Chunk<Vec3F> pivots;
		public Chunk<CollisionShape> collisions;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			while (@in.FilePointer < @in.length())
			{
				string magic = @in.readString(4);
				switch (magic)
				{
				case "VERS":
					version = new Chunk<>(int.TYPE, magic);
					version.unmarshal(@in);
					break;
				case "MODL":
					model = new Chunk<>(typeof(Model), magic);
					model.unmarshal(@in);
					break;
				case "SEQS":
					sequences = new Chunk<>(typeof(Sequence), magic);
					sequences.unmarshal(@in);
					break;
				case "GLBS":
					globalSequences = new Chunk<>(int.TYPE, magic);
					globalSequences.unmarshal(@in);
					break;
				case "TEXS":
					textures = new Chunk<>(typeof(Texture), magic);
					textures.unmarshal(@in);
					break;
				case "SNDS":
					sounds = new Chunk<>(typeof(SoundTrack), magic);
					sounds.unmarshal(@in);
					break;
				case "MTLS":
					materials = new Chunk<>(typeof(Material), magic);
					materials.unmarshal(@in);
					break;
				case "TXAN":
					textureAnimations = new Chunk<>(typeof(TextureAnimation), magic);
					textureAnimations.unmarshal(@in);
					break;
				case "GEOS":
					geosets = new Chunk<>(typeof(Geoset), magic);
					geosets.unmarshal(@in);
					break;
				case "GEOA":
					geosetAnimations = new Chunk<>(typeof(GeosetAnimation), magic);
					geosetAnimations.unmarshal(@in);
					break;
				case "BONE":
					bones = new Chunk<>(typeof(Bone), magic);
					bones.unmarshal(@in);
					break;
				case "LITE":
					lights = new Chunk<>(typeof(Light), magic);
					lights.unmarshal(@in);
					break;
				//TODO HELP, ATCH
				case "PIVT":
					pivots = new Chunk<>(typeof(Vec3F), magic);
					pivots.unmarshal(@in);
					break;
				case "CLID":
					collisions = new Chunk<>(typeof(CollisionShape), magic);
					collisions.unmarshal(@in);
					break;
				//TODO PREM, PRE2, RIBB, EVTS, CAMS
				default:
					Console.Error.WriteLine("Skipping Chunk [" + magic + "]");
					@in.seek(@in.readInt() + @in.FilePointer);
				break;
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			if (version != null)
			{
				version.marshal(@out);
			}
			if (model != null)
			{
				model.marshal(@out);
			}
			if (sequences != null)
			{
				sequences.marshal(@out);
			}
			if (globalSequences != null)
			{
				globalSequences.marshal(@out);
			}
			if (textures != null)
			{
				textures.marshal(@out);
			}
			if (sounds != null)
			{
				sounds.marshal(@out);
			}
			if (materials != null)
			{
				materials.marshal(@out);
			}
			if (textureAnimations != null)
			{
				textureAnimations.marshal(@out);
			}
			if (geosets != null)
			{
				geosets.marshal(@out);
			}
			if (geosetAnimations != null)
			{
				geosetAnimations.marshal(@out);
			}
			if (bones != null)
			{
				bones.marshal(@out);
			}
			if (lights != null)
			{
				lights.marshal(@out);
			}
			if (pivots != null)
			{
				pivots.marshal(@out);
			}
		}

		public virtual jm2lib.blizzard.wow.classic.Model upConvert()
		{
			jm2lib.blizzard.wow.classic.Model output = new jm2lib.blizzard.wow.classic.Model();
			if (model.Count > 0)
			{
				output.name = new ArrayRef<sbyte?>(model[0].name);
			}
			if (globalSequences != null)
			{
				output.globalSequences.AddRange(globalSequences);
			}
			if (sequences != null)
			{
				foreach (Sequence item in sequences)
				{
					output.animations.Add(item.upConvert(model[0].blendTime));
				}
			}
			Animation.linkAnimations(output.animations);
			output.animationLookup = LookupBuilder.buildAnimLookup(output.animations);
			output.playableAnimationLookup = LookupBuilder.buildPlayAnimLookup(output.animationLookup);
			if (bones != null)
			{
				foreach (Bone item in bones)
				{
					output.bones.Add(item.upConvert(pivots));
				}
			}
			//TODO KeyBone Lookup
			if (geosets != null)
			{
				foreach (Geoset item in geosets)
				{
					output.vertices.AddRange(item.Vertices);
				}
			}
			//TODO Views
			if (geosetAnimations != null)
			{
				foreach (GeosetAnimation geoAnim in geosetAnimations)
				{
					output.submeshAnimations.Add(geoAnim.upConvert());
				}
			}
			if (textures != null)
			{
				foreach (Texture item in textures)
				{
					output.textures.Add(item.upConvert());
				}
			}
			//TODO Use rest of Model
			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			/*
			builder.append(getClass().getName()).append(" {\n\tversion: ").append(version).append("\n\tmodel: ")
					.append(model).append("\n\tsequences: ").append(sequences).append("\n\tglobalSequences: ")
					.append(globalSequences).append("\n\ttextures: ").append(textures).append("\n\tsounds: ").append(sounds)
					.append("\n\tmaterials: ").append(materials).append("\n\ttextureAnimations: ").append(textureAnimations)
					.append("\n\tgeosets: ").append(geosets).append("\n}");
			builder.append("\n\tgeosetAnimations: ").append(geosetAnimations).append("\n}");*/
			//builder.append("\n\tbones: ").append(bones).append("\n}");
			//builder.append("\n\tpivots: ").append(pivots).append("\n}");
			//builder.append("\n\tlights: ").append(lights).append("\n}");
			return builder.ToString();
		}
	}

}