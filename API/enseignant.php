<?php
require_once 'app/Database.php';

header('Content-Type: application/json');
if ($db == true) {

    $requete = $db->prepare("SELECT * FROM `enseignant`");
    $requete->execute();


    $resultats = $requete->fetchAll();


    $enseignant["success"] = true;
    $enseignant["values"] = $resultats;
} else {
    echo json_encode($connectionDB);
}



echo json_encode($enseignant);
