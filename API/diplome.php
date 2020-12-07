<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `diplome`");

    $diplome = new stdClass();
    $diplome->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->code_diplome = utf8_encode($req['code_diplome']);

        $obj->libelle_diplome = utf8_encode($req['libelle_diplome']);

        $obj->vdi = utf8_encode($req['vdi']);

        $obj->libelle_vdi = utf8_encode($req['libelle_vdi']);

        $obj->annee_deb = utf8_encode($req['annee_deb']);

        $obj->annee_fin = utf8_encode($req['annee_fin']);

        $diplome->values[] = $obj;
    }

    $diplome->success = true;

    echo json_encode($diplome);
} else {
    echo json_encode($connectionDB);
}
