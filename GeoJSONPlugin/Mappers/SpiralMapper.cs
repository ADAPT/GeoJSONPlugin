﻿using AgGateway.ADAPT.ApplicationDataModel.ADM;
using AgGateway.ADAPT.ApplicationDataModel.Guidance;
using GeoJSON.Net.Feature;
using System.Collections.Generic;
using GeoJSONPlugin.Mappers.GeoJson;

namespace GeoJSONPlugin.Mappers
{
    internal class SpiralMapper
    {
        private PluginProperties _properties;
        private ApplicationDataModel _dataModel;
        private Dictionary<string, object> _featProps;

        public SpiralMapper(PluginProperties properties, ApplicationDataModel dataModel, Dictionary<string, object> featProps)
        {
            this._properties = properties;
            this._dataModel = dataModel;
            this._featProps = featProps;
        }

        public Feature MapAsSingleFeature(Spiral guidancePatternAdapt)
        {
            GeoJSON.Net.Geometry.LineString lineString = LineStringMapper.MapLineString(guidancePatternAdapt.Shape, _properties.AffineTransformation);
            return new Feature(lineString, _featProps);
        }
    }
}