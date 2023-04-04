using NHibernate.Engine;
using System.Drawing;
using System;

namespace KappaApi.Enums
{
    public class StripeConstants
    {
         public static Dictionary<string, int> StripeInvoiceStatuses = new Dictionary<string, int>()
         {
             {"draft", 0},//The starting status for all invoices. You can still edit the invoice at this point.
            {"open", 1},//The invoice has been finalized, and is awaiting customer payment. You can no longer edit the invoice, but you can revise it.
             {"paid", 2},
             {"void", 3},//	This invoice was a mistake, and must be canceled.
            {"uncollectible", 4}//The customer is unlikely to pay this invoice (treat it as bad debt in your accounting process).
        };

    }
}
