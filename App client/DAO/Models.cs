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
        int Version,
        string LibelleVersion,
        int? AnneeDebut = null,
        int? AnneeFin = null
    );
    public record Ec
    (
        string Code,
        string Libelle,
        char? Nature = 'E',
        int? HeuresCM = 0,
        int? HeuresEI = 0,
        int? HeuresTD = 0,
        int? HeuresTP = 0,
        int? HeuresTPL = 0,
        int? HeuresPRJ = 0,
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
        float? HeuresObligees = null,
        float? HeuresMax = null,
        char? CRCT = 'N',
        char? PesPedr = 'N',
        string? Composante = null
    );
    public record Enseignement
    (
        string CodeEc,
        string Annee,
        int? EffectifPrevu = null,
        int? EffectifReel = null,
        int? GroupesCM = 1,
        int? GroupesEI = 0,
        int? GroupesTD = 0,
        int? GroupesTP = 0,
        int? GroupesTPL = 0,
        int? GroupesPRJ = 0,
        int? GroupesCMServis = 0,
        int? GroupesEIServis = 0,
        int? GroupesTDServis = 0,
        int? GroupesTPServis = 0,
        int? GroupesTPLServis = 0,
        int? GroupesPRJServis = 0
    );
    public record Etape
    (
        string Code,
        int VersionEtape,
        string Libelle,
        string Composante,
        string? Diplome = null,
        int? VersionDiplome = null
    );
    public record HorsComp
    (
        string Enseignement,
        string Composante,
        string Annee,
        int? HeuresCM = 0,
        int? HeuresEI = 0,
        int? HeuresTD = 0,
        int? HeuresTP = 0,
        int? HeuresTPL = 0,
        int? HeuresPRJ = 0,
        float? HeuresEquivalentTD = 0f
    );
    public record Semestre
    (
        string Code,
        string Libelle,
        int? Numero = null,
        string? Etape = null,
        int? VersionEtape = null
    );
    public record Service
    (
        string Enseignant,
        string Ec,
        string Annee,
        int? NombreGroupesCM = 0,
        int? NombreGroupesEI = 0,
        int? NombreGroupesTD = 0,
        int? NombreGroupesTP = 0,
        int? NombreGroupesTPL = 0,
        int? NombreGroupesPRJ = 0,
        float? HeuresEquivalentTD = 0f
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