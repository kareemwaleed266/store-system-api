﻿namespace Store.Repository.BasketRepository.Models
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public string? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
