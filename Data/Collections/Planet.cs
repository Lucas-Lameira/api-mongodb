//class de representação da collection no banco
namespace apidotnet.Data.Collections
{
    public class Planet
    {   
        
        public Planet(string name, string galaxy, double size)
        {
            this.Name = name;
            this.Galaxy = galaxy;
            this.Size = size;
        }

        private string Name { get; set;};
        private string Galaxy { get; set;};
        private double Size { get; set;};
    }
}