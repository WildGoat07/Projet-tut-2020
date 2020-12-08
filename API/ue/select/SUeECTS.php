<?php
require_once '../../app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT code_ue, libelle_ue, nature, ECTS, code_ue_pere, code_sem FROM ue WHERE ECTS LIKE '%" .$_POST['ECTS']. "%'
                                                                                                        or ECTS LIKE UPPER('%" .$_POST['ECTS']. "%')");

    $ue = new stdClass();
    $ue->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();
        
        $obj->code_ue = utf8_encode($req['code_ue']);

        $obj->libelle_ue = utf8_encode($req['libelle_ue']);

        $obj->nature = utf8_encode($req['nature']);

        $obj->ECTS = utf8_encode($req['ECTS']);

        $obj->code_ue_pere = utf8_encode($req['code_ue_pere']);

        $obj->code_sem = utf8_encode($req['code_sem']);

        
        $ue->values[]=$obj;
    }

    $ue->success = true;

    echo json_encode($ue);

} else {
    //echo json_encode($connectionDB);
}