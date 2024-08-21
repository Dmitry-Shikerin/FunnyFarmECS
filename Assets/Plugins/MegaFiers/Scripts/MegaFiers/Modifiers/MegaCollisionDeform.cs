using UnityEngine;
using System.Collections.Generic;

namespace MegaFiers
{
	[System.Serializable]
	public class MegaColliderMesh
	{
		public int[]		tris;
		public Vector3[]	verts;
		public Vector3[]	normals;
		public Mesh			mesh;
		public GameObject	obj;
	}

	public enum MegaDeformType
	{
		Old,
		RayCast,
		NearPoint,
	}

	[AddComponentMenu("Modifiers/Collision Deform")]
	public class MegaCollisionDeform : MegaModifier
	{
		public GameObject		obj;
		public float			decay				= 1.0f;
		public bool				usedecay			= false;
		public Vector3			normal				= Vector3.up;
		List<int>				affected			= new List<int>();
		List<float>				distances			= new List<float>();
		Matrix4x4				mat					= new Matrix4x4();
		Vector3[]				offsets;
		Vector3[]				normals;
		public MegaDeformType	method				= MegaDeformType.NearPoint;
		public float			distance			= 0.0f;
		public float			bulgeExtendValue	= 0.0f;
		public float			bulgeValue			= 0.0f;
		public bool				bulge				= false;
		public AnimationCurve	bulgeCrv			= new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
		float[]					vertdist;
		Vector3[]				vertoffsets;
		Vector3[]				nearest;
		Vector3[]				vels;
		Collider				col;
		float[]					penetration;
		public GameObject		hitObject;
		public MegaColliderMesh	colmesh;
		public float			btime				= 1.0f;
		public float			retspd				= 1.0f;
		const float				EPSILON				= 0.000001f;

		public override string ModName() { return "Deform"; }
		public override string GetHelpURL() { return "Deform.htm"; }

		void DeformMeshMine()
		{
			if ( col == null )
				return;

			for ( int i = 0; i < jverts.Length; i++ )
			{
				Vector3 p = jverts[i] + offsets[i];
				Vector3 origin = transform.TransformPoint(p);
				if ( col.bounds.Contains(origin) )
				{
					RaycastHit hit;

					// Do ray from a distance away to the point, if hit then inside mesh
					Ray ray = new Ray(origin, normals[i]);

					if ( col.Raycast(ray, out hit, 10.0f) )
					{
						if ( hit.distance < 10.0f )
						{
							penetration[i] = hit.distance;
							offsets[i] = -(normals[i] * (hit.distance + 0.01f));
						}
					}
				}
			}

			if ( !usedecay )
			{
				for ( int i = 0; i < jverts.Length; i++ )
					jsverts[i] = jverts[i] + offsets[i];
			}
			else
			{
				for ( int i = 0; i < jverts.Length; i++ )
				{
					offsets[i].x *= decay;
					offsets[i].y *= decay;
					offsets[i].z *= decay;

					jsverts[i] = jverts[i] + offsets[i];
				}
			}
		}

		void DeformMesh()
		{
			float dst = distance * 0.001f;

			int index = -1;
			Vector3 bary = Vector3.zero;
			bool collision = false;

			for ( int i = 0; i < jverts.Length; i++ )
			{
				Vector3 p = jverts[i];
				Vector3 origin = transform.TransformPoint(p + (normals[i] * dst));
				if ( col.bounds.Contains(origin) )
				{
					// Point in collider space
					Vector3 lp = col.transform.worldToLocalMatrix.MultiplyPoint(origin);

					Vector3 np = MegaNearestPointTest.NearestPointOnMesh1(lp, colmesh.verts, colmesh.tris, ref index, ref bary);

					Vector3 dir = lp - np;

					Vector3 norm = FaceNormal(colmesh.verts, colmesh.tris, index);

					if ( Vector3.Dot(norm, dir) < 0.0f )
					{
						np = col.transform.localToWorldMatrix.MultiplyPoint(np);
						jsverts[i] = transform.worldToLocalMatrix.MultiplyPoint(np);
						jsverts[i] = jsverts[i] - (normals[i] * dst);
						collision = true;
					}
					else
						jsverts[i] = jverts[i];
				}
				else
					jsverts[i] = jverts[i];
			}

			if ( collision && bulge )
			{
				for ( int i = 0; i < jverts.Length; i++ )
				{
					Vector3 p = jverts[i];
					Vector3 origin = transform.TransformPoint(p + (normals[i] * dst));

					Vector3 lp = col.transform.worldToLocalMatrix.MultiplyPoint(origin);
					Vector3 np = MegaNearestPointTest.NearestPointOnMesh1(lp, colmesh.verts, colmesh.tris, ref index, ref bary);

					float bulgeDist = Vector3.Distance(lp, np);
					float relativedistance = bulgeDist / (bulgeExtendValue + 0.00001f);
					
					// get the bulge curve 
					float bulgeAmount = bulgeCrv.Evaluate(relativedistance);
					
					float delta = bulgeAmount * bulgeValue;
					// set the point position for indirect collision deformation
					jsverts[i] = jsverts[i] + normals[i] * delta;
				}
			}
		}

