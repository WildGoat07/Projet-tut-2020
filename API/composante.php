<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `composante`");

    $composante = new stdClass();
    $composante->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->id_comp = utf8_encode($req['id_comp']);

        $obj->nom_comp = utf8_encode($req['nom_comp']);

        $obj->lieu_comp = utf8_encode($req['lieu_comp']);

        $composante->values[] = $obj;
    }

    $composante->success = true;

    echo json_encode($composante);
} else {
    echo json_encode($connectionDB);
}
