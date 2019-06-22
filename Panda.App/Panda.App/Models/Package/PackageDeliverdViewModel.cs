using System;
using Panda.Domein;

namespace Panda.App.Models.Package
{
    public class PackageDeliverdViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }


        public string Recipient { get; set; }
    }
}