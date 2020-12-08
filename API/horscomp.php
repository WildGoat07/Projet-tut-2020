<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `horscomp`");

    $horscomp = new stdClass();
    $horscomp->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->id_ens = utf8_encode($req['id_ens']);

        $obj->id_comp = utf8_encode($req['id_comp']);

        $obj->annee = utf8_encode($req['annee']);

        $obj->HCM = utf8_encode($req['HCM']);

        $obj->HEI = utf8_encode($req['HEI']);

        $obj->HTD = utf8_encode($req['HTD']);

        $obj->HTP = utf8_encode($req['HTP']);

        $obj->HTPL = utf8_encode($req['HTPL']);

        $obj->HPRJ = utf8_encode($req['HPRJ']);

        $obj->HEqTD = utf8_encode($req['HEqTD']);

        $horscomp->values[] = $obj;
    }

    $horscomp->success = true;

    echo json_encode($horscomp);
} else {
    echo json_encode($connectionDB);
}
