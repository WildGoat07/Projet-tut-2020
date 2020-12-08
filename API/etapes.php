<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `etape`");

    $etape = new stdClass();
    $etape->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->code_etape = utf8_encode($req['code_etape']);

        $obj->vet = utf8_encode($req['vet']);

        $obj->libelle_vet = utf8_encode($req['libelle_vet']);

        $obj->id_comp = utf8_encode($req['id_comp']);

        $obj->code_diplome = utf8_encode($req['code_diplome']);

        $obj->vdi = utf8_encode($req['vdi']);

        $etape->values[] = $obj;
    }

    $etape->success = true;

    echo json_encode($etape);
} else {
    echo json_encode($connectionDB);
}
