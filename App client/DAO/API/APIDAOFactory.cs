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
        public IAnneeUnivDAO AnneeUnivDAO => throw new NotImplementedException();
        public ICategorieDAO CategorieDAO => throw new NotImplementedException();
        public IComposanteDAO ComposanteDAO => throw new NotImplementedException();
        public IDiplomeDAO DiplomeDAO => throw new NotImplementedException();
        public IEcDAO EcDAO => throw new NotImplementedException();
        public IEnseignantDAO EnseignantDAO => throw new NotImplementedException();
        public IEnseignementDAO EnseignementDAO => throw new NotImplementedException();
        public IEtapeDAO EtapeDAO => throw new NotImplementedException();
        public IHorsCompDAO HorsCompDAO => throw new NotImplementedException();
        public ISemestreDAO SemestreDAO => throw new NotImplementedException();
        public IServiceDAO ServiceDAO => throw new NotImplementedException();
        public IUeDAO UeDAO => throw new NotImplementedException();

        //! ne pas commit cette ligne, l'adapter à chacun
        internal static HttpClient Client { get; } = new HttpClient { BaseAddress = new Uri("http://localhost") };
    }
}