<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$annee = new stdClass();
$annee->values = [];
$annee->success = true;
$annee->errors = [];

$strReq = "SELECT `annee` FROM `annee_univ` ";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters))
    if (isset($postObj->filters->annee)) {
        $whereSet = true;
        $firstArrayFilter=true;
        
        $strReq .= ' WHERE ( ';
        foreach ($postObj->filters->annee as $resAnnee) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            
            $strReq .= " `annee` = \"$resAnnee\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `annee` DESC ";
else
    $strReq .= " ORDER BY `annee` ";
$strReq .= " LIMIT $postObj->quantity OFFSET $postObj->skip ";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->annee = utf8_encode($req['annee']);
        
            $annee->values[] = $obj;
        }
    }
}
else {
    $annee->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $annee->errors[] = $obj;
}

echo json_encode($annee);