<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

if ($db == true) {
    $requete = $db->prepare("SELECT * FROM `ec`");
    $requete->execute();
    $resultats = $requete->fetchAll();

    $ec["success"] = true;
    $ec["message"] = "Voici tous les ec";
    $ec["results"]["nbEc"] = count($resultats);
    $ec["results"]["Ec"] = $resultats;
} else {
    echo json_encode($connectionDB);
}

echo json_encode($ec);
