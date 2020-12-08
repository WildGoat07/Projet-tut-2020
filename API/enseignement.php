<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT annee FROM `enseignement`");

    $enseignement = new stdClass();
    $enseignement->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->code_ec = utf8_encode($req['code_ec']);

        $obj->annee = utf8_encode($req['annee']);

        $obj->eff_prev = utf8_encode($req['eff_prev']);

        $obj->eff_reel = utf8_encode($req['eff_reel']);

        $obj->GpCM = utf8_encode($req['GpCM']);

        $obj->GpEI = utf8_encode($req['GpEI']);

        $obj->GpTD = utf8_encode($req['GpTD']);

        $obj->GpTP = utf8_encode($req['GpTP']);

        $obj->GpTPL = utf8_encode($req['GpTPL']);

        $obj->GpPRJ = utf8_encode($req['GpPRJ']);

        $obj->GpCMSer = utf8_encode($req['GpCMSer']);

        $obj->GpEISer = utf8_encode($req['GpEISer']);

        $obj->GpTDSer = utf8_encode($req['GpTDSer']);

        $obj->GpTPSer = utf8_encode($req['GpTPSer']);

        $obj->GpTPLSer = utf8_encode($req['GpTPLSer']);

        $obj->GpPRJer = utf8_encode($req['GpPRJSer']);

        $enseignement->values[] = $obj;
    }

    $enseignement->success = true;

    echo json_encode($enseignement);
} else {
    echo json_encode($connectionDB);
}
