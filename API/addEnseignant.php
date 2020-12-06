<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

if ($db == true) {
    if (!empty($_POST["id_ens"]) && !empty($_POST["nom"]) && !empty($_POST["prenom"]) && !empty($_POST["fonction"]) && !empty($_POST["HOblig"]) && !empty($_POST["HMax"]) && !empty($_POST["CRCT"]) && !empty($_POST["PES_PEDR"]) && !empty($_POST["id_comp"])) {


        $requete = $db->prepare("INSERT INTO `enseignant` (`id_ens`, `nom`, `prenom`, `fonction`, `HOblig`, `HMax`, `CRCT`, `PES_PEDR`, `id_comp`) VALUES (:id_ens, :nom, :prenom, :fonction, :HOblig, :HMax, :CRCT, :PES_PEDR, :id_comp);");
        $requete->bindParam(':id_ens', $_POST["id_ens"]);
        $requete->bindParam(':nom', $_POST["nom"]);
        $requete->bindParam(':prenom', $_POST["prenom"]);
        $requete->bindParam(':fonction', $_POST["fonction"]);
        $requete->bindParam(':HOblig', $_POST["HOblig"]);
        $requete->bindParam(':HMax', $_POST["HMax"]);
        $requete->bindParam(':CRCT', $_POST["CRCT"]);
        $requete->bindParam(':PES_PEDR', $_POST["PES_PEDR"]);
        $requete->bindParam(':id_comp', $_POST["id_comp"]);
        $requete->execute();

        $enseignant["success"] = true;
        $enseignant["message"] = "L'enseignant a bien été ajouté";
    } else {
        $enseignant["success"] = false;
        $enseignant["message"] = "Il manque des informations !";
    }
} else {
    echo json_encode($connectionDB);
}

echo json_encode($enseignant);
