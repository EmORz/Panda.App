using System;
using Panda.Domein;

namespace Panda.App.Models.Package
{
    public class PackageCreateBindingModel
    {
        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public string Recipient { get; set; }
    }
}