-- phpMyAdmin SQL Dump
-- version 4.5.4.1
-- http://www.phpmyadmin.net
--
-- Client :  localhost
-- Généré le :  Jeu 17 Décembre 2020 à 17:30
-- Version du serveur :  5.7.11
-- Version de PHP :  7.0.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `projet_tut`
--

DELIMITER $$
--
-- Fonctions
--
CREATE DEFINER=`root`@`localhost` FUNCTION `compareStrings` (`cle` VARCHAR(100), `valeur` VARCHAR(100)) RETURNS TINYINT(1) BEGIN
	DECLARE nCle varchar(100);
    DECLARE i INTEGER(3);
    DECLARE minSize INTEGER(3);
    DECLARE seuil INTEGER(3); 
    
	SET cle = projet_tut.RemoveExtraChars(cle);
    IF length(cle) = 0 THEN 
		RETURN TRUE;
	END IF;
    
    IF valeur is null THEN 
		RETURN FALSE;
	END IF;
	SET valeur = projet_tut.RemoveExtraChars(valeur);
    IF length(valeur) = 0 THEN 
		RETURN FALSE;
	END IF;
    
    SET seuil = CASE
    WHEN length(cle) div 5<5 THEN length(cle) div 5
    ELSE 5
    END;
    
	SET nCle = '';
    SET minSize = CASE 
    WHEN length(cle) < length(valeur) THEN length(cle)
    ELSE 
		length(valeur)
	END;
    
    SET i = 1;
    WHILE i <= length(cle) DO
		IF i > 1 THEN 
			SET nCle = concat(nCle, '.*');
		end if;
        SET nCle = concat(nCle, SubString(cle, i, 1) );
        SET i = i + 1;
    END WHILE;
    
    IF valeur regexp nCle THEN
		RETURN TRUE;
	ELSE 
		SET i = 1;
        WHILE i <= length(valeur)-minSize+1 DO 
			IF projet_tut.Levenshtein(cle, substring(valeur, i, minSize)) <= seuil THEN
				RETURN TRUE;
			END IF;
            SET i = i+1;
        END WHILE;
        RETURN FALSE;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `Levenshtein` (`chaine1` VARCHAR(100), `chaine2` VARCHAR(100)) RETURNS INT(3) BEGIN
    DECLARE liste text;
    DECLARE i,j integer(3);
    DECLARE lC1 integer(3);
    DECLARE lC2 integer(3);
    DECLARE cout integer(3);
    DECLARE minVal, a, b, c integer(3);
    
    if chaine1 = chaine2 then 
        return 0;
    end if;
    
    SET i = 1;
    SET j = 1;
    SET lC1 = length(chaine1);
    SET lC2 = length(chaine2);
    SET liste = '';
    
    while i <= lC1+1 do
		SET j=1;
        while j <= lC2+1 do
            set liste=concat(liste, char(0));
            set j = j + 1;
        end while;
        set i = i + 1;
    end while;
    
    set i = 1;
    set j = 1;
    
    while i <= lC1+1 do
        SET liste = insert(liste, i, 1, char(i-1));         
        SET i = i + 1;
    end while;
    
    while j <= lC1+1 do
        SET liste = insert(liste, (j-1)*(lC2+1)+1 , 1, char(j-1)); 
        SET j = j + 1;
    end while;
    
    set i = 1;
    set j = 1;
    
    while i <= lC1 do
		SET j=1;
        while j <= lC2 do
            SET cout = CASE
            WHEN ascii(substring(chaine1, i, 1)) = ascii(substring(chaine2, j, 1)) THEN 0
            ELSE 
                1
            END;
            
            SET a = ascii(substring(liste, (i-1)*(lC2+1)+j+1, 1)) + 1;
            SET b = ascii(substring(liste, i*(lC2+1)+j, 1)) + 1;
            SET minVal = CASE
                WHEN a < b THEN a
                ELSE b
            END;
            
            SET c = ascii(substring(liste, (i-1)*(lC2+1)+j, 1)) + cout;
            SET minVal = CASE
                WHEN minVal  < c THEN minVal 
                ELSE c
            END;
            
            SET liste = INSERT(liste, i*(lC2+1)+j+1, 1, char(minVal));
            
            set j = j + 1;
        end while;
        set i = i + 1;
    end while;
    
    
RETURN ascii(substring(liste, (lC1+1)*(lC2+1), 1));
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `RemoveExtraChars` (`p_OriginalString` VARCHAR(100)) RETURNS VARCHAR(100) CHARSET utf8mb4 BEGIN
		DECLARE i integer(3);
        DECLARE OriginalString varchar(100);
        DECLARE ModifiedString varchar(100);
        DECLARE caractere varchar(1);
        
		SET i = 1;
        SET OriginalString = lower(p_OriginalString);
        SET ModifiedString  = '';
   
		WHILE i <= length(OriginalString) DO
			SET caractere = SubString(OriginalString, i, 1);
            SET caractere = CASE
            WHEN caractere="â" THEN "a"
            WHEN caractere="ä" THEN "a"
            WHEN caractere="à" THEN "a"
            WHEN caractere="ë" THEN "e"
            WHEN caractere="ê" THEN "e"
            WHEN caractere="é" THEN "e"
            WHEN caractere="è" THEN "e"
            WHEN caractere="ï" THEN "i"
            WHEN caractere="î" THEN "i"
            WHEN caractere="ì" THEN "i"
            WHEN caractere="ö" THEN "o"
			WHEN caractere="ô" THEN "o"
            WHEN caractere="ü" THEN "u"
            WHEN caractere="û" THEN "u"
            WHEN caractere="ù" THEN "u"
            WHEN caractere="ÿ" THEN "y"
            WHEN caractere="ç" THEN "c"
            WHEN caractere="œ" THEN "oe"
            ELSE 
				caractere
            END;
			if caractere regexp '[a-z0-9]'
            then 
				set ModifiedString = concat(ModifiedString, caractere);
            end if; 
            set i = i + 1;
        END WHILE;
        
        return ModifiedString;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `annee_univ`
--

