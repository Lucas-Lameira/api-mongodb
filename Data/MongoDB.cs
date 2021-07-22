using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
namespace apidotnet.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["databasename"]); //appsettings
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("It was not possible to connect to MongoDB", ex);
            }
        }

        private void MapClasses()
        {   
            //conven��o para usar camelCase - coment�rio de estudo so be cool XD
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
               
            
            //Se n�o huover nenhuma class mapeada do tipo 
            if (!BsonClassMap.IsClassMapRegistered(typeof(Planet)))
            {
                BsonClassMap.RegisterClassMap<Planet>(i =>
                {
                    i.AutoMap();//todas as propriedades podem ter o mesmo tipo e nome no banco
                    
                    i.SetIgnoreExtraElements(true);//
                });
            }
        }
    }
}