		[ContextMenu("Reset Offsets")]
		public void ResetOffsets()
		{
			vertdist = null;
		}

		void DeformMeshNew()
		{
			if ( vertdist == null || vertdist.Length != jverts.Length )
			{
				vertdist = new float[jverts.Length];
				vertoffsets = new Vector3[jverts.Length];
				nearest = new Vector3[jverts.Length];
				vels = new Vector3[jverts.Length];
			}

			float dst = distance * 0.001f;

			int index = -1;
			Vector3 bary = Vector3.zero;

			float totalpen = 0.0f;

			int count = 0;

			for ( int i = 0; i < jverts.Length; i++ )
			{
				Vector3 p = jverts[i];
				Vector3 origin = transform.TransformPoint(p + (normals[i] * dst));
				if ( col.bounds.Contains(origin) )
				{
					Vector3 lp = col.transform.worldToLocalMatrix.MultiplyPoint(origin);

					Vector3 np = MegaNearestPointTest.NearestPointOnMesh1(lp, colmesh.verts, colmesh.tris, ref index, ref bary);

					Vector3 dir = lp - np;
					float dist = Vector3.Distance(lp, np);

					Vector3 norm = FaceNormal(colmesh.verts, colmesh.tris, index);

					if ( Vector3.Dot(norm, dir) < 0.0f )
					{
						totalpen += dist;
						dist = -dist;
					}

					np = col.transform.localToWorldMatrix.MultiplyPoint(np);
					nearest[i] = transform.worldToLocalMatrix.MultiplyPoint(np);

					vertdist[i] = dist;
				}
				else
				{
					Vector3 lp = col.transform.worldToLocalMatrix.MultiplyPoint(origin);
					// Distance is calculated in here
					Vector3 np = MegaNearestPointTest.NearestPointOnMesh1(lp, colmesh.verts, colmesh.tris, ref index, ref bary);

					float dist = Vector3.Distance(lp, np);
					vertdist[i] = dist;
					np = col.transform.localToWorldMatrix.MultiplyPoint(np);
					nearest[i] = transform.worldToLocalMatrix.MultiplyPoint(np);
					count++;
				}
			}

			if ( totalpen == 0.0f )
			{
				for ( int i = 0; i < jverts.Length; i++ )
				{
					jsverts[i] = jverts[i] + vertoffsets[i];
					vertoffsets[i] *= retspd;
				}
			}
			else
			{
				for ( int i = 0; i < jverts.Length; i++ )
				{
					if ( vertdist[i] < 0.0f )
						vertoffsets[i] = nearest[i] - jverts[i];
					else
					{
						Vector3 newpos = normals[i] * ((totalpen / (float)count) * bulgeValue);
						vertoffsets[i] = Vector3.SmoothDamp(vertoffsets[i], newpos, ref vels[i], btime);
					}

					jsverts[i] = jverts[i] + vertoffsets[i];
					vertoffsets[i] *= retspd;
				}
			}
		}

		Vector3 FaceNormal(Vector3[] verts, int[] tris, int f)
		{
			Vector3 v30 = verts[tris[f]];
			Vector3 v31 = verts[tris[f + 1]];
			Vector3 v32 = verts[tris[f + 2]];

			float vax = v31.x - v30.x;
			float vay = v31.y - v30.y;
			float vaz = v31.z - v30.z;

			float vbx = v32.x - v31.x;
			float vby = v32.y - v31.y;
			float vbz = v32.z - v31.z;

			v30.x = vay * vbz - vaz * vby;
			v30.y = vaz * vbx - vax * vbz;
			v30.z = vax * vby - vay * vbx;

			// Uncomment this if you dont want normals weighted by poly size
			//float l = v30.x * v30.x + v30.y * v30.y + v30.z * v30.z;
			//l = 1.0f / Mathf.Sqrt(l);
			//v30.x *= l;
			//v30.y *= l;
			//v30.z *= l;

			return v30;
		}

