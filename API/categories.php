<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM `categories`");

    $categorie = new stdClass();
    $categorie->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->no_cat = utf8_encode($req['no_cat']);

        $obj->categorie = utf8_encode($req['categorie']);

        $categorie->values[] = $obj;
    }

    $categorie->success = true;

    echo json_encode($categorie);
} else {
    echo json_encode($connectionDB);
}
