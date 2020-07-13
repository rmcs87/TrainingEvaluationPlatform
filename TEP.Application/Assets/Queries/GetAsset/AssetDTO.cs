﻿using AutoMapper;
using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Queries.GetAsset
{
    public class AssetDTO : IMapFrom<Asset>
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
    }
}