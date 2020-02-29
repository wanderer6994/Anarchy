﻿using System.Collections.Generic;

namespace Discord
{
    public static class BillingExtensions
    {
        public static IReadOnlyList<Payment> GetPayments(this DiscordClient client, int limit = 100)
        {
            return client.HttpClient.Get("/users/@me/billing/payments?limit=" + limit)
                                .Deserialize<IReadOnlyList<Payment>>();
        }


        public static IReadOnlyList<PaymentMethod> GetPaymentMethods(this DiscordClient client)
        {
            return client.HttpClient.Get("/users/@me/billing/payment-sources")
                                .Deserialize<IReadOnlyList<PaymentMethod>>();
        }
    }
}
