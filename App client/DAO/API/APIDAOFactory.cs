using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace DAO.API
{
    public class APIDAOFactory : IDAOFactory
    {
        public APIDAOFactory(Uri baseUri)
        {
            Client = new HttpClient { BaseAddress = baseUri ?? new Uri("http://localhost/") };
            AnneeUnivDAO = new APIAnneeUnivDAO(Client);
            CategorieDAO = new APICategorieDAO(Client);
            ComposanteDAO = new APIComposanteDAO(Client);
            DiplomeDAO = new APIDiplomeDAO(Client);
            EcDAO = new APIEcDAO(Client);
            EnseignantDAO = new APIEnseignantDAO(Client);
            EnseignementDAO = new APIEnseignementDAO(Client);
            EtapeDAO = new APIEtapeDAO(Client);
            HorsCompDAO = new APIHorsCompDAO(Client);
            SemestreDAO = new APISemestreDAO(Client);
            ServiceDAO = new APIServiceDAO(Client);
            UeDAO = new APIUeDAO(Client);
        }

        public IAnneeUnivDAO AnneeUnivDAO { get; }
        public ICategorieDAO CategorieDAO { get; }
        public IComposanteDAO ComposanteDAO { get; }
        public IDiplomeDAO DiplomeDAO { get; }
        public IEcDAO EcDAO { get; }
        public IEnseignantDAO EnseignantDAO { get; }
        public IEnseignementDAO EnseignementDAO { get; }
        public IEtapeDAO EtapeDAO { get; }
        public IHorsCompDAO HorsCompDAO { get; }
        public ISemestreDAO SemestreDAO { get; }
        public IServiceDAO ServiceDAO { get; }
        public IUeDAO UeDAO { get; }

        private HttpClient Client { get; }
    }
}