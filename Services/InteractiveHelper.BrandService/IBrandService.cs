﻿using InteractiveHelper.BrandService.Models;

namespace InteractiveHelper.BrandService;

public interface IBrandService
{
    Task<IEnumerable<BrandModel>> GetBrands();
    Task<IEnumerable<ItemModel>> GetBrandsItems(int brandId, int offset = 0, int limit = 10);
    Task<BrandModel> AddBrand(AddBrandModel model);
    Task<BrandModel> UpdateBrand(int BrandId, UpdateBrandModel model);
    Task DeleteBrand(int BrandId);
}
