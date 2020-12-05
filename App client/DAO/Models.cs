using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public record AnneeUniv
    (
        string Annee
    );
    public record Categorie
    (
        int Numero,
        string Libelle
    );
    public record Composante
    (
        string Id,
        string Nom,
        string? Lieu = null
    );
    public record Diplome
    (
        string Code,
        string LibelleDiplome,
        int Vdi,
        string LibelleVdi,
        int? AnneeDebut = null,
        int? AnneeFin = null
    );
    public record Ec
    (
        string Code,
        string Libelle,
        char? Nature = 'E',
        int? HCM = 0,
        int? HEI = 0,
        int? HTD = 0,
        int? HTP = 0,
        int? HTPL = 0,
        int? HPRJ = 0,
        int? NombreEpreuves = 1,
        int? CNU = 2700,
        int? NumCategorie = null,
        int? Pere = null,
        int? Ue = null
    );
    public record Enseignant
    (
        string Id,
        string Nom,
        string Prenom,
        string? Fonction = null,
        float? HOblig = null,
        float? HMax = null,
        char? CRCT = 'N',
        char? PesPedr = 'N',
        string? Composante = null
    );
    public record Enseignement
    (
        string CodeEc,
        string Annee,
        int? EffPrev = null,
        int? EffReel = null,
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
        string Code,
        int Vet,
        string Libelle,
        string Composante,
        string? Diplome = null,
        int? Vdi = null
    );
    public record HorsComp
    (
        string Enseignement,
        string Composante,
        string Annee,
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
        string Code,
        string Libelle,
        int? Numero = null,
        string? Etape = null,
        int? Vet = null
    );
    public record Service
    (
        string Enseignant,
        string Ec,
        string Annee,
        int? NbGpCM = 0,
        int? NbGpEI = 0,
        int? NBGpTD = 0,
        int? NbGpTP = 0,
        int? NbGpTPL = 0,
        int? NBGpPRJ = 0,
        float? HEqTD = 0f
    );
    public record Ue
    (
        string Code,
        string Libelle,
        char? Nature = 'U',
        int? ECTS = 2700,
        string? Pere = null,
        string? Semestre = null
    );
}