		public override void Modify(MegaModifyObject mc)
		{
			switch ( method )
			{
				case MegaDeformType.Old:
					DeformMesh();
					break;
				case MegaDeformType.RayCast:
					DeformMeshMine();
					break;
				case MegaDeformType.NearPoint:
					DeformMeshNew();
					break;
			}
		}

		public override bool ModLateUpdate(MegaModContext mc)
		{
			return prepared;
		}

		public override bool Prepare(MegaModContext mc)
		{
			if ( colmesh == null )
				colmesh = new MegaColliderMesh();

			if ( colmesh.obj != hitObject && hitObject != null )
			{
				colmesh = new MegaColliderMesh();

				// This is colldier mesh not this mesh
				colmesh.mesh = MegaUtils.GetMesh(hitObject);
				colmesh.verts = colmesh.mesh.vertices;
				colmesh.tris = colmesh.mesh.triangles;
				colmesh.normals = colmesh.mesh.normals;
				colmesh.obj = hitObject;
			}

			if ( hitObject )
				col = hitObject.GetComponent<Collider>();

			if ( hitObject == null )
				return false;

			affected.Clear();
			distances.Clear();

			//if ( offsets == null || offsets.Length != mc.mod.verts.Length )
				//offsets = new Vector3[mc.mod.verts.Length];
			if ( offsets == null || offsets.Length != mc.mod.jverts.Length )
				offsets = new Vector3[mc.mod.jverts.Length];

			if ( normals == null || normals.Length != verts.Length )
				normals = mc.mod.mesh.normals;	// get current normals

			if ( penetration == null || penetration.Length != mc.mod.jverts.Length )
				penetration = new float[mc.mod.jverts.Length];
			mat = Matrix4x4.identity;

			SetAxis(mat);

			return true;
		}

		int intersect_triangle(Vector3 orig, Vector3 dir, Vector3 vert0, Vector3 vert1, Vector3 vert2, out float t, out float u, out float v)
		{
			Vector3 edge1,edge2,tvec,pvec,qvec;
			float det,inv_det;

			// find vectors for two edges sharing vert0
			edge1 = vert1 - vert0;
			edge2 = vert2 - vert0;
		
			/* begin calculating determinant - also used to calculate U parameter */
			pvec = Vector3.Cross(dir, edge2);

			/* if determinant is near zero, ray lies in plane of triangle */
			det = Vector3.Dot(edge1, pvec);

			t = u = v = 0.0f;	// or use ref

	#if TEST_CULL           // define TEST_CULL if culling is desired
			if ( det < EPSILON )
				return 0;

			// calculate distance from vert0 to ray origin
			//SUB(tvec, orig, vert0);
			tvec = orig - vert0;

			/* calculate U parameter and test bounds */
			//*u = DOT(tvec, pvec);
			u = Vector3.Dot(tvec, pvec);
			if ( u < 0.0f || u > det )
				return 0;

			/* prepare to test V parameter */
			//CROSS(qvec, tvec, edge1);
			qvec = Vector3.Cross(tvec, edge1);

			/* calculate V parameter and test bounds */
			//*v = DOT(dir, qvec);
			v = Vector3.Dot(dir, qvec);
			if ( v < 0.0f || u + v > det )
				return 0;

			/* calculate t, scale parameters, ray intersects triangle */
			//*t = DOT(edge2, qvec);
			t = Vector3.Dot(edge2, qvec);
			inv_det = 1.0f / det;
			t *= inv_det;
			u *= inv_det;
			v *= inv_det;
	#else                    // the non-culling branch
			if ( det > -EPSILON && det < EPSILON )
				return 0;

			inv_det = 1.0f / det;

			/* calculate distance from vert0 to ray origin */
			tvec = orig - vert0;

			/* calculate U parameter and test bounds */
			u = Vector3.Dot(tvec, pvec) * inv_det;
			if ( u < 0.0f || u > 1.0f )
				return 0;

			/* prepare to test V parameter */
			qvec = Vector3.Cross(tvec, edge1);

			/* calculate V parameter and test bounds */
			v = Vector3.Dot(dir, qvec) * inv_det;
			if ( v < 0.0f || u + v > 1.0f )
				return 0;

			/* calculate t, ray intersects triangle */
			t = Vector3.Dot(edge2, qvec) * inv_det;
	#endif
			return 1;
		}
	}
}