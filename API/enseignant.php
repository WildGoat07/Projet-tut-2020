<?php
require_once 'app/Database.php';

header('Content-Type: application/json');
if ($db == true) {
    if (!empty($_POST["id_ens"])) {
        $requete = $db->prepare("SELECT * FROM `enseignant` WHERE `ìd_ens` LIKE :ens");
        $requete->bindParam(':ens', $_POST["id_ens"]);
        $requete->execute();
    } else {
        $requete = $db->prepare("SELECT * FROM `enseignant`");
        $requete->execute();
    }
    $resultats = $requete->fetchAll();

    $enseignant["success"] = true;
    $enseignant["message"] = "Voici tous les enseignants";
    $enseignant["results"]["nbenseignants"] = count($resultats);
    $enseignant["results"]["enseignants"] = $resultats;
} else {
    echo json_encode($connectionDB);
}



echo json_encode($enseignant);
