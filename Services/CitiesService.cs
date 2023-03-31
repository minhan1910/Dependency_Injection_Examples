using ServiceContracts;

namespace Services
{
    public class CitiesService : ICitiesService, IDisposable
    {
        private List<string> _cities;

        private Guid _serviceInstanceId;

        public CitiesService() 
        {
            _serviceInstanceId = Guid.NewGuid();    
            this._cities = new()
            {
                "London",
                "Paris",
                "New York",
                "Tokyo",
                "Rome",
            };

            // TO DO: Add logic to open the DB connection
        }

        public Guid ServiceInstanceId
        {
            get
            {
                return _serviceInstanceId;
            }
        }

        public List<string> GetCities()
        {
            return this._cities;
        }

        public void Dispose()
        {
            // TO DO: add logic to close DB connection
        }
    }
}