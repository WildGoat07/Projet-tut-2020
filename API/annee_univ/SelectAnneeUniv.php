<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$annee = new stdClass();
$annee->values = [];
$annee->success = false;
$annee->errors = [];

$strReq = "SELECT `annee` FROM `annee_univ`";
$postObj = json_decode(file_get_contents('php://input'));

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();


if ($error[0]=='00000') {
    foreach ($requete as $req) {
        $obj = new stdClass();
    
        $obj->annee = utf8_encode($req['annee']);
    
        $annee->values[] = $obj;
    }

    $annee->success = true;
}
else {
    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $annee->errors[] = $obj;
}

echo json_encode($annee);
