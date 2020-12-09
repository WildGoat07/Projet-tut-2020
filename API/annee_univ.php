<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $strReq = "SELECT `annee` FROM `annee_univ`";
    $postObj = json_decode(file_get_contents('php://input'));

    $requete = $db->query($strReq);

    $annee = new stdClass();
    $annee->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->annee = utf8_encode($req['annee']);

        $annee->values[] = $obj;
    }

    $annee->success = true;

    echo json_encode($annee);
} else {
    echo json_encode($connectionDB);
}
