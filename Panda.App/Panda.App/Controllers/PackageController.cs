using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models.Package;
using Panda.Data;
using Panda.Domein;

namespace Panda.App.Controllers
{
    public class PackageController : Controller
    {
        private readonly PandaDbContext context;

        public PackageController(PandaDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            this.ViewData["Recipients"] = this.context.Users.ToList();
            return this.View();
        }

        [HttpGet("/Package/Details/{id}")]
        public IActionResult Details(string id)
        {
            var package = context.Packages.Find(id);

            return this.View(new PackageDetailsViewModel()
            {
                Description = package.Description,
                EstimateDelivaryDate = package.EstimatedDeliveryDate.ToString(),
                Recipient = package.Recipient.UserName,
                ShippingAddress = package.ShippingAddress,
                Status = package.Status.Name
            });

            //todo Status of packges
            if (package.Status.Name=="Pending")
            {
               
            }

        }
        [HttpGet("/Packages/Skip/{id}")]

        public IActionResult Ship(string id)
        {
            var package = this.context.Packages.Find(id);
           // package.Status = package.Status.Name == "Shipped";

           package.EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(20, 40)); 

           this.context.Update(package);
           this.context.SaveChanges();

           return this.Redirect("Packages/Shipped");
        }


        [HttpPost]
        public IActionResult Create(PackageCreateBindingModel bindingModel)
        {
            var package = new Package()
            {
                Description = bindingModel.Description,
                Recipient = this.context.Users.SingleOrDefault(x => x.UserName == bindingModel.Recipient),
                ShippingAddress = bindingModel.ShippingAddress,
                Weight = bindingModel.Weight,
                Status = this.context.PackageStatuses.FirstOrDefault(status => status.Name == "Pending")

            };
            this.context.Packages.Add(package);
            this.context.SaveChanges();

            return Redirect("/Package/Pending");
        }

        public IActionResult Pending()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Pending")
                .ToList()
                .Select(package =>
                {
                    return new PackagePendingViewModel()
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight = package.Weight,
                        ShippingAddress = package.ShippingAddress,
                        Recipient = package.Recipient.UserName
                    };
                }).ToList());
        }

        public IActionResult Shipped()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Shipped")
                .ToList()
                .Select(package =>
                {
                    return new PackageShippedViewModel()
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight = package.Weight,
                        EstimatedDeliveryDay = package.EstimatedDeliveryDate.ToString(),
                        Recipient = package.Recipient.UserName
                    };
                }).ToList());
        }

        public IActionResult Deliverd()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Deliverd")
                .ToList()
                .Select(package =>
                {
                    return new PackageDeliverdViewModel()
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight = package.Weight,
                        ShippingAddress = package.ShippingAddress,
                        Recipient = package.Recipient.UserName
                    };
                }).ToList());
        }

    }
}