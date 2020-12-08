<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT annee FROM `annee_univ`");

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
