<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `semestre`");

    $semestre = new stdClass();
    $semestre->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->code_sem = utf8_encode($req['code_sem']);

        $obj->libelle_sem = utf8_encode($req['libelle_sem']);

        $obj->no_sem = utf8_encode($req['no_sem']);

        $obj->code_etape = utf8_encode($req['code_etape']);

        $obj->vet = utf8_encode($req['vet']);

        $semestre->values[] = $obj;
    }

    $semestre->success = true;

    echo json_encode($semestre);
} else {
    echo json_encode($connectionDB);
}
