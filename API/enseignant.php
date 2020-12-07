<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `enseignant`");

    $enseignant = new stdClass();
    $enseignant->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->id_ens = utf8_encode($req['id_ens']);

        $obj->nom = utf8_encode($req['nom']);

        $obj->prenom = utf8_encode($req['prenom']);

        $obj->fonction = utf8_encode($req['fonction']);

        $obj->HOblig = utf8_encode($req['HOblig']);

        $obj->HMax = utf8_encode($req['HMax']);

        $obj->CRCT = utf8_encode($req['CRCT']);

        $obj->PES_PEDR = utf8_encode($req['PES_PEDR']);

        $obj->id_comp = utf8_encode($req['id_comp']);

        $enseignant->values[] = $obj;
    }

    $enseignant->success = true;

    echo json_encode($enseignant);
} else {
    echo json_encode($connectionDB);
}
