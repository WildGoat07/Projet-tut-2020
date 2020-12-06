<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

if ($db == true) {
    $requete = $db->prepare("SELECT * FROM `diplome`");
    $requete->execute();
    $resultats = $requete->fetchAll();

    $diplome["success"] = true;
    $diplome["message"] = "Voici tous les diplomes";
    $diplome["results"]["nbDiplomes"] = count($resultats);
    $diplome["results"]["Diplomes"] = $resultats;
} else {
    echo json_encode($connectionDB);
}


echo json_encode($diplome);
