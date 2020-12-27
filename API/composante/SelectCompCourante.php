<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$comp_courante = new stdClass();
$comp_courante->values = [];
$comp_courante->success = true;
$comp_courante->errors = [];

$strReq = "SELECT `id_comp` FROM `comp_courante`";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters)) {
    if (isset($postObj->filters->id_comp)) {
        $whereSet = true;
        $firstFilter = true;

        $strReq .= " WHERE ( ";
        foreach ($postObj->filters->id_comp as $id_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`id_comp` = \"$id_comp\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `id_comp` DESC ";
else
    $strReq .= " ORDER BY `id_comp`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->id_comp = utf8_encode($req['id_comp']);
        
            $comp_courante->values[] = $obj;
        }
    }
}
else {
    $comp_courante->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $comp_courante->errors[] = $obj;
}

echo json_encode($comp_courante);