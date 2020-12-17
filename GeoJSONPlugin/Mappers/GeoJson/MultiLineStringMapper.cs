using GeoJSON.Net.Geometry;
using System.Collections.Generic;

namespace GeoJSONPlugin.Mappers.GeoJson
{
    public class MultiLineStringMapper
    {
        #region Export
        public static GeoJSON.Net.Geometry.MultiLineString MapMultiLineString(List<LineString> lineStrings)
        {
            return new GeoJSON.Net.Geometry.MultiLineString(lineStrings);
        }
        #endregion
    }
}