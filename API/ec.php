<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `ec`");

    $ec = new stdClass();
    $ec->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->code_ec = utf8_encode($req['code_ec']);

        $obj->libelle_ec = utf8_encode($req['libelle_ec']);

        $obj->nature = utf8_encode($req['nature']);

        $obj->HCM = utf8_encode($req['HCM']);

        $obj->HEI = utf8_encode($req['HEI']);

        $obj->HTD = utf8_encode($req['HTD']);

        $obj->HTP = utf8_encode($req['HTP']);

        $obj->HTPL = utf8_encode($req['HTPL']);

        $obj->HPRJ = utf8_encode($req['HPRJ']);

        $obj->NbEpr = utf8_encode($req['NbEpr']);

        $obj->CNU = utf8_encode($req['CNU']);

        $obj->no_cat = utf8_encode($req['no_cat']);

        $obj->code_ec_pere = utf8_encode($req['code_ec_pere']);

        $obj->code_ue = utf8_encode($req['code_ue']);

        $ec->values[] = $obj;
    }

    $ec->success = true;

    echo json_encode($ec);
} else {
    echo json_encode($connectionDB);
}
