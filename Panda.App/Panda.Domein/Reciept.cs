using System;

namespace Panda.Domein
{
    public class Reciept
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string RecipientId { get; set; }
        public PandaUser Recipient { get; set; }

        public string PackageId { get; set; }

        public Package Package { get; set; }
        /*•	Has an Id – a GUID String or an Integer.
           •	Has a Fee – a decimal number.
           •	Has an Issued On – a DateTime object.
           •	Has a Recipient – a User object.
           •	Has a Package – a Package object.
           */
    }
}