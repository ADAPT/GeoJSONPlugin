﻿using GeoJSON.Net.Geometry;
using NetTopologySuite.Geometries.Utilities;
using System.Collections.Generic;

namespace GeoJSONPlugin.Mappers.GeoJson
{
	public class LineStringMapper
	{
		#region Export
		public static GeoJSON.Net.Geometry.LineString MapLinearRing(AgGateway.ADAPT.ApplicationDataModel.Shapes.LinearRing adaptLinearRing, AffineTransformation affineTransformation = null)
		{
			var lineString = MapLineString(adaptLinearRing, affineTransformation);

			// [Check] for https://tools.ietf.org/html/rfc7946#section-3.1.6 to ensure no ArgumentException from GeoJSON.Net when adding the lineString to a Polygon
			if (!lineString.IsLinearRing())
			{
				lineString = MakeClosedLinearRing(lineString);

				if (lineString.Coordinates.Count < 4)
				{
					return null;
				}
			}

			return lineString;
		}

//<<<<<<< HEAD
        public static GeoJSON.Net.Geometry.LineString MapLineString(AgGateway.ADAPT.ApplicationDataModel.Shapes.Point a, AgGateway.ADAPT.ApplicationDataModel.Shapes.Point b, AffineTransformation affineTransformation = null)
        {
			var positions = new List<Position>();
			var positionA = PointMapper.MapPoint(a, affineTransformation);
			var positionB = PointMapper.MapPoint(b, affineTransformation);
			positions.Add(positionA);
			positions.Add(positionB);

			return new GeoJSON.Net.Geometry.LineString(positions);
		}

        public static GeoJSON.Net.Geometry.LineString MapLineString(AgGateway.ADAPT.ApplicationDataModel.Shapes.LineString adaptLineString, AffineTransformation affineTransformation = null)
        {
			var positions = new List<Position>();
            foreach (var point in adaptLineString.Points)
            {
				var position = PointMapper.MapPoint(point, affineTransformation);
				positions.Add(position);
            }

			return new GeoJSON.Net.Geometry.LineString(positions);
        }

        public static GeoJSON.Net.Geometry.LineString MapLineString(AgGateway.ADAPT.ApplicationDataModel.Shapes.LinearRing adaptLinearRing, AffineTransformation affineTransformation = null)
		{
			var positions = new List<Position>();
			foreach (var point in adaptLinearRing.Points)
			{
				var position = PointMapper.MapPoint(point, affineTransformation);
				positions.Add(position);
			}

			return new GeoJSON.Net.Geometry.LineString(positions);
		}

		public static GeoJSON.Net.Geometry.LineString MakeClosedLinearRing(GeoJSON.Net.Geometry.LineString lineString)
		{
			if (!lineString.IsClosed())
			{
				var positions = new List<Position>();
				foreach (var coordinate in lineString.Coordinates)
				{
					positions.Add((Position)coordinate);
				}
				// Add first position also as last position
				positions.Add((Position)lineString.Coordinates[0]);
				lineString = new GeoJSON.Net.Geometry.LineString(positions);
			}

			return lineString;
		}
		#endregion

		#region Import
		public static AgGateway.ADAPT.ApplicationDataModel.Shapes.LinearRing MapLineString(GeoJSON.Net.Geometry.LineString lineString, AffineTransformation affineTransformation = null)
		{
			// ToDo: [Check] if the LineString is actually a LinearRing
			var linearRing = new AgGateway.ADAPT.ApplicationDataModel.Shapes.LinearRing();

			foreach (var position in lineString.Coordinates)
			{
				linearRing.Points.Add(PointMapper.MapPosition(position, affineTransformation));
			}

			return linearRing;
		}
		#endregion
	}
}