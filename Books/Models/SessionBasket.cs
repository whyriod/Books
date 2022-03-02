using Books.Infastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Books.Models
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();
            basket.Session = session;

            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book b, int q)
        {
            base.AddItem(b, q);
            Session.SetJson("Basket", this);
        }

        public override void RemoveItem(Book b)
        {
            base.RemoveItem(b);
            Session.SetJson("Basket", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }
    }
}
