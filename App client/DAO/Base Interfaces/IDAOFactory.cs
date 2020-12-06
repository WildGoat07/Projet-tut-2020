using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IDAOFactory
    {
        IAnneeUnivDAO AnneeUnivDAO { get; }
        ICategorieDAO CategorieDAO { get; }
        IComposanteDAO ComposanteDAO { get; }
        IDiplomeDAO DiplomeDAO { get; }
        IEcDAO EcDAO { get; }
        IEnseignantDAO EnseignantDAO { get; }
        IEtapeDAO EtapeDAO { get; }
        IHorsCompDAO HorsCompDAO { get; }
        ISemestreDAO SemestreDAO { get; }
        IServiceDAO ServiceDAO { get; }
        IUeDAO UeDAO { get; }
    }
}