
namespace WebSocket_API.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ExchangeDataSeeder exchangeDataSeeder;
        private readonly GicsDataSeeder gicsDataSeeder;
        private readonly InstrumentDataSeeder instrumentDataSeeder;
        private readonly KindDataSeeder kindDataSeeder;
        private readonly ProviderDataSeeder providerDataSeeder;

        public DataSeeder(ExchangeDataSeeder exchangeDataSeeder, 
            GicsDataSeeder gicsDataSeeder, InstrumentDataSeeder instrumentDataSeeder, 
            KindDataSeeder kindDataSeeder, ProviderDataSeeder providerDataSeeder)
        {
            this.exchangeDataSeeder = exchangeDataSeeder;
            this.gicsDataSeeder = gicsDataSeeder;
            this.instrumentDataSeeder = instrumentDataSeeder;
            this.kindDataSeeder = kindDataSeeder;
            this.providerDataSeeder = providerDataSeeder;
        }

        public async Task Seed()
        {
            await kindDataSeeder.Seed();
            await providerDataSeeder.Seed();
            await exchangeDataSeeder.Seed();
            await gicsDataSeeder.Seed();
            await instrumentDataSeeder.Seed();
        }
    }
}
