using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{


	using Vec2F = jm2lib.blizzard.common.types.Vec2F;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Vertex = jm2lib.blizzard.wow.classic.Vertex;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Geoset : Marshalable
	{
		public ArrayMagic<Vec3F> vertexPositions; //m2 Vertex#Position
		public ArrayMagic<Vec3F> vertexNormals; //m2 Vertex#Normal
		public ArrayMagic<int?> faceTypeGroups; //m2 irrelevant
		public ArrayMagic<int?> faceGroups;
		public ArrayMagic<char?> faces; //m2 : skinHeader#triangles
		public ArrayMagic<sbyte?> vertexGroups; //m2 : skinHeader#properties
		public ArrayMagic<int?> matrixGroups; //m2 : Vertex#Bone indices. getMatrix() use matrixGr&Ind there's 1 per vertex
		public ArrayMagic<int?> matrixIndices;
		public int materialId;
		public int selectionGroup;
		public int selectionFlags;
		public Extent extent;
		public List<Extent> extents;
		public ArrayMagic<TextureCoordinateSet> textureCoordinateSets; //m2 : Vertex#textureCoords

		public Geoset()
		{
			vertexPositions = new ArrayMagic<Vec3F>(typeof(Vec3F), "VRTX");
			vertexNormals = new ArrayMagic<Vec3F>(typeof(Vec3F), "NRMS");
			faceTypeGroups = new ArrayMagic<int?>(int.TYPE, "PTYP");
			faceGroups = new ArrayMagic<int?>(int.TYPE, "PCNT");
			faces = new ArrayMagic<char?>(char.TYPE, "PVTX");
			vertexGroups = new ArrayMagic<sbyte?>(sbyte.TYPE, "GNDX");
			matrixGroups = new ArrayMagic<int?>(int.TYPE, "MTGX");
			matrixIndices = new ArrayMagic<int?>(int.TYPE, "MATS");
			extent = new Extent();
			extents = new List<Extent>();
			textureCoordinateSets = new ArrayMagic<TextureCoordinateSet>(typeof(TextureCoordinateSet), "UVAS");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			//FIXME Fixed size in the spec, but there is an inclusive size which must be there for a reason..
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unused") long endOffset = in.getFilePointer() + in.readInt();
			long endOffset = @in.FilePointer + @in.readInt();
			vertexPositions.unmarshal(@in);
			vertexNormals.unmarshal(@in);
			faceTypeGroups.unmarshal(@in);
			faceGroups.unmarshal(@in);
			faces.unmarshal(@in);
			vertexGroups.unmarshal(@in);
			matrixGroups.unmarshal(@in);
			matrixIndices.unmarshal(@in);
			materialId = @in.readInt();
			selectionGroup = @in.readInt();
			selectionFlags = @in.readInt();
			extent.unmarshal(@in);
			int count = @in.readInt();
			for (int i = 0; i < count; i++)
			{
				extents.Add((Extent) @in.readGeneric(typeof(Extent)));
			}
			textureCoordinateSets.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			long sizeOffset = @out.FilePointer;
			@out.writeInt(0);
			vertexPositions.marshal(@out);
			vertexNormals.marshal(@out);
			faceTypeGroups.marshal(@out);
			faceGroups.marshal(@out);
			faces.marshal(@out);
			vertexGroups.marshal(@out);
			matrixGroups.marshal(@out);
			matrixIndices.marshal(@out);

			@out.writeInt(materialId);
			@out.writeInt(selectionGroup);
			@out.writeInt(selectionFlags);
			extent.marshal(@out);

			@out.writeInt(extents.Count);
			foreach (Extent value in extents)
			{
				value.marshal(@out);
			}

			textureCoordinateSets.marshal(@out);

			int size = (int)(@out.FilePointer - sizeOffset);
			long currentOffset = @out.FilePointer;
			@out.seek(sizeOffset);
			@out.writeInt(size);
			@out.seek(currentOffset);
		}

		public virtual List<List<int?>> Matrix
		{
			get
			{
				List<List<int?>> matrix = new List<List<int?>>();
				int globalIndex = 0;
				foreach (int count in matrixGroups)
				{
					List<int?> values = new List<int?>();
					for (int i = 0; i < count; i++)
					{
						values.Add(matrixIndices[globalIndex]);
						globalIndex++;
					}
					matrix.Add(values);
				}
				return matrix;
			}
		}

		public virtual List<Vertex> Vertices
		{
			get
			{
				List<Vertex> vertices = new List<Vertex>();
				List<List<int?>> matrix = Matrix;
				for (int i = 0; i < vertexPositions.Count; i++)
				{
					Vertex vertex = new Vertex();
					int matrixIndex = vertexGroups[i];
					vertex.pos = vertexPositions[i];
					// Partition of 255 (100%)
					List<int?> partition = new List<int?>();
					int dividedValue = (int) Math.Ceiling(255.0 / matrixGroups[matrixIndex]);
					int n = 255;
					while (n > dividedValue)
					{
						n -= dividedValue;
						partition.Add(dividedValue);
					}
					partition.Add(n);
    
					for (int j = 0; j < partition.Count; j++)
					{
						vertex.boneWeights[j] = (short)partition[j];
					}
    
					for (int j = 0; j < matrix[matrixIndex].Count; j++)
					{
						vertex.boneIndices[j] = (short)matrix[matrixIndex][j];
					}
    
					vertex.normal = vertexNormals[i];
					//Should support up to 2 textureCoordinateSets
					for (int j = 0; j < textureCoordinateSets.Count; j++)
					{
						Debug.Assert(j <= vertex.texCoords.Length);
						vertex.texCoords[j] = textureCoordinateSets[j].get(i);
					}
					vertices.Add(vertex);
				}
				return vertices;
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tvertexPositions: ").Append(vertexPositions).Append("\n\tvertexNormals: ").Append(vertexNormals).Append("\n\tfaceTypeGroups: ").Append(faceTypeGroups).Append("\n\tfaceGroups: ").Append(faceGroups);
			builder.Append("\nfaces:{");
			foreach (char face in faces)
			{
				builder.Append((int) face + ":");
			}
			builder.Append("}");
			builder.Append("\n\tvertexGroups: ").Append(vertexGroups).Append("\n\tmatrixGroups: ").Append(matrixGroups).Append("\n\tmatrixIndices: ").Append(matrixIndices).Append("\n\tmaterialId: ").Append(materialId).Append("\n\tselectionGroup: ").Append(selectionGroup).Append("\n\tselectionFlags: ").Append(selectionFlags).Append("\n\textent: ").Append(extent).Append("\n\textents: ").Append(extents).Append("\n\ttextureCoordinateSets: ").Append(textureCoordinateSets).Append("\n}");
			return builder.ToString();
		}

		public class ArrayMagic<T> : List<T>, Marshalable
		{
			internal const long serialVersionUID = 2934017788615761081L;
			/// <summary>
			/// The type of the content </summary>
			internal readonly Type type;
			internal readonly string magic;

			public ArrayMagic(Type type, string magic)
			{
				this.type = type;
				Debug.Assert(magic.Length == 4);
				this.magic = magic;
			}
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
			public virtual void unmarshal(UnmarshalingStream @in)
			{
				@in.readInt();
				int count = @in.readInt();
				for (int i = 0; i < count; i++)
				{
					this.Add((T) @in.readGeneric(type));
				}
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
			public virtual void marshal(MarshalingStream @out)
			{
				@out.write(magic.GetBytes(StandardCharsets.UTF_8));
				@out.writeInt(this.Count);
				foreach (T value in this)
				{
					@out.writeGeneric(type, value);
				}
			}
		}

		public class TextureCoordinateSet : ArrayMagic<Vec2F>
		{
			internal const long serialVersionUID = 4094967909659297679L;
			public TextureCoordinateSet() : base(typeof(Vec2F), "UVBS")
			{
			}
		}
	}
}