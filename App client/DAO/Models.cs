using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public record AnneeUniv
    (
        string annee
    );
    public record Categorie
    (
        int no_cat,
        string categorie
    );
    public record Composante
    (
        string id_comp,
        string nom_comp,
        string? lieu_comp = null
    );
    public record CompCourante
    (
        string id_comp
    );
    public record Diplome
    (
        string code_diplome,
        string libelle_diplome,
        int vdi,
        string libelle_vdi,
        int? annee_deb = null,
        int? annee_fin = null
    );
    public record Ec
    (
        string code_ec,
        string libelle_ec,
        char? nature = 'E',
        int? HCM = 0,
        int? HEI = 0,
        int? HTD = 0,
        int? HTP = 0,
        int? HTPL = 0,
        int? HPRJ = 0,
        int? NbEpr = 1,
        int? CNU = 2700,
        int? no_cat = null,
        string? code_ec_pere = null,
        string? code_ue = null
    );
    public record Enseignant
    (
        string id_ens,
        string nom,
        string prenom,
        string? fonction = null,
        float? HOblig = null,
        float? HMax = null,
        char? CRCT = 'N',
        char? PES_PEDR = 'N',
        string? id_comp = null
    );
    public record Enseignement
    (
        string code_ec,
        string annee,
        int? eff_prev = null,
        int? eff_reel = null,
        int? GpCM = 1,
        int? GpEI = 0,
        int? GpTD = 0,
        int? GpTP = 0,
        int? GpTPL = 0,
        int? GpPRJ = 0,
        int? GpCMSer = 0,
        int? GpEISer = 0,
        int? GpTDSer = 0,
        int? GpTPSer = 0,
        int? GpTPLSer = 0,
        int? GpPRJSer = 0
    );
    public record Etape
    (
        string code_etape,
        int vet,
        string libelle_vet,
        string id_comp,
        string? code_diplome = null,
        int? vdi = null
    );
    public record HorsComp
    (
        string id_ens,
        string id_comp,
        string annee,
        int? HCM = 0,
        int? HEI = 0,
        int? HTD = 0,
        int? HTP = 0,
        int? HTPL = 0,
        int? HPRJ = 0,
        float? HEqTD = 0f
    );
    public record Semestre
    (
        string code_sem,
        string libelle_sem,
        int? no_sem = null,
        string? code_etape = null,
        int? vet = null
    );
    public record Service
    (
        string id_ens,
        string code_ec,
        string annee,
        int? NbGpCM = 0,
        int? NbGpEI = 0,
        int? NbGpTD = 0,
        int? NbGpTP = 0,
        int? NbGpTPL = 0,
        int? NbGpPRJ = 0,
        float? HEqTD = 0f
    );
    public record Ue
    (
        string code_ue,
        string libelle_ue,
        char? nature = 'U',
        int? ECTS = 2700,
        string? code_ue_pere = null,
        string? code_sem = null
    );
}