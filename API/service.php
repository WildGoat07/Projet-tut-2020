<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `service`");

    $service = new stdClass();
    $service->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->id_ens = utf8_encode($req['id_ens']);

        $obj->code_ec = utf8_encode($req['code_ec']);

        $obj->annee = utf8_encode($req['annee']);

        $obj->NbGpCM = utf8_encode($req['NbGpCM']);

        $obj->NbGpEI = utf8_encode($req['NbGpEI']);

        $obj->NbGpTD = utf8_encode($req['NBGpTD']);

        $obj->NbGpTP = utf8_encode($req['NbGpTP']);

        $obj->NbGpTPL = utf8_encode($req['NbGpTPL']);

        $obj->NBGpPRJ = utf8_encode($req['NBGpPRJ']);

        $obj->NbHEqTDGp = utf8_encode($req['HEqTD']);

        $service->values[] = $obj;
    }

    $service->success = true;

    echo json_encode($service);
} else {
    echo json_encode($connectionDB);
}