CREATE TABLE `annee_univ` (
  `annee` varchar(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `annee_univ`
--

INSERT INTO `annee_univ` (`annee`) VALUES
('2019/2020'),
('2020/2021');

-- --------------------------------------------------------

--
-- Structure de la table `categories`
--

CREATE TABLE `categories` (
  `no_cat` int(2) NOT NULL,
  `categorie` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `categories`
--

INSERT INTO `categories` (`no_cat`, `categorie`) VALUES
(0, 'Divers'),
(1, 'Mathématiques et outils mathématiques pour l\'infor'),
(2, 'Algorithmique, Méthodologie et Programmation impér'),
(3, 'Méthodologie, Conception et Programmation Objet'),
(4, 'Techniques Informatiques'),
(5, 'Bases de Données'),
(6, 'Découvertes Informatique Avancée'),
(7, 'Découvertes autres disciplines'),
(8, 'Langue'),
(9, 'Projets/Stage'),
(10, 'Remise à niveau ou Parcours ECS'),
(11, 'Développement Web et Mobile'),
(12, 'Système et Réseaux'),
(13, 'Matières transverses');

-- --------------------------------------------------------

--
-- Structure de la table `composante`
--

CREATE TABLE `composante` (
  `id_comp` varchar(3) NOT NULL,
  `nom_comp` varchar(30) NOT NULL,
  `lieu_comp` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `composante`
--

INSERT INTO `composante` (`id_comp`, `nom_comp`, `lieu_comp`) VALUES
('FB0', 'UFR MIM', 'Metz Technopôle'),
('HE0', 'IUT Metz', 'Metz Saulcy');

-- --------------------------------------------------------

--
-- Structure de la table `comp_courante`
--

CREATE TABLE `comp_courante` (
  `id_comp` varchar(3) CHARACTER SET utf8mb4 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `comp_courante`
--

INSERT INTO `comp_courante` (`id_comp`) VALUES
('HE0');

-- --------------------------------------------------------

--
-- Structure de la table `diplome`
--

CREATE TABLE `diplome` (
  `code_diplome` varchar(10) NOT NULL,
  `libelle_diplome` varchar(100) NOT NULL,
  `vdi` int(3) NOT NULL,
  `libelle_vdi` varchar(50) NOT NULL,
  `annee_deb` int(4) DEFAULT NULL,
  `annee_fin` int(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `diplome`
--

INSERT INTO `diplome` (`code_diplome`, `libelle_diplome`, `vdi`, `libelle_vdi`, `annee_deb`, `annee_fin`) VALUES
('3WLFINF', 'S&T_Licence Informatique', 200, 'L2-Informatique', NULL, NULL),
('3WLFINF', 'S&T_Licence Informatique', 300, 'L3-Informatique', NULL, NULL),
('3WLFPW1', 'Portail Mathématiques Informatique', 300, 'Portail Mathématiques Informatique', NULL, NULL),
('5WMFINF', 'S&T_Master Informatique', 400, 'M1-Informatique', NULL, NULL),
('5WMFINF', 'S&T_Master Informatique', 502, 'M2-Informatique PT Décisionnelle', NULL, NULL),
('5WMFINF', 'S&T_Master Informatique', 503, 'M2-Informatique PT G2IHM', NULL, NULL),
('5WMFINF', 'S&T_Master Informatique', 504, 'M2-Informatique PT SIS', NULL, NULL);

-- --------------------------------------------------------

--
-- Structure de la table `ec`
--

CREATE TABLE `ec` (
  `code_ec` varchar(10) NOT NULL,
  `libelle_ec` varchar(100) NOT NULL,
  `nature` char(1) DEFAULT 'E',
  `HCM` int(3) DEFAULT '0',
  `HEI` int(3) DEFAULT '0',
  `HTD` int(3) DEFAULT '0',
  `HTP` int(3) DEFAULT '0',
  `HTPL` int(3) DEFAULT '0',
  `HPRJ` int(3) DEFAULT '0',
  `NbEpr` int(1) DEFAULT '1',
  `CNU` int(4) DEFAULT '2700',
  `no_cat` int(2) DEFAULT NULL,
  `code_ec_pere` varchar(10) DEFAULT NULL,
  `code_ue` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `ec`
--

INSERT INTO `ec` (`code_ec`, `libelle_ec`, `nature`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `NbEpr`, `CNU`, `no_cat`, `code_ec_pere`, `code_ue`) VALUES
('0WEHMM03', 'Découverte de l\'IHM dans des domaines d\'application', 'E', 14, NULL, 14, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('0WEHMM04', 'EC Compléments Génie Logiciel', 'E', 14, NULL, 14, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('0WEIMM01', 'Applications de l\'informatique décisionnelle', 'E', 14, NULL, 14, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('1WCK7M04', 'Choix de Langues (1/2)', 'C', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 8, NULL, '1WUK7M12'),
('1WEK7M03', 'Systèmes logiques et numériques', 'E', NULL, 15, NULL, NULL, NULL, NULL, 1, 6100, 7, NULL, '1WUK7M09'),
('1WEK7M05', 'Anglais', 'E', NULL, NULL, NULL, NULL, 20, NULL, 1, 1100, 8, '1WCK7M04', NULL),
('1WEK7M06', 'Outils & Cultures NUMériques', 'E', 2, NULL, NULL, 18, NULL, NULL, 1, 2700, 1, NULL, '1WUK7M12'),
('1WEK7M07', 'Méthodologie du Travail Universitaire', 'E', NULL, NULL, 10, NULL, NULL, NULL, 1, 0, 0, NULL, '1WUK7M12'),
('1WEK7M08', 'Calculs et mathématiques', 'E', NULL, 60, NULL, NULL, NULL, NULL, 1, 2600, 1, NULL, '1WUK7M01'),
('1WEK7M09', 'Algorithmique et Programmation 1', 'E', NULL, 44, NULL, 16, NULL, NULL, 1, 2700, 2, NULL, '1WUK7M02'),
('1WEK7M10', 'Fondements mathématiques', 'E', NULL, 30, NULL, NULL, NULL, NULL, 1, 2500, 1, NULL, '1WUK7M03'),
('1WEK7M11', 'Introduction au Web', 'E', NULL, 8, NULL, 22, NULL, NULL, 1, 2700, 11, NULL, '1WUK7M04'),
('1WEK7M12', 'Nombres complexes et géométrie', 'E', NULL, 30, NULL, NULL, NULL, NULL, 1, 2600, 1, NULL, '1WUK7M06'),
('1WEK7M13', 'Codage numérique : du nombre au pixel', 'E', NULL, 24, NULL, 6, NULL, NULL, 1, 2700, 4, NULL, '1WUK7M07'),
('1WEK7M14', 'Culture Scientifique', 'E', NULL, 30, NULL, NULL, NULL, NULL, 1, 2700, 7, NULL, '1WUK7M08'),
('1WEK7M15', 'Mécanique du point', 'E', NULL, 30, NULL, NULL, NULL, NULL, 1, 6000, 7, NULL, '1WUK7M10'),
('1WEK7M16', 'Allemand', 'E', NULL, NULL, NULL, NULL, 20, NULL, 1, 1200, 8, '1WCK7M04', '2WUK7M10'),
('1WEL2M01', 'Principes de macroéconomie (1)', 'E', 30, NULL, 15, NULL, NULL, NULL, 1, 500, 7, NULL, '1WUK7M11'),
('2WCK7M05', 'Choix de langue (1/2)', 'C', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 8, NULL, '2WUK7M04'),
('2WEK7M03', 'Outils & Cultures NUMériques', 'E', 2, NULL, NULL, 18, NULL, NULL, 1, 2700, 7, NULL, '2WUK7M04'),
('2WEK7M04', 'Anglais', 'E', NULL, NULL, NULL, NULL, 20, NULL, 1, 1100, 8, '2WCK7M05', NULL),
('2WEK7M05', 'Projet professionnel personnel', 'E', NULL, NULL, 10, NULL, NULL, NULL, 1, 0, 0, NULL, '2WUK7M04'),
('2WEK7M06', 'Méthodologie Niveau 1', 'E', NULL, 20, NULL, 10, NULL, NULL, 1, 2700, 2, NULL, '2WUK7M14'),
('2WEK7M07', 'Méthodologie Niveau 2', 'E', NULL, 8, NULL, 22, NULL, NULL, 1, 2700, 3, NULL, '2WUK7M15'),
('2WEK7M08', 'EC1 Intro aux systèmes dynamiques et simulation numérique', 'E', NULL, 21, NULL, 9, NULL, NULL, 1, 6100, 7, NULL, '2WUK7M06'),
('2WEK7M09', 'EC2 Phénomènes électromagnétiques', 'E', NULL, 21, NULL, 9, NULL, NULL, 1, 6300, 7, NULL, '2WUK7M06'),
('2WEK7M10', 'Introduction a la statistique exploratoire', 'E', NULL, 20, NULL, NULL, NULL, NULL, 1, 2600, 1, NULL, '2WUK7M10'),
('2WEK7M11', 'Algorithmique et Programmation 2', 'E', NULL, 40, NULL, 20, NULL, NULL, 1, 2700, 3, NULL, '2WUK7M01'),
('2WEK7M12', 'Outils Mathématiques', 'E', NULL, 60, NULL, NULL, NULL, NULL, 1, 2600, 1, NULL, '2WUK7M03'),
('2WEK7M13', 'Allemand', 'E', NULL, NULL, NULL, NULL, 20, NULL, 1, 1200, 8, '2WCK7M05', NULL),
('2WEK7M14', 'Algèbre linéaire 1', 'E', NULL, 60, NULL, NULL, NULL, NULL, 1, 2500, 1, NULL, '2WUK7M07'),
('2WEK7M15', 'Analyse 1', 'E', NULL, 60, NULL, NULL, NULL, NULL, 1, 2600, 1, NULL, '2WUK7M08'),
('2WEK7M16', 'Electromagnétisme', 'E', NULL, 60, NULL, NULL, NULL, NULL, 1, 6300, 1, NULL, '2WUK7M09'),
('2WEK7M17', 'Arithmétique', 'E', NULL, 26, NULL, 4, NULL, NULL, 1, 2500, 1, NULL, '2WUK7M11'),
('2WEK7M18', 'Compléments d\'analyse', 'E', NULL, 26, NULL, 4, NULL, NULL, 1, 2600, 1, NULL, '2WUK7M12'),
('2WEK7M19', 'Codage Numérique : du nombre au pixel', 'E', NULL, 24, NULL, 6, NULL, NULL, 1, 2700, 4, NULL, '2WUK7M13'),
('2WEK7M20', 'Introduction aux systèmes séquentiels', 'E', NULL, 21, NULL, 9, NULL, NULL, 1, 6100, 7, NULL, '2WUK7M16'),
('2WEK7M21', 'Circuits en régime transitoire', 'E', NULL, 18, NULL, 12, NULL, NULL, 1, 6300, 7, NULL, '2WUK7M16'),
('2WEK7M22', 'Mécanique du solide', 'E', NULL, 44, NULL, 16, NULL, NULL, 1, 6000, 7, NULL, '2WUK7M17'),
('2WEL2M01', 'Principes de microéconomie (1)', 'E', 30, NULL, 15, NULL, NULL, NULL, 1, 500, 7, NULL, '2WUK7M10'),
('3WE31M01', 'Maths discrètes 1', 'E', 30, NULL, 30, NULL, NULL, NULL, 1, 2700, 1, NULL, '3WU31M01'),
('3WE31M02', 'Outils Système', 'E', 6, NULL, 4, 20, NULL, NULL, 1, 2700, 12, NULL, '3WU31M08'),
('3WE31M03', 'Réseau 1', 'E', 12, NULL, 8, 10, NULL, NULL, 1, 2700, 12, NULL, '3WU31M08'),
('3WE31M04', 'Algorithmique Programmation 3', 'E', 24, NULL, 24, 12, NULL, NULL, 1, 2700, 2, NULL, '3WU31M02'),
('3WE31M05', 'Programmation Avancée', 'E', 10, NULL, NULL, 20, NULL, NULL, 1, 2700, 2, NULL, '3WU31M03'),
('3WE31M06', 'Architecture des ordinateurs', 'E', 18, NULL, 15, 12, NULL, NULL, 1, 2700, 4, NULL, '3WU31M04'),
('3WE31M07', 'Remise à niveau informatique', 'E', NULL, NULL, 45, NULL, NULL, NULL, 1, 2700, 10, NULL, '3WU31M05'),
('3WE31M08', 'Langue 2', 'E', NULL, NULL, 45, NULL, NULL, NULL, 1, 1200, 8, NULL, '3WU31M06'),
('3WE31M09', 'Introduction aux Bases de Données', 'E', 10, NULL, 12, 8, NULL, NULL, 1, 2700, 5, NULL, '3WU31M07'),
('3WE31M10', 'Anglais', 'E', NULL, NULL, NULL, NULL, 30, NULL, 1, 1100, 8, NULL, '3WU31M09'),
('4WE31M01', 'Maths discrètes 2', 'E', 14, NULL, 16, NULL, NULL, NULL, 1, 2700, 1, NULL, '4WU31M01'),
('4WE31M02', 'Langages formels', 'E', 14, NULL, 16, NULL, NULL, NULL, 1, 2700, 1, NULL, '4WU31M01'),
('4WE31M03', 'Interfaces graphiques', 'E', 8, NULL, NULL, 22, NULL, NULL, 1, 2700, 4, NULL, '4WU31M02'),
('4WE31M04', 'Projet de synthèse', 'E', 4, NULL, 8, 18, NULL, NULL, 1, 2700, 9, NULL, '4WU31M02'),
('4WE31M05', 'Système 1', 'E', 12, NULL, 6, 12, NULL, NULL, 1, 2700, 12, NULL, '4WU31M03'),
('4WE31M06', 'Probabilités Statistiques', 'E', NULL, NULL, 45, NULL, NULL, NULL, 1, 2700, 1, NULL, '4WU31M04'),
('4WE31M07', 'Remise à niveau Mathématiques', 'E', NULL, NULL, 45, NULL, NULL, NULL, 1, 2700, 1, NULL, '4WU31M05'),
('4WE31M08', 'Langues 2', 'E', NULL, NULL, 45, NULL, NULL, NULL, 1, 1200, 8, NULL, '4WU31M06'),
('4WE31M09', 'Bases de la programmation objet', 'E', 16, NULL, 16, 28, NULL, NULL, 1, 2700, 3, NULL, '4WU31M07'),
('4WE31M10', 'Anglais', 'E', NULL, NULL, NULL, NULL, 30, NULL, 1, 1100, 8, NULL, '4WU31M09'),
('4WEULM03', 'Civilisation et culture des pays germanophones : mythes et .', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 1200, 7, NULL, '4WU31M08'),
('4WEULM04', 'Musique, arts et société', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 1800, 7, NULL, '4WU31M08'),
('4WEULM05', 'Ecriture inventive et création littéraire', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 900, 7, NULL, '4WU31M08'),
('4WEULM06', 'Les fictions contemporaines', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 1000, 7, NULL, '4WU31M08'),
('4WEULM07', 'Droit public pour les concours administratifs', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 200, 7, NULL, '3WU31M08'),
('4WEULM08', 'Problèmes économiques et sociaux contemporains', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 500, 7, NULL, '4WU31M08'),
('4WEULM09', 'Tutorat pour les cordées de la réussite', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '4WU31M08'),
('4WEULM10', 'Nutrition et santé', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '3WU31M08'),
('4WEULM12', 'Eléments de défense et de sécurité nationales', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 600, 7, NULL, '4WU31M08'),
('4WEULM13', 'Entreprendre, c\'est facile !', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 600, 7, NULL, '4WU31M08'),
('4WEULM14', 'Gestion du stress et de la performance', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 7400, 7, NULL, '4WU31M08'),
('4WEULM17', 'Psychologie', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 1600, 7, NULL, '4WU31M08'),
('4WEULM19', 'Sociologie générale', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 1900, 7, NULL, '4WU31M08'),
('4WEULM20', 'Théologie : Religions & cinéma', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 7600, 7, NULL, '3WU31M08'),
('4WEULM22', 'L\'Europe et l\'Union Européenne', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 2300, 7, NULL, '4WU31M08'),
('4WEULM23', 'Pratiques associatives et citoyennes et Handicap', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '4WU31M08'),
('4WEULM24', 'Pédagogie et didact des sciences/Métier d\'enseignant', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '4WU31M08'),
('4WEULM25', 'Mathématique et société', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 2500, 7, NULL, '4WU31M08'),
('4WEULM26', 'ABC de l\'astronomie', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 3400, 7, NULL, '4WU31M08'),
('4WEULM27', 'La Main à la pâte : animat° scient à l\'école primaire', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '4WU31M08'),
('4WEULM31', 'Education pour la santé autour des addictions', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 4600, 7, NULL, '4WU31M08'),
('4WEULM32', 'Les jeux vidéo, une industrie culturelle', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 7100, 7, NULL, '4WU31M08'),
('4WEULM33', 'Culture et patrimoine', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 2100, 7, NULL, '4WU31M08'),
('4WEULM34', 'Connaissance de soi et Gestion de sa vie physique', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '4WU31M08'),
('4WEULM35', 'Pratique de l\'argumentation orale', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 100, 7, NULL, '4WU31M08'),
('4WEULM36', 'Histoire de la coupe du monde de football', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 7400, 7, NULL, '4WU31M08'),
('4WEULM37', 'Réseau social, solidarités étudiantes et vieillissement', 'E', 27, NULL, NULL, NULL, NULL, NULL, 1, 1600, 7, NULL, '4WU31M08'),
('5WE31M01', 'Algorithmique', 'E', 25, NULL, 20, NULL, NULL, NULL, 1, 2700, 2, NULL, '5WU31M02'),
('5WE31M02', 'Conception Programmation Objet Avancée', 'E', 14, NULL, 16, 15, NULL, NULL, 1, 2700, 3, NULL, '5WU31M02'),
('5WE31M03', 'Conception et manipulation de bases de données', 'E', 8, NULL, 10, 12, NULL, NULL, 1, 2700, 5, NULL, '5WU31M03'),
('5WE31M04', 'Programmation web', 'E', 12, NULL, 6, 12, NULL, NULL, 1, 2700, 11, NULL, '5WU31M03'),
('5WE31M05', 'Système 2', 'E', 6, NULL, 12, 12, NULL, NULL, 1, 2700, 12, NULL, '5WU31M04'),
('5WE31M06', 'Compression de données', 'E', 14, NULL, 16, NULL, NULL, NULL, 1, 2700, 4, NULL, '5WU31M07'),
('5WE31M07', 'Robotique', 'E', 14, NULL, NULL, 16, NULL, NULL, 1, 2700, 7, NULL, '5WU31M08'),
('5WE31M08', 'Anglais', 'E', NULL, NULL, NULL, NULL, 20, NULL, 1, 1100, 8, NULL, '5WU31M09'),
('5WE31M09', 'Projet Personnel professionnel', 'E', NULL, NULL, 20, NULL, NULL, NULL, 1, 0, 13, NULL, '5WU31M09'),
('5WE31M10', 'Logique', 'E', 30, NULL, 30, NULL, NULL, NULL, 1, 2700, 1, NULL, '5WU31M01'),
('5WE31M11', 'Programmation fonctionnelle', 'E', 14, NULL, 16, NULL, NULL, NULL, 1, 2700, 2, NULL, '5WU31M05'),
('5WE31M12', 'XML', 'E', 10, NULL, 8, 12, NULL, NULL, 1, 2700, 4, NULL, '5WU31M06'),
('6WC31M02', 'CHOIX BDD3/CAPES/AD(1/3)', 'C', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, '6WU31M07'),
('6WE31M01', 'Sécurité', 'E', 10, NULL, 10, 10, NULL, NULL, 1, 2700, 6, NULL, '6WU31M01'),
('6WE31M02', 'Réseau 2', 'E', 12, NULL, 6, 12, NULL, NULL, 1, 2700, 12, NULL, '6WU31M01'),
('6WE31M03', 'Optimisation', 'E', 14, NULL, 16, NULL, NULL, NULL, 1, 2700, 6, NULL, '6WU31M02'),
('6WE31M04', 'Modélisation', 'E', 14, NULL, 16, NULL, NULL, NULL, 1, 2700, 6, NULL, '6WU31M02'),
('6WE31M05', 'Compilation', 'E', 14, NULL, 16, NULL, NULL, NULL, 1, 2700, 4, NULL, '6WU31M03'),
('6WE31M06', 'Premiers pas vers l\'ingénierie du logiciel', 'E', 12, NULL, 12, 6, NULL, NULL, 1, 2700, 9, NULL, '6WU31M04'),
('6WE31M07', 'Introduction à l\'Intelligence Artificielle', 'E', 10, NULL, 10, 10, NULL, NULL, 1, 2700, 6, NULL, '6WU31M05'),
('6WE31M08', 'Infographie', 'E', 14, NULL, NULL, 16, NULL, NULL, 1, 2700, 6, NULL, '6WU31M06'),
('6WE31M09', 'Développement d\'applications mobiles', 'E', 10, NULL, 8, 12, NULL, NULL, 1, 2700, 11, NULL, '6WU31M07'),
('6WE31M10', 'Système de gestion de Bases de données', 'E', 12, NULL, 8, 10, NULL, NULL, 1, 2700, 5, '6WC31M02', NULL),
('6WE31M11', 'Maths pour le CAPES', 'E', NULL, 30, NULL, NULL, NULL, NULL, 1, 2700, 1, '6WC31M02', NULL),
('6WE31M12', 'Analyse de données', 'E', 16, NULL, 14, NULL, NULL, NULL, 1, 2700, 6, '6WC31M02', NULL),
('6WE31M13', 'Préparation à l\'immersion professionnelle', 'E', NULL, NULL, 10, NULL, NULL, NULL, 1, 2700, 9, NULL, '6WU31M08'),
('6WT31M01', 'Stage', 'S', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 9, NULL, '6WU31M08'),
('7WE31M01', 'Analyse et conception de logiciels', 'E', 22, NULL, 6, 16, NULL, NULL, 1, 2700, 6, NULL, '7WU31M01'),
('7WE31M02', 'Algorithmique et Complexité', 'E', 22, NULL, 22, NULL, NULL, NULL, 1, 2700, 2, NULL, '7WU31M02'),
('7WE31M03', 'Design patterns', 'E', 22, NULL, 10, 12, NULL, NULL, 1, 2700, 6, NULL, '7WU31M03'),
('7WE31M04', 'Logique et modèles de calcul', 'E', 22, NULL, 22, NULL, NULL, NULL, 1, 2700, 6, NULL, '7WU31M04'),
('7WE31M05', 'Optimisation combinatoire', 'E', 22, NULL, 14, 8, NULL, NULL, 1, 2700, 6, NULL, '7WU31M05'),
('7WE31M06', 'Réseaux', 'E', 22, NULL, 8, 14, NULL, NULL, 1, 2700, 12, NULL, '7WU31M06'),
('7WE31M07', 'Anglais', 'E', NULL, NULL, NULL, NULL, 24, NULL, 1, 1100, 8, NULL, '7WU31M07'),
('8WE31M01', 'Intelligence Artificielle', 'E', 12, NULL, 6, 6, NULL, NULL, 1, 2700, 6, NULL, '8WU31M01'),
('8WE31M02', 'Représentation des données visuelles', 'E', 12, NULL, NULL, 12, NULL, NULL, 1, 2700, 6, NULL, '8WU31M02'),
('8WE31M03', 'Anglais', 'E', NULL, NULL, NULL, NULL, 24, NULL, 1, 1100, 8, NULL, '8WU31M03'),
('8WE31M04', 'Techniques de communication et d\'expression', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 7100, 13, NULL, '8WU31M04'),
('8WE31M05', 'Outils d\'aide à la décision', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M05'),
('8WE31M06', 'Introduction à la  fouille de données', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M06'),
('8WE31M07', 'Initiation à l\'Ordonnancement', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M07'),
('8WE31M08', 'Métaheuristiques & Algorithmes de recherche stochastique', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M08'),
('8WE31M09', 'Introduction aux modèles financiers', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M09'),
('8WE31M10', 'Systèmes d\' Information Décisionnels', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M10'),
('8WE31M11', 'Graphes d\'attaques et Réseaux de transport', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M11'),
('8WE31M12', 'Ergonomie des systèmes interactifs expérience utilisateur', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 1600, 6, NULL, '8WU31M22'),
('8WE31M13', 'Psychologie cognitive et diversité et interactions', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 1600, 6, NULL, '8WU31M23'),
('8WE31M14', 'Initiation à la multimodalité', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M24'),
('8WE31M15', 'La plateforme . NET', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 3, NULL, '8WU31M26'),
('8WE31M16', 'Prototypage d’interfaces par langage de script', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M25'),
('8WE31M17', 'Fonctionnement d\'un moteur de rendu 3D', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M27'),
('8WE31M18', 'Administration d\'un système en réseau', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 12, NULL, '8WU31M18'),
('8WE31M19', 'Droit informatique', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 7, NULL, '8WU31M19'),
('8WE31M20', 'Ordonnancement temps-réel sous Linux', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 12, NULL, '8WU31M20'),
('8WE31M21', 'Méthodologie de la sécurité', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M21'),
('8WE31M22', 'Introduction à la cryptographie', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M16'),
('8WE31M23', 'Introduction à la sécurité des systèmes d\'information', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M17'),
('8WE31M24', 'Traitement d\'images et vision par ordinateur', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M12'),
('8WE31M25', 'Données semi-structurées et XML', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 4, NULL, '8WU31M13'),
('8WE31M26', 'Sémantique des langages de programmation', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '8WU31M14'),
('8WE31M27', 'Bases de données avancées', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 5, NULL, '8WU31M15'),
('8WP31M01', 'Initiation à la Recherche', 'P', NULL, NULL, NULL, NULL, NULL, 96, 1, NULL, 9, NULL, '8WU31M28'),
('8WT31M01', 'Projet en Entreprise (Stage en Alternance)', 'S', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 9, NULL, '8WU31M29'),
('9WECPM17', 'Management et Gestion de projet', 'E', 40, NULL, NULL, NULL, NULL, NULL, 1, 0, 13, NULL, '9WUCPM18'),
('9WECPM18', 'Création d\'entreprise', 'E', 40, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '9WUCPM19'),
('9WECPM19', 'Marketing', 'E', 40, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '9WUCPM20'),
('9WECPM20', 'Droit et fiscalité', 'E', 40, NULL, NULL, NULL, NULL, NULL, 1, 0, 7, NULL, '9WUCPM21'),
('9WEHMM01', '980 Conception Logicielle', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEHMM02', '912 Gestion de Projets', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 13, NULL, NULL),
('9WEHMM03', '980 Entrepôt de Données', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEHMM04', '982 Portails Web d\'Entreprises', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 11, NULL, NULL),
('9WEHMM05', '939 Sécurité des Réseaux', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 12, NULL, NULL),
('9WEHMM06', '932 Sécurité des Systèmes d\'Information', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEHMM07', 'Recherche d\'emploi et création d\' entreprise', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 7100, 7, NULL, NULL),
('9WEHMM08', 'Ergonomie et facteurs humains pour l\'accessibilté', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 1600, 6, NULL, NULL),
('9WEHMM09', 'Méthodes pour la conception centrée utilisateurs', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 1600, 6, NULL, '9WUHMM05'),
('9WEHMM10', 'Méthodes pour l\'évaluation centrée utilisateurs', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 1600, 6, NULL, '9WUHMM06'),
('9WEHMM11', 'Techniques de Visualisation de données et analyse', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUHMM07'),
('9WEHMM12', 'Modèles pour concevoir des systèmes interactifs', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUHMM08'),
('9WEHMM13', 'Gestion de projet', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 13, NULL, NULL),
('9WEHMM14', 'Anglais', 'E', NULL, NULL, NULL, NULL, 24, NULL, 1, 1100, 8, NULL, NULL),
('9WEHMM15', 'Français Langue Etrangère', 'E', NULL, NULL, NULL, NULL, 24, NULL, 1, 900, 8, NULL, NULL),
('9WEHMM16', 'Techniques d\'interactions innovantes', 'E', 24, NULL, 24, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEHMM17', 'Développement mobile', 'E', 24, NULL, 24, NULL, NULL, NULL, 1, 2700, 11, NULL, NULL),
('9WEHMM18', 'Développement Web', 'E', 24, NULL, 24, NULL, NULL, NULL, 1, 2700, 11, NULL, NULL),
('9WEHMM19', 'Intégration méthodologique IHM', 'E', NULL, NULL, 36, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUHMM13'),
('9WEHMM20', 'ERp - Introduction au progiciel de gestion intégré', 'E', 24, NULL, 24, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEHMM21', 'Développement Mobile', 'E', 24, NULL, 24, NULL, NULL, NULL, 1, 2700, 11, NULL, NULL),
('9WEHMM22', 'Développement Web', 'E', 24, NULL, 24, NULL, NULL, NULL, 1, 2700, 11, NULL, NULL),
('9WEHMM29', 'Anglais', 'E', NULL, NULL, NULL, NULL, 24, NULL, 1, 1100, 8, NULL, NULL),
('9WEHMM30', 'Français Langue Etrangère', 'E', NULL, NULL, NULL, NULL, 24, NULL, 1, 900, 8, NULL, NULL),
('9WEHMM31', 'Création d\'entreprise (Alternance)', 'E', 6, NULL, 6, NULL, NULL, NULL, 1, 7100, 7, NULL, '9WUHMM51'),
('9WEIMM01', 'Intégration méthodologique OPAL', 'E', NULL, NULL, 36, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM01'),
('9WEIMM02', 'Gestion de projets', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 13, NULL, NULL),
('9WEIMM03', 'Professionnalisation OPAL', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 13, NULL, '9WUIMM03'),
('9WEIMM04', 'Résolution de modèles d\'optimisation de grande taille', 'E', 24, NULL, NULL, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM04'),
('9WEIMM05', 'Combinatoire et algorithmes d\'approximation', 'E', 24, NULL, NULL, NULL, NULL, NULL, 1, 2700, 2, NULL, '9WUIMM05'),
('9WEIMM06', 'Théorie des graphes', 'E', 24, NULL, NULL, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM06'),
('9WEIMM07', 'Optimisation globale', 'E', 24, NULL, NULL, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM07'),
('9WEIMM08', 'Théorie de la complexité et inapproximabilité', 'E', 24, NULL, NULL, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM08'),
('9WEIMM09', 'Intégration méthodologique SID', 'E', NULL, NULL, 36, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM09'),
('9WEIMM10', 'Gestion de projets', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 13, NULL, NULL),
('9WEIMM11', 'Professionnalisation SID', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM11'),
('9WEIMM12', 'Datawarehouse', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM13'),
('9WEIMM13', 'Sécurité des systèmes d\'information', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM14'),
('9WEIMM14', 'Conception d\'un système d\'information décisionnel', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM15'),
('9WEIMM15', 'Reporting&Dashboarding: création de tableaux de bord', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEIMM16', 'Portail web d\'entreprise 1', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 11, NULL, NULL),
('9WEIMM17', 'Aide à la décision multicritère', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM18'),
('9WEIMM18', 'Décision dans l\'incertain', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM19'),
('9WEIMM19', 'Décision pour les systèmes parallèles et distribués', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM20'),
('9WEIMM20', 'Problèmes de satisfaction de contraintes', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM21'),
('9WEIMM21', 'Modèles de satisfaisabilité - SAT', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEIMM22', 'Big Data', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 5, NULL, NULL),
('9WEIMM23', 'Fouille informatique des données', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM24'),
('9WEIMM24', 'Techniques de visualisation de données et analyse', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, NULL, 6, NULL, '9WUIMM25'),
('9WEIMM25', 'Algorithmique d\' analyse de données', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 2, NULL, NULL),
('9WEIMM27', 'Bioinformatique et datamining', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM27'),
('9WEIMM28', 'Ordonnancement et application', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM28'),
('9WEIMM29', 'Optimisation et logiciels - Etude de cas en transport', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM29'),
('9WEIMM30', 'Problèmes de dimensionnement de lots', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM30'),
('9WEIMM31', 'Modèles décisionnels pour la sécurité des SI', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEIMM32', 'Portails web d\'entreprises 2', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 11, NULL, '9WUIMM32'),
('9WEIMM33', 'Optimisation pour l\'industrie', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUIMM33'),
('9WEJAM31', 'Enseignement Interculturel', 'E', 26, NULL, 4, NULL, NULL, NULL, 1, 1600, 7, NULL, NULL),
('9WEOTM01', 'Intégration méthodologiques SSI', 'E', NULL, NULL, 36, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUOTM01'),
('9WEOTM02', 'Gestion de projet', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 13, NULL, '9WUOTM02'),
('9WEOTM03', 'Recherche d\'emploi et création d\'entreprise', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 7, NULL, '9WUOTM03'),
('9WEOTM04', 'Sécurité des Systèmes d\'information', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUOTM04'),
('9WEOTM05', 'Politique de la Sécurité d\'un Systèmes d\'Information', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEOTM06', 'Audit Sécurité d\'un Système d\'Information', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEOTM07', 'Identity Access Manag', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUOTM07'),
('9WEOTM08', 'Résilience des Systèmes d\'information', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUOTM08'),
('9WEOTM09', 'Tests d\'intrusion', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUOTM09'),
('9WEOTM10', 'Malware, rétro-ingéniérie', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, NULL),
('9WEOTM11', 'Sécurité des réseaux', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 12, NULL, '9WUOTM11'),
('9WEOTM12', 'Sécurité des Données', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUOTM12'),
('9WEOTM13', 'Sécurité des systèmes', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 12, NULL, NULL),
('9WEOTM14', 'Contrôle d\'accès', 'E', 12, NULL, 12, NULL, NULL, NULL, 1, 2700, 6, NULL, '9WUOTM14');

-- --------------------------------------------------------

--
-- Structure de la table `enseignant`
--

CREATE TABLE `enseignant` (
  `id_ens` varchar(3) NOT NULL,
  `nom` varchar(15) NOT NULL,
  `prenom` varchar(15) NOT NULL,
  `fonction` varchar(15) DEFAULT NULL,
  `HOblig` decimal(5,2) DEFAULT NULL,
  `HMax` decimal(5,2) DEFAULT NULL,
  `CRCT` char(1) DEFAULT 'N',
  `PES_PEDR` char(1) DEFAULT 'N',
  `id_comp` varchar(3) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `enseignant`
--

INSERT INTO `enseignant` (`id_ens`, `nom`, `prenom`, `fonction`, `HOblig`, `HMax`, `CRCT`, `PES_PEDR`, `id_comp`) VALUES
('ChK', 'CHELGHOUM', 'Kamel', 'MCF', '192.00', '192.00', 'N', 'N', 'FB0');

-- --------------------------------------------------------

--
-- Structure de la table `enseignement`
--

CREATE TABLE `enseignement` (
  `code_ec` varchar(10) NOT NULL,
  `annee` varchar(9) NOT NULL,
  `eff_prev` int(4) DEFAULT NULL,
  `eff_reel` int(4) DEFAULT NULL,
  `GpCM` int(2) DEFAULT '1',
  `GpEI` int(2) DEFAULT '0',
  `GpTD` int(2) DEFAULT '0',
  `GpTP` int(2) DEFAULT '0',
  `GpTPL` int(2) DEFAULT '0',
  `GpPRJ` int(2) DEFAULT '0',
  `GpCMSer` int(2) DEFAULT '0',
  `GpEISer` int(2) DEFAULT '0',
  `GpTDSer` int(2) DEFAULT '0',
  `GpTPSer` int(2) DEFAULT '0',
  `GpTPLSer` int(2) DEFAULT '0',
  `GpPRJSer` int(2) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `etape`
--

CREATE TABLE `etape` (
  `code_etape` varchar(10) NOT NULL,
  `vet` int(3) NOT NULL,
  `libelle_vet` varchar(100) NOT NULL,
  `id_comp` varchar(3) NOT NULL DEFAULT 'FB0',
  `code_diplome` varchar(10) DEFAULT NULL,
  `vdi` int(3) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `etape`
--

INSERT INTO `etape` (`code_etape`, `vet`, `libelle_vet`, `id_comp`, `code_diplome`, `vdi`) VALUES
('1WFMK7', 800, 'L1-Portail Mathématiques Informatique', 'FB0', '3WLFPW1', 300),
('3WFM31', 800, 'L2-Informatique', 'FB0', '3WLFINF', 200),
('5WFM31', 800, 'L3-Informatique', 'FB0', '3WLFINF', 300),
('7WFM31', 800, 'M1-Informatique', 'FB0', '5WMFINF', 400),
('9WFMHM', 800, 'M2-Informatique PT G2IHM', 'FB0', '5WMFINF', 503),
('9WFMIM', 800, 'M2-Informatique PT Décisionnelle', 'FB0', '5WMFINF', 502),
('9WFMOT', 800, 'M2-Informatique PT SIS', 'FB0', '5WMFINF', 504);

-- --------------------------------------------------------

--
-- Structure de la table `horscomp`
--

CREATE TABLE `horscomp` (
  `id_ens` varchar(3) NOT NULL,
  `id_comp` varchar(3) NOT NULL,
  `annee` varchar(9) NOT NULL,
  `HCM` int(2) DEFAULT '0',
  `HEI` int(2) DEFAULT '0',
  `HTD` int(2) DEFAULT '0',
  `HTP` int(2) DEFAULT '0',
  `HTPL` int(2) DEFAULT '0',
  `HPRJ` int(2) DEFAULT '0',
  `HEqTD` decimal(5,2) DEFAULT '0.00'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `semestre`
--

CREATE TABLE `semestre` (
  `code_sem` varchar(10) NOT NULL,
  `libelle_sem` varchar(50) NOT NULL,
  `no_sem` int(2) DEFAULT NULL,
  `code_etape` varchar(10) DEFAULT NULL,
  `vet` int(3) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `semestre`
--

INSERT INTO `semestre` (`code_sem`, `libelle_sem`, `no_sem`, `code_etape`, `vet`) VALUES
('0WSHMM01', 'Semestre 10', 10, '9WFMHM', 800),
('0WSIMM02', 'Semestre 10', 10, '9WFMIM', 800),
('0WSOTM01', 'SEMESTRE 10', 10, '9WFMOT', 800),
('1WSK7M01', 'Semestre 1 L1 Math/Info', 1, '1WFMK7', 800),
('2WSK7M01', 'Semestre 2 L2 Math/Info', 2, '1WFMK7', 800),
('3WS31M01', 'Semestre 3 L2 Informatique', 3, '3WFM31', 800),
('4WS31M01', 'Semestre 4 L2 Informatique', 4, '3WFM31', 800),
('5WS31M01', 'Semestre 5 L3 Informatique', 5, '5WFM31', 800),
('6WS31M01', 'Semestre 6 L3 Informatique', 6, '5WFM31', 800),
('7WS31M01', 'Semestre 7 M1 Informatique', 7, '7WFM31', 800),
('8WS31M01', 'Semestre 8 M1 Informatique', 8, '7WFM31', 800),
('9WSHMM01', 'SEMESTRE 9', 9, '9WFMHM', 800),
('9WSIMM02', 'Semestre 9', 9, '9WFMIM', 800),
('9WSOTM01', 'SEMESTRE 9', 9, '9WFMOT', 800);

-- --------------------------------------------------------

--
-- Structure de la table `service`
--

CREATE TABLE `service` (
  `id_ens` varchar(3) NOT NULL,
  `code_ec` varchar(10) NOT NULL,
  `annee` varchar(9) NOT NULL,
  `NbGpCM` int(2) DEFAULT '0',
  `NbGpEI` int(2) DEFAULT '0',
  `NBGpTD` int(2) DEFAULT '0',
  `NbGpTP` int(2) DEFAULT '0',
  `NbGpTPL` int(2) DEFAULT '0',
  `NBGpPRJ` int(2) DEFAULT '0',
  `HEqTD` decimal(5,2) DEFAULT '0.00'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `ue`
--

CREATE TABLE `ue` (
  `code_ue` varchar(10) NOT NULL,
  `libelle_ue` varchar(100) NOT NULL,
  `nature` char(1) DEFAULT 'U',
  `ECTS` int(4) DEFAULT '2700',
  `code_ue_pere` varchar(10) DEFAULT NULL,
  `code_sem` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `ue`
--

INSERT INTO `ue` (`code_ue`, `libelle_ue`, `nature`, `ECTS`, `code_ue_pere`, `code_sem`) VALUES
('0WUHMM01', 'UE 1002 Découverte de l’IHM dans des domaines d’application', 'U', 2, NULL, '0WSHMM01'),
('0WUHMM02', 'UE 1001 STAGE', 'U', 28, NULL, '0WSHMM01'),
('0WUHMM04', 'UE 1003 Compléments de Génie Logiciel', 'U', NULL, NULL, '0WSHMM01'),
('0WUIMM01', '1002 Applications de l’Informatique Décisionnelle', 'U', 2, NULL, '0WSIMM02'),
('0WUIMM03', 'UE 1001 Stage', 'U', 28, NULL, '0WSIMM02'),
('0WUOTM01', 'UE Stage', 'U', 30, NULL, '0WSOTM01'),
('1WCK7M02', 'Choix 1 (1/3)', 'C', NULL, NULL, '1WSK7M01'),
('1WCK7M03', 'Choix 2 (1/3)', 'C', NULL, NULL, '1WSK7M01'),
('1WUK7M01', 'UE101 Calculs et mathématiques', 'U', 6, NULL, '1WSK7M01'),
('1WUK7M02', 'UE102 Algorithmique et Programmation 1', 'U', 6, NULL, '1WSK7M01'),
('1WUK7M03', 'UE111 Fondements mathématiques', 'U', 3, NULL, '1WSK7M01'),
('1WUK7M04', 'UE112 Introduction au Web', 'U', 3, NULL, '1WSK7M01'),
('1WUK7M06', 'UE182 Nombres complexes et géométrie', 'U', 3, '1WCK7M02', '1WSK7M01'),
('1WUK7M07', 'UE183 Codage numérique : du nombre au pixel', 'U', 3, '1WCK7M02', '1WSK7M01'),
('1WUK7M08', 'UE184 Culture Scientifique', 'U', 3, '1WCK7M02', '1WSK7M01'),
('1WUK7M09', 'UE103 Introduction aux systèmes logiques et numériques', 'U', 3, '1WCK7M03', '1WSK7M01'),
('1WUK7M10', 'UE Mécanique du point', 'U', 3, '1WCK7M03', '1WSK7M01'),
('1WUK7M11', 'UE Economie S1', 'U', 3, '1WCK7M03', '1WSK7M01'),
('1WUK7M12', 'UE190 Compétences Transversales', 'U', 6, NULL, '1WSK7M01'),
('2WCK7M01', 'Choix d\'orientation (1/2)', 'C', NULL, NULL, '2WSK7M01'),
('2WCK7M02', 'Choix (1/2)', 'C', NULL, NULL, '2WSK7M01'),
('2WCK7M03', 'Choix info-phys-éco', 'C', NULL, NULL, '2WSK7M01'),
('2WCK7M04', 'Choix (2/5)', 'C', NULL, NULL, '2WSK7M01'),
('2WCK7M06', 'Choix UE SPI (1/2)', 'C', NULL, NULL, '2WSK7M01'),
('2WOK7M01', 'Orientation Info SPI', 'O', NULL, '2WCK7M01', '2WSK7M01'),
('2WOK7M02', 'Orientation Math Info', 'O', NULL, '2WCK7M01', '2WSK7M01'),
('2WUK7M01', 'UE Algorithmique et Programmation 2', 'U', 6, '2WCK7M03', '2WSK7M01'),
('2WUK7M03', 'UE210 Outils Mathématiques', 'U', 6, '2WOK7M01', '2WSK7M01'),
('2WUK7M04', 'UE290 Compétences Transversales', 'U', 6, NULL, '2WSK7M01'),
('2WUK7M05', 'UE203 Méthodologie de Conception et de Programmation', 'U', 6, '2WCK7M02', '2WSK7M01'),
('2WUK7M06', 'UE204 SPI EEA', 'U', 6, '2WCK7M02', '2WSK7M01'),
('2WUK7M07', 'UE Algèbre linéaire 1', 'U', 6, '2WOK7M02', '2WSK7M01'),
('2WUK7M08', 'UE212 Analyse 1', 'U', 6, '2WOK7M02', '2WSK7M01'),
('2WUK7M09', 'UE Electromagnétisme', 'U', 6, '2WCK7M03', '2WSK7M01'),
('2WUK7M10', 'UE Economie - statistiques', 'U', 6, '2WCK7M03', '2WSK7M01'),
('2WUK7M11', 'UE 231 Arithmétique', 'U', 3, '2WCK7M04', '2WSK7M01'),
('2WUK7M12', 'UE232 Compléments d\'analyse', 'U', 3, '2WCK7M04', '2WSK7M01'),
('2WUK7M13', 'UE233 Codage Numérique : du nombre au pixel', 'U', 3, '2WCK7M04', '2WSK7M01'),
('2WUK7M14', 'UE234 Méthodologie de Conception et de Programmation', 'U', 3, '2WCK7M04', '2WSK7M01'),
('2WUK7M15', 'UE235 Méthodo de Conception et de Programmation - Avancée', 'U', 3, '2WCK7M04', '2WSK7M01'),
('2WUK7M16', 'UE EEA', 'U', 6, '2WCK7M06', '2WSK7M01'),
('2WUK7M17', 'UE Mécanique du solide', 'U', 6, '2WCK7M06', '2WSK7M01'),
('3WC31M01', 'Choix Architecture Avancée/Remise à niveau Info (1/3)', 'C', NULL, NULL, '3WS31M01'),
('3WU31M01', 'UE MATHS DISCRETES 1', 'U', 6, NULL, '3WS31M01'),
('3WU31M02', 'UE ALGORITHMIQUE PROGRAMMATION 3', 'U', 6, NULL, '3WS31M01'),
('3WU31M03', 'UE PROGRAMMATION AVANCEE', 'U', 3, NULL, '3WS31M01'),
('3WU31M04', 'UE ARCHITECTURE DES ORDINATEURS', 'U', 3, '3WC31M01', '3WS31M01'),
('3WU31M05', 'UE REMISE A NIVEAU INFORMATIQUE', 'U', 3, '3WC31M01', '3WS31M01'),
('3WU31M06', 'UE LANGUE 2 LANGUE 2', 'U', 3, '3WC31M01', '3WS31M01'),
('3WU31M07', 'UE INTRODUCTION AUX BASES DE DONNEES', 'U', 3, NULL, '3WS31M01'),
('3WU31M08', 'UE OUTILS SYSTEME / RESEAU 1', 'U', 6, NULL, '3WS31M01'),
('3WU31M09', 'UE LANGUES', 'U', 3, NULL, '3WS31M01'),
('4WC31M01', 'Choix Probabilités Statistiques / Remise à niveau Maths(1/3)', 'C', NULL, NULL, '4WS31M01'),
('4WU31M01', 'UE MATHS DISCRETES 2-LANGAGES FORMELS', 'U', 6, NULL, '4WS31M01'),
('4WU31M02', 'UE INTERFACES GRAPHIQUES / PROJETS DE SYNTHESE', 'U', 6, NULL, '4WS31M01'),
('4WU31M03', 'UE SYSTEME 1', 'U', 3, NULL, '4WS31M01'),
('4WU31M04', 'UE PROBABILITES STATISTIQUES', 'U', 3, '4WC31M01', '4WS31M01'),
('4WU31M05', 'UE REMISE A NIVEAU MATHEMATIQUES', 'U', 3, '4WC31M01', '4WS31M01'),
('4WU31M06', 'UE LANGUE 2', 'U', 3, '4WC31M01', '4WS31M01'),
('4WU31M07', 'UE BASES DE LA PROGRAMMATION OBJET', 'U', 6, NULL, '4WS31M01'),
('4WU31M08', 'UE LIBRE', 'U', 3, NULL, '4WS31M01'),
('4WU31M09', 'UE LANGUES', 'U', 3, NULL, '4WS31M01'),
('5WC31M01', 'Choix Ouverture (1/4)', 'C', NULL, NULL, '5WS31M01'),
('5WU31M01', 'UE LOGIQUE', 'U', 6, NULL, '5WS31M01'),
('5WU31M02', 'UE ALGORITHMIQUE / CONCEPTION PROGRAMMATION OBJET AVANCEE', 'U', 9, NULL, '5WS31M01'),
('5WU31M03', 'UE Bases de données relationnelles et web', 'U', 6, NULL, '5WS31M01'),
('5WU31M04', 'UE SYSTEME 2', 'U', 3, NULL, '5WS31M01'),
('5WU31M05', 'UE PROGRAMMATION FONCTIONNELLE', 'U', 3, '5WC31M01', '5WS31M01'),
('5WU31M06', 'UE XML', 'U', 3, '5WC31M01', '5WS31M01'),
('5WU31M07', 'UE COMPRESSION DE DONNEES', 'U', 3, '5WC31M01', '5WS31M01'),
('5WU31M08', 'UE INTRODUCTION A LA  ROBOTIQUE', 'U', 3, '5WC31M01', '5WS31M01'),
('5WU31M09', 'UE COMPETENCES TRANSVERSALES', 'U', 3, NULL, '5WS31M01'),
('6WC31M01', 'Choix Introduction à l\'IA/Infographie (1/2)', 'C', NULL, NULL, '5WS31M01'),
('6WU31M01', 'UE SECURITE/RESEAU', 'U', 6, NULL, '6WS31M01'),
('6WU31M02', 'UE OPTIMISATION / MODELISATION', 'U', 6, NULL, '6WS31M01'),
('6WU31M03', 'UE COMPILATION', 'U', 3, NULL, '6WS31M01'),
('6WU31M04', 'UE PREMIERS PAS VERS L\'INGENIERIE DU LOGICIEL', 'U', 3, NULL, '6WS31M01'),
('6WU31M05', 'UE INTRODUCTION A L\'INTELLIGENCE ARTIFICIELLE', 'U', 3, '6WC31M01', '6WS31M01'),
('6WU31M06', 'UE INFOGRAPHIE', 'U', 3, '6WC31M01', '6WS31M01'),
('6WU31M07', 'UE OUVERTURE', 'U', 6, NULL, '6WS31M01'),
('6WU31M08', 'UE Stage', 'U', 3, NULL, '6WS31M01'),
('7WU31M01', 'UE 721 ANALYSE ET CONCEPTION DE LOGICIELS', 'U', 4, NULL, '7WS31M01'),
('7WU31M02', 'UE 722 ALGORITHMIQUE ET COMPLEXITE', 'U', 5, NULL, '7WS31M01'),
('7WU31M03', 'UE 723 Design patterns', 'U', 4, NULL, '7WS31M01'),
('7WU31M04', 'UE 724 LOGIQUE ET MODELES DE CALCUL', 'U', 5, NULL, '7WS31M01'),
('7WU31M05', 'UE 725 OPTIMISATION COMBINATOIRE', 'U', 5, NULL, '7WS31M01'),
('7WU31M06', 'UE 726 RESEAUX', 'U', 5, NULL, '7WS31M01'),
('7WU31M07', 'UE 701 ANGLAIS', 'U', 2, NULL, '7WS31M01'),
('8WC31M01', 'CHOIX 810 Travail d\'Etude et de Recherche (1/1)', 'C', NULL, NULL, '8WS31M01'),
('8WC31M15', 'CHOIX UE / 6 UE AU CHOIX', 'C', NULL, NULL, '8WS31M01'),
('8WU31M01', 'UE 821 INTELLIGENCE ARTIFICIELLE', 'U', 3, NULL, '8WS31M01'),
('8WU31M02', 'UE 822 REPRESENTATION DES DONNEES VISUELLES', 'U', 3, NULL, '8WS31M01'),
('8WU31M03', 'UE 801 ANGLAIS', 'U', 2, NULL, '8WS31M01'),
('8WU31M04', 'UE 802 TECHNIQUES DE COMMUNCATION ET D\'EXPRESSION', 'U', 2, NULL, '8WS31M01'),
('8WU31M05', 'UE 851 OUTILS D\'AIDE A LA DECISION', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M06', 'UE 852 INTRODUCTION A LA FOUILLE DE DONNEES', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M07', 'UE 853 INITIATION A L\'ORDONNANCEMENT', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M08', 'UE 854 METAHEURISTIQUES & ALGORITHMES DE RECH STOCHASTIQUE', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M09', 'UE 855 INTRODUCTION AUX MODELES FINANCIERS', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M10', 'UE 856 SYSTEMES D\'INFORMATION DECISIONNELS', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M11', 'UE 857 GRAPHES D\'ATTAQUES ET RESEAUX DE TRANSPORT', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M12', 'UE 861 TRAITEMENT D\'IMAGES ET VISION PAR ORDINATEUR', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M13', 'UE 862 DONNEES SEMI-STRUCTUREES ET XML', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M14', 'UE 863 SEMANTIQUE DES LANGAGES DE PROGRAMMATION', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M15', 'UE 864 BASES DE DONNEES AVANCEES', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M16', 'UE 841 INTRODUCTION A LA CRYPTOGRAPHIE', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M17', 'UE 842 INTRODUCTION A LA SECUITE DES SYSTEMES D\'INFORMATION', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M18', 'UE 843 ADMINISTRATION D\'UN SYSTEME EN RESEAU', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M19', 'UE 844 DROIT INFORMATIQUE', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M20', 'UE 845 ORDONNANCEMENT ET TEMPS-REEL SOUS LINUX', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M21', 'UE 846 METHODOLOGIE DE LA SECURITE', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M22', 'UE 831 ERGONOMIE SYSTEMES INTERACTIFS EXPERIENCE UTILISATEUR', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M23', 'UE 832 PSYCHOLOGIE COGNITIVE ET DIVERSITE ET INTERACTIONS', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M24', 'UE 833 INITIATION A LA MULTIMODALITE', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M25', 'UE 834 PROTOTYPAGE D\'INTERFACES PAR LANGAGE DE SCRIPT', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M26', 'UE 835 LA PLATEFORME.NET', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M27', 'UE 836 FONCTIONNEMENT D\'UN MOTEUR DE RENDU 3D', 'U', 3, '8WC31M15', '8WS31M01'),
('8WU31M28', 'UE 811 INITIATION A LA  RECHERCHE', 'U', 2, '8WC31M01', '8WS31M01'),
('8WU31M29', 'UE 812 PROJET EN ENTREPRISE(STAGE EN ALTERNANCE)', 'U', 2, '8WC31M01', '8WS31M01'),
('9WUCPM18', 'UE ALT 01 Management et gestion de projet', 'U', 2, NULL, '9WSHMM01'),
('9WUCPM19', 'UE ALT 02 Création d\'entreprise', 'U', 2, NULL, '9WSHMM01'),
('9WUCPM20', 'UE ALT 03 Marketing', 'U', 2, NULL, '9WSHMM01'),
('9WUCPM21', 'UE ALT 04 Droit et fiscalité', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM01', 'UE 901-1 Anglais', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM02', 'UE 901-2 Français Langue Etrangère', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM03', 'UE 902 Recherche d\'emploi et Création d\'Entreprise', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM04', 'UE 969 Ergonomie et facteurs humains pour l’accessibilité', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM05', 'UE 970 Méthodes pour la conception centrée utilisateurs', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM06', 'UE 971 Méthodes pour l\'évaluation centrée utilisateurs', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM07', 'UE 972 Techniques de visualisation de données et analyse', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM08', 'EU 973 Modèles pour concevoir des systèmes Interactifs', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM09', 'UE 912 Gestion de Projet', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM10', 'UE 978-979 Techniques d’interactions innovantes', 'U', 4, NULL, '9WSHMM01'),
('9WUHMM11', 'UE 974-975 Développement mobile', 'U', 4, NULL, '9WSHMM01'),
('9WUHMM12', 'UE 976-977 Développement Web', 'U', 4, NULL, '9WSHMM01'),
('9WUHMM13', 'UE 911 Intégration Méthodologique IHM', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM14', 'UE 901-1 Anglais', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM15', 'UE 901-2 Français Langue Etrangère', 'U', 2, NULL, '9WSHMM01'),
('9WUHMM16', 'UE Conception Logicielle et Gestion de Projets', 'U', 4, NULL, '9WSHMM01'),
('9WUHMM17', 'UE Entrepôts de Données et Portail Web d’Entreprise', 'U', 3, NULL, '9WSHMM01'),
('9WUHMM18', 'UE Sécurité des Réseaux et des Systèmes d\'Information', 'U', 3, NULL, '9WSHMM01'),
('9WUHMM19', 'UE983-984 ERP -Introduction au Progiciel de Gestion Intégré', 'U', 3, NULL, '9WSHMM01'),
('9WUHMM20', 'UE 974-975 Développement mobile', 'U', 4, NULL, '9WSHMM01'),
('9WUHMM21', 'UE 976-977 Développement Web', 'U', 4, NULL, '9WSHMM01'),
('9WUHMM50', 'UE 911 STG Projet entreprise', 'U', 3, NULL, '9WSHMM01'),
('9WUHMM51', 'UE Création d\'entreprise(Alternance)', 'U', 2, NULL, '9WSHMM01'),
('9WUIMM01', '911 Intégration Méthodologique OPAL', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM02', '912 Gestion de Projet', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM03', '902 Professionnalisation OPAL', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM04', '985 Résolution de Modèles d\'Optimisation de Grande Taille', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM05', '986 Combinatoire et Algorithmes d\'Approximation', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM06', '987 Théorie des Graphes', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM07', '988 Optimisation Globale', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM08', '990 Théorie de la complexité et inapproximabilité', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM09', '911 Intégration Méthodologique SID', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM10', '912 Gestion de Projet', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM11', '902 Professionnalisation SID', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM12', 'UE Isfates : Enseignement interculturel', 'U', 2, NULL, '9WSHMM01'),
('9WUIMM13', '991 Datawarehouse', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM14', '992 Sécurité des systèmes d\'information', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM15', '993 Conception d\'un Système d\'Information Décisionnel', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM16', '994 Reporting & Dashboarding : Création de tableaux de bord', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM17', '995 Portails Web d\'Entreprises 1', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM18', '996-1 Aide à la Décision Multicritère', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM19', '996-2 Décision dans l\'Incertain', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM20', '996-3 Décision pour les Systèmes Parallèles et Distribués', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM21', '996-4 Problèmes de Satisfaction de Contraintes', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM22', '996-5 Modèles de Satisfabilité(SAT)', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM23', '961 BigData', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM24', '997-1 Fouille Informatique des Données', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM25', '972 Techniques de visualisation de données et analyse', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM26', '997-2 Algorithmique d\'Analyse des Données', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM27', '997-3 BioInformatique et Datamining', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM28', '998-1 Ordonnancement et Applications', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM29', '998-2 Optimisation et Logiciels - Etude de Cas en Transport', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM30', '998-3 Problèmes de Dimensionnement de lots', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM31', '998-4 Modèles Décionnels pour la Sécurité des SI', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM32', '998-5 Portails Web d\'Entreprises 2', 'U', 2, NULL, '9WSIMM02'),
('9WUIMM33', '998-6 Optimisation pour l\'industrie', 'U', 2, NULL, '9WSIMM02'),
('9WUOTM01', 'UE 911 Intégration Méthodologiques SSI', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM02', 'UE 912 Gestion de Projet', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM03', 'UE 913 Recherche d\'Emploi et Création d\'Entreprise', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM04', 'UE 932 Sécurité des systèmes d\'information', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM05', 'UE 933 Politique de la Sécurité d\'un Système d\'Information', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM06', 'UE 934 Audit de la Sécurité d\'un Système d\'Information', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM07', 'UE 935 Identity Access Management', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM08', 'UE 936 Résilience des Systèmes d\'Information', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM09', 'UE 937 Tests d\'Intrusion', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM10', 'UE 938 Malware, retro-ingénierie', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM11', 'UE 939 Sécurité des Réseaux', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM12', 'UE 940 Sécurité des Données', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM13', 'UE 941 Sécurité des Systèmes', 'U', 2, NULL, '9WSOTM01'),
('9WUOTM14', 'UE 942 Contrôle d\'Accès', 'U', 2, NULL, '9WSOTM01');

--
-- Index pour les tables exportées
--

--
-- Index pour la table `annee_univ`
--
ALTER TABLE `annee_univ`
  ADD PRIMARY KEY (`annee`);

--
-- Index pour la table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`no_cat`);

--
-- Index pour la table `composante`
--
ALTER TABLE `composante`
  ADD PRIMARY KEY (`id_comp`);

--
-- Index pour la table `comp_courante`
--
ALTER TABLE `comp_courante`
  ADD PRIMARY KEY (`id_comp`);

--
-- Index pour la table `diplome`
--
ALTER TABLE `diplome`
  ADD PRIMARY KEY (`code_diplome`,`vdi`);

--
-- Index pour la table `ec`
--
ALTER TABLE `ec`
  ADD PRIMARY KEY (`code_ec`),
  ADD KEY `ec_fk_1` (`no_cat`),
  ADD KEY `ec_fk_2` (`code_ue`),
  ADD KEY `code_ec_pere` (`code_ec_pere`);

--
-- Index pour la table `enseignant`
--
ALTER TABLE `enseignant`
  ADD PRIMARY KEY (`id_ens`),
  ADD KEY `enseignant_fk_1` (`id_comp`);

--
-- Index pour la table `enseignement`
--
ALTER TABLE `enseignement`
  ADD PRIMARY KEY (`code_ec`,`annee`),
  ADD KEY `enseignement_fk_2` (`annee`);

--
-- Index pour la table `etape`
--
ALTER TABLE `etape`
  ADD PRIMARY KEY (`code_etape`,`vet`),
  ADD KEY `etape_fk_1` (`code_diplome`,`vdi`),
  ADD KEY `etape_fk_2` (`id_comp`);

--
-- Index pour la table `horscomp`
--
ALTER TABLE `horscomp`
  ADD PRIMARY KEY (`id_ens`,`id_comp`,`annee`),
  ADD KEY `horscomp_fk_1` (`annee`),
  ADD KEY `horscomp_fk_2` (`id_comp`);

--
-- Index pour la table `semestre`
--
ALTER TABLE `semestre`
  ADD PRIMARY KEY (`code_sem`),
  ADD KEY `semestre_fk_1` (`code_etape`,`vet`);

--
-- Index pour la table `service`
--
ALTER TABLE `service`
  ADD PRIMARY KEY (`id_ens`,`code_ec`,`annee`),
  ADD KEY `service_fk_1` (`code_ec`,`annee`);

--
-- Index pour la table `ue`
--
ALTER TABLE `ue`
  ADD PRIMARY KEY (`code_ue`),
  ADD KEY `ue_fk_1` (`code_sem`),
  ADD KEY `code_ue_pere` (`code_ue_pere`);

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `comp_courante`
--
ALTER TABLE `comp_courante`
  ADD CONSTRAINT `comp_courante_ibfk_1` FOREIGN KEY (`id_comp`) REFERENCES `composante` (`id_comp`);

--
-- Contraintes pour la table `ec`
--
ALTER TABLE `ec`
  ADD CONSTRAINT `ec_fk_1` FOREIGN KEY (`no_cat`) REFERENCES `categories` (`no_cat`),
  ADD CONSTRAINT `ec_fk_2` FOREIGN KEY (`code_ue`) REFERENCES `ue` (`code_ue`),
  ADD CONSTRAINT `ec_ibfk_1` FOREIGN KEY (`code_ec_pere`) REFERENCES `ec` (`code_ec`);

--
-- Contraintes pour la table `enseignant`
--
ALTER TABLE `enseignant`
  ADD CONSTRAINT `enseignant_fk_1` FOREIGN KEY (`id_comp`) REFERENCES `composante` (`id_comp`);

--
-- Contraintes pour la table `enseignement`
--
ALTER TABLE `enseignement`
  ADD CONSTRAINT `enseignement_fk_1` FOREIGN KEY (`code_ec`) REFERENCES `ec` (`code_ec`),
  ADD CONSTRAINT `enseignement_fk_2` FOREIGN KEY (`annee`) REFERENCES `annee_univ` (`annee`);

--
-- Contraintes pour la table `etape`
--
ALTER TABLE `etape`
  ADD CONSTRAINT `etape_fk_1` FOREIGN KEY (`code_diplome`,`vdi`) REFERENCES `diplome` (`code_diplome`, `vdi`),
  ADD CONSTRAINT `etape_fk_2` FOREIGN KEY (`id_comp`) REFERENCES `composante` (`id_comp`);

--
-- Contraintes pour la table `horscomp`
--
ALTER TABLE `horscomp`
  ADD CONSTRAINT `horscomp_fk_1` FOREIGN KEY (`annee`) REFERENCES `annee_univ` (`annee`),
  ADD CONSTRAINT `horscomp_fk_2` FOREIGN KEY (`id_comp`) REFERENCES `composante` (`id_comp`),
  ADD CONSTRAINT `horscomp_fk_3` FOREIGN KEY (`id_ens`) REFERENCES `enseignant` (`id_ens`);

--
-- Contraintes pour la table `semestre`
--
ALTER TABLE `semestre`
  ADD CONSTRAINT `semestre_fk_1` FOREIGN KEY (`code_etape`,`vet`) REFERENCES `etape` (`code_etape`, `vet`);

--
-- Contraintes pour la table `service`
--
ALTER TABLE `service`
  ADD CONSTRAINT `service_fk_1` FOREIGN KEY (`code_ec`,`annee`) REFERENCES `enseignement` (`code_ec`, `annee`),
  ADD CONSTRAINT `service_fk_3` FOREIGN KEY (`id_ens`) REFERENCES `enseignant` (`id_ens`);

--
-- Contraintes pour la table `ue`
--
ALTER TABLE `ue`
  ADD CONSTRAINT `ue_fk_1` FOREIGN KEY (`code_sem`) REFERENCES `semestre` (`code_sem`),
  ADD CONSTRAINT `ue_ibfk_1` FOREIGN KEY (`code_ue_pere`) REFERENCES `ue` (`code_ue`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
