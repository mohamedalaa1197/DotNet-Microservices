﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public IndexModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogService.GetCatalog();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogService.GetCatalog(productId);

            var basket = await _basketService.GetBasket("swn");

            if (basket != null)
            {
                if (basket.Items is null)
                    basket.Items = new List<BasketItemModel>();

                basket.Items.Add(new BasketItemModel()
                {
                    ProductId = long.Parse(productId),
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    Color = "Black"
                });
            }

            var basketUpdate = await _basketService.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}