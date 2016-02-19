using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.sc2
{


	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Header : Indexer
	{
		private readonly int version;

		public Reference<sbyte?> name = new Reference<sbyte?>();
		public int flags;
		public Reference<Sequence> sequences = new Reference<Sequence>();
		public Reference<STC> stc = new Reference<STC>();
		public Reference<STG> stg = new Reference<STG>();
		public float unknown0;
		public float unknown1;
		public float unknown2;
		public float unknown3;
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> sts = new Reference<>();
		public Reference<?> sts = new Reference<?>();
		public Reference<Bone> bones = new Reference<Bone>();
		public int skinBones;
		public int vertexFlags;
		public Reference<sbyte?> vertices = new Reference<sbyte?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> divisions = new Reference<>();
		public Reference<?> divisions = new Reference<?>();
		public Reference<char?> boneLookup = new Reference<char?>();
		public BoundingSphere boundings = new BoundingSphere();
		public int[] unknown4To19 = new int[16]; //16
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> attachmentPoints = new Reference<>();
		public Reference<?> attachmentPoints = new Reference<?>();
		public Reference<char?> attachmentPointsAddons = new Reference<char?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> lights = new Reference<>();
		public Reference<?> lights = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> shbx = new Reference<>();
		public Reference<?> shbx = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> cameras = new Reference<>();
		public Reference<?> cameras = new Reference<?>();
		public Reference<char?> unknown20 = new Reference<char?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> materialMaps = new Reference<>();
		public Reference<?> materialMaps = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> standardMaterials = new Reference<>();
		public Reference<?> standardMaterials = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> displacementMaterials = new Reference<>();
		public Reference<?> displacementMaterials = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> compositeMaterials = new Reference<>();
		public Reference<?> compositeMaterials = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> terrainMaterials = new Reference<>();
		public Reference<?> terrainMaterials = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> volumeMaterials = new Reference<>();
		public Reference<?> volumeMaterials = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> volumeNoiseMaterials = new Reference<>();
		public Reference<?> volumeNoiseMaterials = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> creepMaterials = new Reference<>();
		public Reference<?> creepMaterials = new Reference<?>();
		public Reference<sbyte?> unknown21 = new Reference<sbyte?>(); //if v > 24
		public Reference<sbyte?> unknown22 = new Reference<sbyte?>(); //if v > 25
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> particleEmitters = new Reference<>();
		public Reference<?> particleEmitters = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> particleEmittersCopies = new Reference<>();
		public Reference<?> particleEmittersCopies = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> ribbonEmitters = new Reference<>();
		public Reference<?> ribbonEmitters = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> projections = new Reference<>();
		public Reference<?> projections = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> forces = new Reference<>();
		public Reference<?> forces = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> warps = new Reference<>();
		public Reference<?> warps = new Reference<?>();
		public Reference<sbyte?> unknown23 = new Reference<sbyte?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> rigidBodies = new Reference<>();
		public Reference<?> rigidBodies = new Reference<?>();
		public Reference<sbyte?> unknown24 = new Reference<sbyte?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> physicJoints = new Reference<>();
		public Reference<?> physicJoints = new Reference<?>();
		public Reference<sbyte?> unknown25 = new Reference<sbyte?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> ikjt = new Reference<>();
		public Reference<?> ikjt = new Reference<?>();
		public Reference<sbyte?> unknown26 = new Reference<sbyte?>();
		public Reference<sbyte?> unknown27 = new Reference<sbyte?>(); //if v > 24
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> patu = new Reference<>();
		public Reference<?> patu = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> trgd = new Reference<>();
		public Reference<?> trgd = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> initialReference = new Reference<>();
		public Reference<?> initialReference = new Reference<?>();
		public SSGS tightHitTest = new SSGS();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> fuzzyHitTestObjects = new Reference<>();
		public Reference<?> fuzzyHitTestObjects = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> attachmentVolums = new Reference<>();
		public Reference<?> attachmentVolums = new Reference<?>();
		public Reference<char?> attachmentVolumsAddons0 = new Reference<char?>();
		public Reference<char?> attachmentVolumsAddons1 = new Reference<char?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> bbsc = new Reference<>();
		public Reference<?> bbsc = new Reference<?>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public Reference<?> tmd = new Reference<>();
		public Reference<?> tmd = new Reference<?>();
		public int unknown28;
		public Reference<int?> unknown29 = new Reference<int?>();

		public Header(int version)
		{
			this.version = version;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			name.unmarshal(@in);
			flags = @in.readInt();
			sequences.unmarshal(@in);
			stc.unmarshal(@in);
			stg.unmarshal(@in);
			unknown0 = @in.readFloat();
			unknown1 = @in.readFloat();
			unknown2 = @in.readFloat();
			unknown3 = @in.readFloat();
			sts.unmarshal(@in);
			bones.unmarshal(@in);
			skinBones = @in.readInt();
			vertexFlags = @in.readInt();
			vertices.unmarshal(@in);
			divisions.unmarshal(@in);
			boneLookup.unmarshal(@in);
			boundings.unmarshal(@in);
			for (int i = 0; i < unknown4To19.Length; i++)
			{
				unknown4To19[i] = @in.readInt();
			}
			attachmentPoints.unmarshal(@in);
			attachmentPointsAddons.unmarshal(@in);
			lights.unmarshal(@in);
			shbx.unmarshal(@in);
			cameras.unmarshal(@in);
			unknown20.unmarshal(@in);
			materialMaps.unmarshal(@in);
			standardMaterials.unmarshal(@in);
			displacementMaterials.unmarshal(@in);
			compositeMaterials.unmarshal(@in);
			terrainMaterials.unmarshal(@in);
			volumeMaterials.unmarshal(@in);
			volumeNoiseMaterials.unmarshal(@in);
			creepMaterials.unmarshal(@in);
			if (version > 24)
			{
				unknown21.unmarshal(@in);
			}
			if (version > 25)
			{
				unknown22.unmarshal(@in);
			}
			particleEmitters.unmarshal(@in);
			particleEmittersCopies.unmarshal(@in);
			ribbonEmitters.unmarshal(@in);
			projections.unmarshal(@in);
			forces.unmarshal(@in);
			warps.unmarshal(@in);
			unknown23.unmarshal(@in);
			rigidBodies.unmarshal(@in);
			unknown24.unmarshal(@in);
			physicJoints.unmarshal(@in);
			unknown25.unmarshal(@in);
			ikjt.unmarshal(@in);
			unknown26.unmarshal(@in);
			if (version > 24)
			{
				unknown27.unmarshal(@in);
			}
			patu.unmarshal(@in);
			trgd.unmarshal(@in);
			initialReference.unmarshal(@in);
			tightHitTest.unmarshal(@in);
			fuzzyHitTestObjects.unmarshal(@in);
			attachmentVolums.unmarshal(@in);
			attachmentVolumsAddons0.unmarshal(@in);
			attachmentVolumsAddons1.unmarshal(@in);
			bbsc.unmarshal(@in);
			tmd.unmarshal(@in);
			unknown28 = @in.readInt();
			unknown29.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			// TODO Auto-generated method stub

		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				name.Entries = value;
				sequences.Entries = value;
				stc.Entries = value;
				stg.Entries = value;
				sts.Entries = value;
				bones.Entries = value;
				vertices.Entries = value;
				divisions.Entries = value;
				boneLookup.Entries = value;
				attachmentPoints.Entries = value;
				attachmentPointsAddons.Entries = value;
				lights.Entries = value;
				shbx.Entries = value;
				cameras.Entries = value;
				unknown20.Entries = value;
				materialMaps.Entries = value;
				standardMaterials.Entries = value;
				displacementMaterials.Entries = value;
				compositeMaterials.Entries = value;
				terrainMaterials.Entries = value;
				volumeMaterials.Entries = value;
				volumeNoiseMaterials.Entries = value;
				creepMaterials.Entries = value;
				if (version > 24)
				{
					unknown21.Entries = value;
				}
				if (version > 25)
				{
					unknown22.Entries = value;
				}
				particleEmitters.Entries = value;
				particleEmittersCopies.Entries = value;
				ribbonEmitters.Entries = value;
				projections.Entries = value;
				forces.Entries = value;
				warps.Entries = value;
				unknown23.Entries = value;
				rigidBodies.Entries = value;
				unknown24.Entries = value;
				physicJoints.Entries = value;
				unknown25.Entries = value;
				ikjt.Entries = value;
				unknown26.Entries = value;
				if (version > 24)
				{
					unknown27.Entries = value;
				}
				patu.Entries = value;
				trgd.Entries = value;
				initialReference.Entries = value;
				fuzzyHitTestObjects.Entries = value;
				attachmentVolums.Entries = value;
				attachmentVolumsAddons0.Entries = value;
				attachmentVolumsAddons1.Entries = value;
				bbsc.Entries = value;
				tmd.Entries = value;
				unknown29.Entries = value;
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tversion: ").Append(version).Append("\n\tname: ").Append(name).Append("\n\tflags: ").Append(flags).Append("\n\tsequences: ").Append(sequences).Append("\n\tunknown0: ").Append(unknown0).Append("\n\tunknown1: ").Append(unknown1).Append("\n\tunknown2: ").Append(unknown2).Append("\n\tunknown3: ").Append(unknown3).Append("\n\tsts: ").Append(sts).Append("\n\tbones: ").Append(bones).Append("\n\tskinBones: ").Append(skinBones).Append("\n\tvertexFlags: ").Append(vertexFlags).Append("\n\tvertices: ").Append(vertices).Append("\n\tdivisions: ").Append(divisions).Append("\n\tboneLookup: ").Append(boneLookup).Append("\n\tboundings: ").Append(boundings).Append("\n\tunknown4To19: ").Append(Arrays.ToString(unknown4To19)).Append("\n\tattachmentPoints: ").Append(attachmentPoints).Append("\n\tattachmentPointsAddons: ").Append(attachmentPointsAddons).Append("\n\tlights: ").Append(lights).Append("\n\tshbx: ").Append(shbx).Append("\n\tcameras: ").Append(cameras).Append("\n\tunknown20: ").Append(unknown20).Append("\n\tmaterialMaps: ").Append(materialMaps).Append("\n\tstandardMaterials: ").Append(standardMaterials).Append("\n\tdisplacementMaterials: ").Append(displacementMaterials).Append("\n\tcompositeMaterials: ").Append(compositeMaterials).Append("\n\tterrainMaterials: ").Append(terrainMaterials).Append("\n\tvolumeMaterials: ").Append(volumeMaterials).Append("\n\tvolumeNoiseMaterials: ").Append(volumeNoiseMaterials).Append("\n\tcreepMaterials: ").Append(creepMaterials).Append("\n\tunknown21: ").Append(unknown21).Append("\n\tunknown22: ").Append(unknown22).Append("\n\tparticleEmitters: ").Append(particleEmitters).Append("\n\tparticleEmittersCopies: ").Append(particleEmittersCopies).Append("\n\tribbonEmitters: ").Append(ribbonEmitters).Append("\n\tprojections: ").Append(projections).Append("\n\tforces: ").Append(forces).Append("\n\twarps: ").Append(warps).Append("\n\tunknown23: ").Append(unknown23).Append("\n\trigidBodies: ").Append(rigidBodies).Append("\n\tunknown24: ").Append(unknown24).Append("\n\tphysicJoints: ").Append(physicJoints).Append("\n\tunknown25: ").Append(unknown25).Append("\n\tikjt: ").Append(ikjt).Append("\n\tunknown26: ").Append(unknown26).Append("\n\tunknown27: ").Append(unknown27).Append("\n\tpatu: ").Append(patu).Append("\n\ttrgd: ").Append(trgd).Append("\n\tinitialReference: ").Append(initialReference).Append("\n\ttightHitTest: ").Append(tightHitTest).Append("\n\tfuzzyHitTestObjects: ").Append(fuzzyHitTestObjects).Append("\n\tattachmentVolums: ").Append(attachmentVolums).Append("\n\tattachmentVolumsAddons0: ").Append(attachmentVolumsAddons0).Append("\n\tattachmentVolumsAddons1: ").Append(attachmentVolumsAddons1).Append("\n\tbbsc: ").Append(bbsc).Append("\n\ttmd: ").Append(tmd).Append("\n\tunknown28: ").Append(unknown28).Append("\n\tunknown29: ").Append(unknown29).Append("\n}");
			return builder.ToString();
		}

	}

}