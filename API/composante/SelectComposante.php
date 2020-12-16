<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$composante = new stdClass();
$composante->values = [];
$composante->success = false;
$composante->errors = [];

$strReq = "SELECT `id_comp`, `nom_comp`, `lieu_comp` FROM `composante`";
$postObj = json_decode(file_get_contents('php://input'));
if (isset($postObj->filters)) {
    $firstFilter = true;
    $whereSet = false;
    if (isset($postObj->filters->id_comp)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->id_comp as $id_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`id_comp` = \"$id_comp\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->nom_comp)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->nom_comp as $nom_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`nom_comp` = \"$nom_comp\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->lieu_comp)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->lieu_comp as $lieu_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`lieu_comp` = \"$lieu_comp\"";
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
    foreach ($requete as $req) {
        $obj = new stdClass();
    
        $obj->id_comp = utf8_encode($req['id_comp']);
    
        $obj->nom_comp = utf8_encode($req['nom_comp']);
    
        $obj->lieu_comp = utf8_encode($req['lieu_comp']);
    
        $composante->values[] = $obj;
    }

    $composante->success = true;
}
else {
    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $composante->errors[] = $obj;
}


echo json_encode($composante);
